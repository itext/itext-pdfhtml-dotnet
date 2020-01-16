#!/usr/bin/env groovy
@Library('pipeline-library')_

def schedule = env.BRANCH_NAME.contains('master') ? '@monthly' : env.BRANCH_NAME == 'develop' ? '@midnight' : ''

pipeline {

    agent { label 'windows' }

    options {
        ansiColor('xterm')
        buildDiscarder(logRotator(artifactNumToKeepStr: '1'))
        parallelsAlwaysFailFast()
        skipStagesAfterUnstable()
        timeout(time: 60, unit: 'MINUTES')
        timestamps()
    }

    triggers {
        cron(schedule)
    }

    stages {
        stage('Wait for blocking jobs') {
            steps {
                script {
                    properties([[$class: 'BuildBlockerProperty', blockLevel: 'GLOBAL', blockingJobs: ".*/itextcore/${env.JOB_BASE_NAME}", scanQueueFor: 'ALL', useBuildBlocker: true]])
                }
            }
        }
        stage('Build') {
            options {
                retry(2)
            }
            stages {
                stage('Clean workspace') {
                    options {
                        timeout(time: 5, unit: 'MINUTES')
                    }
                    steps {
                        cleanWs deleteDirs: true, patterns: [
                            [pattern: 'packages', type: 'INCLUDE'],
                            [pattern: 'global-packages', type: 'INCLUDE'],
                            [pattern: 'tmp/NuGetScratch', type: 'INCLUDE'],
                            [pattern: 'http-cache', type: 'INCLUDE'],
                            [pattern: 'plugins-cache', type: 'INCLUDE'],
                            [pattern: '**/obj', type: 'INCLUDE'],
                            [pattern: '**/bin', type: 'INCLUDE'],
                            [pattern: '**/*.nupkg', type: 'INCLUDE']
                        ]
                    }
                }
                stage('Install branch dependencies') {
                    options {
                        timeout(time: 5, unit: 'MINUTES')
                    }
                    when {
                        not {
                            anyOf {
                                branch "master"
                                branch "develop"
                            }
                        }
                    }
                    steps {
                        script {
                            getAndConfigureJFrogCLI()
                            sh "./jfrog rt dl --flat=true branch-artifacts/${env.JOB_BASE_NAME}/**/dotnet/"
                            // create global-packages directory
                            dir("${env.WORKSPACE}/global-packages") {writeFile file:'dummy', text:''}
                            nuspecFiles = findFiles(glob: '**/*.nuspec')
                            buildArtifacts = []
                            nuspecFiles.each{ nuspecFile ->
                                def xmlTxt = sh(returnStdout: true, script: '#!/bin/sh -e\n' + "cat \"${nuspecFile.path.replace('\\','/')}\"").replaceAll("^.*<", "<")
                                def xml = new XmlSlurper(false, false).parseText("${xmlTxt}")
                                def artifactId = "${xml.metadata.id}"
                                def artifactVersion = "${xml.metadata.version}"
                                artifact = "${artifactId}.${artifactVersion}"
                                buildArtifacts.add(artifact)
                            }
                            withEnv(["NUGET_PACKAGES=${env.WORKSPACE}/global-packages", "temp=${env.WORKSPACE}/tmp/NuGetScratch", "NUGET_HTTP_CACHE_PATH=${env.WORKSPACE}/http-cache", "NUGET_PLUGINS_CACHE_PATH=${env.WORKSPACE}/plugins-cache", "gsExec=${gsExec}", "compareExec=${compareExec}"]) {
                                 createInstallAllFile(findFiles(glob: '**/*.nupkg'), buildArtifacts)
                                 load 'installAll.groovy'
                            }
                            nupkgFiles = findFiles(glob: '*.nupkg')
                            nupkgFiles.each{ nupkgFile ->
                                println "Delete downloaded ${nupkgFile.path}"
                                cleanWs deleteDirs: true, patterns: [[pattern: "${nupkgFile.path}", type: 'INCLUDE']]
                            }
                        }
                    }
                }
                stage('Compile') {
                    options {
                        timeout(time: 20, unit: 'MINUTES')
                    }
                    steps {
                        withEnv(["NUGET_PACKAGES=${env.WORKSPACE}/global-packages", "temp=${env.WORKSPACE}/tmp/NuGetScratch", "NUGET_HTTP_CACHE_PATH=${env.WORKSPACE}/http-cache", "NUGET_PLUGINS_CACHE_PATH=${env.WORKSPACE}/plugins-cache", "gsExec=${gsExec}", "compareExec=${compareExec}"]) {
                            bat "\"${env.NuGet}\" restore itext.html2pdf.sln"
                            bat "dotnet restore itext.html2pdf.sln"
                            bat "dotnet build itext.html2pdf.sln --configuration Release --source ${env.WORKSPACE}/packages"
                            script {
                                createPackAllFile(findFiles(glob: '**/*.nuspec'))
                                load 'packAll.groovy'
                            }
                        }
                    }
                }
            }
            post {
                failure {
                    sleep time: 2, unit: 'MINUTES'
                }
                success {
                    script { currentBuild.result = 'SUCCESS' }
                }
            }
        }
        stage('Run Tests') {
            options {
                timeout(time: 60, unit: 'MINUTES')
            }
            steps {
                withEnv(["NUGET_PACKAGES=${env.WORKSPACE}/global-packages", "temp=${env.WORKSPACE}/tmp/NuGetScratch", "NUGET_HTTP_CACHE_PATH=${env.WORKSPACE}/http-cache", "NUGET_PLUGINS_CACHE_PATH=${env.WORKSPACE}/plugins-cache", "gsExec=${gsExec}", "compareExec=${compareExec}"]) {
                    script {
                        createRunTestDllsFile(findFiles(glob: '**/itext.*.tests.dll'))
                        load 'runTestDlls.groovy'
                        createRunTestCsProjsFile(findFiles(glob: '**/itext.*.tests.netstandard.csproj'))
                        load 'runTestCsProjs.groovy'
                    }
                }
            }
        }
        stage('Artifactory Deploy') {
            options {
                timeout(time: 5, unit: 'MINUTES')
            }
            when {
                anyOf {
                    branch "master"
                    branch "develop"
                }
            }
            steps {
                script {
                    getAndConfigureJFrogCLI()
                    findFiles(glob: '*.nupkg').each { item ->
                        def itemArray = (item =~ /(.*?)(\.[0-9]*\.[0-9]*\.[0-9]*(-SNAPSHOT)?)/)
                        def dir = itemArray[ 0 ][ 1 ]
                        sh "./jfrog rt u \"${item.path}\" nuget/${dir}/ --flat=false --build-name ${env.BRANCH_NAME} --build-number ${env.BUILD_NUMBER}"
                    }
                }
            }
        }
        stage('Branch Artifactory Deploy') {
            options {
                timeout(time: 5, unit: 'MINUTES')
            }
            when {
                not {
                    anyOf {
                        branch "master"
                        branch "develop"
                    }
                }
            }
            steps {
                script {
                    if (env.GIT_URL) {
                        repoName = ("${env.GIT_URL}" =~ /(.*\/)(.*)(\.git)/)[ 0 ][ 2 ]
                        findFiles(glob: '*.nupkg').each { item ->
                            sh "./jfrog rt u \"${item.path}\" branch-artifacts/${env.BRANCH_NAME}/${repoName}/dotnet/ --recursive=false --build-name ${env.BRANCH_NAME} --build-number ${env.BUILD_NUMBER} --props \"vcs.revision=${env.GIT_COMMIT};repo.name=${repoName}\""
                        }
                    }
                }
            }
        }
        stage('Archive Artifacts') {
             options {
                timeout(time: 5, unit: 'MINUTES')
            }
            steps {
                archiveArtifacts allowEmptyArchive: true, artifacts: '*.nupkg'
            }
        }
    }

    post {
        always {
            echo 'One way or another, I have finished \uD83E\uDD16'
        }
        success {
            echo 'I succeeeded! \u263A'
            cleanWs deleteDirs: true
        }
        unstable {
            echo 'I am unstable \uD83D\uDE2E'
        }
        failure {
            echo 'I failed \uD83D\uDCA9'
        }
        changed {
            echo 'Things were different before... \uD83E\uDD14'
        }
        fixed {
            script {
                if ((env.BRANCH_NAME == 'master') || (env.BRANCH_NAME == 'develop')) {
                    slackNotifier("#ci", currentBuild.currentResult, "${env.BRANCH_NAME} - Back to normal")
                }
            }
        }
        regression {
            script {
                if ((env.BRANCH_NAME == 'master') || (env.BRANCH_NAME == 'develop')) {
                    slackNotifier("#ci", currentBuild.currentResult, "${env.BRANCH_NAME} - First failure")
                }
            }
        }
    }

}

