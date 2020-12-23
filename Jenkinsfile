#!/usr/bin/env groovy
@Library('pipeline-library')_

def repoName = "pdfHtml"
def dependencyRegex = "itextcore"
def solutionFile = "itext.html2pdf.sln"
def csprojFramework = "netcoreapp2.0"

automaticDotnetBuild(repoName, dependencyRegex, solutionFile, csprojFramework)
