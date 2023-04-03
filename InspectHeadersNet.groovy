#!/usr/bin/env groovy
package com.itextpdf.copyright

import groovy.cli.commons.CliBuilder
import groovy.ant.FileNameFinder

void processScript(String... args) {
    def time = System.currentTimeMillis()
    def date = Calendar.getInstance()

    def scriptDir = ""
    def inclList = ""

    def cli = new CliBuilder()
    cli.h(type: Boolean, 'help')
    cli.i(type: String, longOpt: 'includePattern', 'pattern in Ant\'s fileset format')
    cli.d(type: String, longOpt: 'dir', 'base dir for search (LICENSE.md should be present in destination folder)')
    def options = cli.parse(args)

    if (options == null) {
        throw new ScriptException("", 101)
    }

    if ( options.h ) {
        cli.usage()
        throw new ScriptException("", 0)
    }

    if ( options.i ) {
        inclListFile = options.i
        println "Including files in list '$inclListFile'"
        new File(inclListFile).eachLine { line ->
            inclList = inclList + " " + line
        }
    } else {
        println "Including all files"
        inclList = '**/*.java **/*.cs **/*.nuspec'
    }

    if ( options.arguments()[0] ) {
        scriptDir = options.arguments()[0].trim()
    } else if (options.d) {
        scriptDir = options.d.trim()
    } else {
        scriptDir = new File("").getAbsolutePath()
    }
    println "Starting groovy script @ $scriptDir"

    def licenseFile = new File("${scriptDir}/LICENSE.md")
    println "licenseFile is ${licenseFile.canonicalPath}"

    copyrightYear = "Copyright (c) 1998-" + date.get(Calendar.YEAR) + " Apryse Group NV"
    def sb = new StringBuilder()
    sb << "/*\n"
    sb << "This file is part of the iText (R) project.\n"
    sb << "" + copyrightYear
    sb << "\n"
    sb << "Authors: Apryse Software.\n\n"
    sb << licenseFile.text
    sb << " \n*/\n"
    copyrightText = sb.toString()
    def producerLine = /private static final String producerLine = iTextProductName \+ " " \+ release \+ " \\u00a92000-.*?Apryse Group NV";/

    def sourceFolder = new File(scriptDir)
    if ( ! sourceFolder.directory ) {
        println "${sourceFolder.name} is not an existing folder"
        println "Aborting port attempt"
        return
    } else {
        println "Source folder is ${sourceFolder.canonicalPath}"
    }

    def other_copyrights = [
            "Adobe"   : /Copyright .* Adobe Systems Incorporated/,
            "Apache"  : /Copyright .* Apache Software Foundation/,
            "AL2.0"   : /Apache License, Version 2.0/,
            "ZXing"   : /Copyright .* ZXing authors/,
            "Sun"     : /Copyright .* Sun Microsystems, Inc./,
            "Fontbox" : /Copyright .* www.fontbox.org/,
            "SGI"     : /Copyright .* Silicon Graphics, Inc./,
            "Clipper" : /Copyright .* Angus Johnson/,
            "Oracle"  : /Copyright .* Oracle/,
            "Zlib"    : /Copyright .* ymnk, JCraft,Inc./,
            "zlib"    : /Copyright .* Lapo Luchini/,
            "sboxes"  : /Copyright: .* Dr B. R Gladman/,
            "iharder" : /@author Robert Harder/,
            "Google"  : /Copyright .* Google Inc./,
            "MIT"     : /Distributed under MIT license/
    ]

// needed when run on Windows
    if ( File.separator.equals("\\") ) {
        inclList = inclList.replace("/", "\\\\")
    }

    fileList = new FileNameFinder().getFileNames(scriptDir, inclList)
    fileList.each {fileStr ->
        File file = new File(fileStr)
        skip = false

        def content = new StringBuilder()
        def changed = false

        if ( file.name.endsWith("package-info.java") ) {
            skip = true
        }

        if ( file.canonicalPath.contains("itext.kernel/bouncycastle") ) {
            skip = true
        }

        other_copyrights.each { author, copyright ->
            if ( file.text.find(copyright) ) {
                skip = true
            }
        }

        if ( !skip ) {
            copyright = /Copyright .* Apryse/
            if ( file.text.find(copyright) ) {
                file.eachLine('UTF-8') { line ->
                    if ( line.find(copyright) && !line.find(date.get(Calendar.YEAR) + "") ) {
                        println "[YEAR] ${line} - ${file.canonicalPath}"
                        if ( file.name.endsWith("AssemblyInfo.cs") ) {
                            content << "[assembly: AssemblyCopyright(\"" + copyrightYear + "\")]"
                        } else if ( file.name.endsWith(".nuspec") ) {
                            content << "" + "<copyright>${copyrightYear}</copyright>"
                        } else {
                            content << "" + copyrightYear
                        }
                        changed = true
                    } else {
                        content << line
                    }
                    content << "\n"
                }
            } else {
                println "[MISSING] Copyright added to ${file.canonicalPath}"
                content << copyrightText
                content << file.getText('UTF-8')
                changed = true
            }
            if ( changed ) {
                file.write(content.toString(), "UTF8")
            }
        }

        copyrightTo = "COPYRIGHT_TO = .*?;"
        if (file.name.contains("ProductData")) {
            String contents = file.getText('UTF-8')
            contents = contents.replaceAll( copyrightTo , "COPYRIGHT_TO = " + date.get(Calendar.YEAR) + ";")
            file.write(contents, "UTF-8")
        }

        // the following block of code makes sense only for 7.1. Drop these lines when 7.1 is no longer supported
        if ( file.absolutePath.endsWith("kernel" + File.separator + "Version.java") ) {
            String contents = file.getText('UTF-8')
            contents = contents.replaceAll( producerLine, 'private static final String producerLine = iTextProductName + " " + release + " \\\\u00a92000-' + date.get(Calendar.YEAR) + ' iText Group NV";' )
            file.write(contents, "UTF-8")
        }
    }

    println "\nCopyright check took " + ( ( System.currentTimeMillis() - time ) / 1000 ) + " seconds."
}

class ScriptException extends Exception {
    int exitCode

    ScriptException(String message, int exitCode) {
        super(message)
        this.exitCode = exitCode
    }
}

try {
    processScript(args)
} catch (ScriptException e) {
    System.exit(e.exitCode)
}