@NonCPS // has to be NonCPS or the build breaks on the call to .each
def createInstallAllFile(list, buildArtifacts) {
    // creates file because the sh command brakes the loop
    def buildArtifactsList = buildArtifacts.join(",")
    def ws = "${env.WORKSPACE.replace('\\','/')}"
    def cmd = "import groovy.xml.XmlUtil\n"
    cmd = cmd + "def xmlTxt = ''\n"
    cmd = cmd + "def buildArtifacts = \"${buildArtifactsList}\".split(',').collect{it as java.lang.String}\n"
    list.each { item ->
        filename = item.getName()
        def itemArray = (item.getName() =~ /(.*?)\.([0-9]*)\.([0-9]*)\.([0-9]*)(|-SNAPSHOT)/)
        def name = itemArray[0][1]
        if (!buildArtifacts.contains("${filename.replace(".nupkg","")}")) {
            cmd = cmd + "try {xmlTxt = sh(returnStdout: true, script: 'unzip -p ${filename} ${name}.nuspec')} catch (Exception err) { }\n"
            cmd = cmd + "xmlTxt = \"\${xmlTxt.replaceFirst('.*?<?xml version','<?xml version')}\"\n"
            cmd = cmd + "xml = new XmlSlurper(false, false).parseText(xmlTxt)\n"
            cmd = cmd + "install = true\n"
            cmd = cmd + "xml.metadata.dependencies.group.dependency.each { dependency ->\n"
            cmd = cmd + "    artifact = \"\${dependency[\'@id\']}.\${dependency[\'@version\']}\".toString()\n"
            cmd = cmd + "    if (buildArtifacts.contains(artifact)) {\n"
            cmd = cmd + "        install = false\n"
            cmd = cmd + "    }\n"
            cmd = cmd + "}\n"
            cmd = cmd + "xml.metadata.dependencies.dependency.each { dependency ->\n"
            cmd = cmd + "    artifact = \"\${dependency[\'@id\']}.\${dependency[\'@version\']}\".toString()\n"
            cmd = cmd + "    if (buildArtifacts.contains(artifact)) {\n"
            cmd = cmd + "        install = false\n"
            cmd = cmd + "    }\n"
            cmd = cmd + "}\n"
            cmd = cmd + "if (install) {\n"
            cmd = cmd + "    xml.metadata.dependencies.group.dependency.each { dependency ->\n"
            cmd = cmd + "        if (\"\${dependency[\'@id\']}\".contains(\"itext7\")) {\n"
            cmd = cmd + "            sh \"${env.NuGet.replace('\\','/')} install \${dependency[\'@id\']} -PreRelease -Version \${dependency[\'@version\']} -OutputDirectory packages -Source '${ws};https://repo.itextsupport.com/api/nuget/nuget;https://api.nuget.org/v3/index.json'\"\n"
            cmd = cmd + "        }\n"
            cmd = cmd + "    }\n"
            cmd = cmd + "    xml.metadata.dependencies.dependency.each { dependency ->\n"
            cmd = cmd + "        if (\"\${dependency[\'@id\']}\".contains(\"itext7\")) {\n"
            cmd = cmd + "            sh \"${env.NuGet.replace('\\','/')} install \${dependency[\'@id\']} -PreRelease -Version \${dependency[\'@version\']} -OutputDirectory packages -Source '${ws};https://repo.itextsupport.com/api/nuget/nuget;https://api.nuget.org/v3/index.json'\"\n"
            cmd = cmd + "        }\n"
            cmd = cmd + "    }\n"
            cmd = cmd + "    try {sh 'rm -r ${ws}/global-packages/${name}'} catch (Exception err) { }\n"
            cmd = cmd + "    try {sh 'rm -r ${ws}/packages/${filename.replace('.nupkg','')}'} catch (Exception err) { }\n"
            cmd = cmd + "    sh '\"${env.NuGet.replace('\\','/')}\" install ${name} -PreRelease -OutputDirectory packages -Source \"${ws};https://api.nuget.org/v3/index.json\"'\n"
            cmd = cmd + "    sh '\"${env.NuGet.replace('\\','/')}\" push ${ws}/${filename} -Source \"${ws}/global-packages\"'\n"
            cmd = cmd + "} else {\n"
            cmd = cmd + "    println \"Not installing '${filename}' - this repository will build dependencies for it....\"\n"
            cmd = cmd + "}\n"
        }
    }
    writeFile file: 'installAll.groovy', text: cmd
}

