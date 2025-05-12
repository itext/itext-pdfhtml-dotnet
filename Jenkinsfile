#!/usr/bin/env groovy
@Library('pipeline-library')_

def repoName = "pdfHtml"
def dependencyRegex = "itextcore"
def solutionFile = "itext.html2pdf.sln"
def frameworksToTest = "net461"
def frameworksToTestForMainBranches = "net461;netcoreapp2.0"

automaticDotnetBuild(repoName, dependencyRegex, solutionFile, frameworksToTest, frameworksToTestForMainBranches)