@NonCPS // has to be NonCPS or the build breaks on the call to .each
def createPackAllFile(list) {
    // creates file because the bat command brakes the loop
    def cmd = ''
    list.each { item ->
        if (!item.path.contains("packages")) {
            cmd = cmd + "bat '\"${env.NuGet.replace('\\','\\\\')}\" pack \"${item.path.replace('\\','\\\\')}\"'\n"
        }
    }
    writeFile file: 'packAll.groovy', text: cmd
}

@NonCPS // has to be NonCPS or the build breaks on the call to .each
def createRunTestDllsFile(list) {
    // creates file because the bat command brakes the loop
    def ws = "${env.WORKSPACE.replace('\\','\\\\')}"
    def nunit = "${env.'Nunit3-console'.replace('\\','\\\\')}"
    def cmd = ''
    list.each { item ->
        if (!item.path.contains("netcoreapp1.0") && !item.path.contains("obj")) {
            cmd = cmd + "bat '\"${nunit}\" \"${ws}\\\\${item.path.replace('\\','\\\\')}\" --result=${item.name}-TestResult.xml'\n"
        }
    }
    writeFile file: 'runTestDlls.groovy', text: cmd
}

@NonCPS // has to be NonCPS or the build breaks on the call to .each
def createRunTestCsProjsFile(list) {
    // creates file because the bat command brakes the loop
    def ws = "${env.WORKSPACE.replace('\\','\\\\')}"
    def cmd = ''
    list.each { item ->
        cmd = cmd + "bat 'dotnet test ${ws}\\\\${item.path.replace('\\','\\\\')} --framework netcoreapp1.0 --configuration Release --no-build --logger \"trx;LogFileName=results.trx\"'\n"
    }
    writeFile file: 'runTestCsProjs.groovy', text: cmd
}

