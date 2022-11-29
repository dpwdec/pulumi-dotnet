﻿open System.IO
open System.Linq
open Fake.IO
open Fake.Core

/// Recursively tries to find the parent of a file starting from a directory
let rec findParent (directory: string) (fileToFind: string) = 
    let path = if Directory.Exists(directory) then directory else Directory.GetParent(directory).FullName
    let files = Directory.GetFiles(path)
    if files.Any(fun file -> Path.GetFileName(file).ToLower() = fileToFind.ToLower()) 
    then path 
    else findParent (DirectoryInfo(path).Parent.FullName) fileToFind

let repositoryRoot = findParent __SOURCE_DIRECTORY__ "README.md";

let sdk = Path.Combine(repositoryRoot, "sdk")

let pulumiSdk = Path.Combine(sdk, "Pulumi")
let pulumiSdkTests = Path.Combine(sdk, "Pulumi.Tests")
let pulumiAutomationSdk = Path.Combine(sdk, "Pulumi.Automation")
let pulumiAutomationSdkTests = Path.Combine(sdk, "Pulumi.Automation.Tests")
let pulumiFSharp = Path.Combine(sdk, "Pulumi.FSharp")

/// Runs `dotnet clean` command against the solution file,
/// then proceeds to delete the `bin` and `obj` directory of each project in the solution
let cleanSdk() = 
    printfn "Cleaning Pulumi SDK"
    if Shell.Exec(Tools.dotnet, "clean", sdk) <> 0
    then failwith "clean failed"

    let projects = [ 
        pulumiSdk
        pulumiSdkTests
        pulumiAutomationSdk
        pulumiAutomationSdkTests
        pulumiFSharp
    ]

    for project in projects do
        Shell.deleteDir (Path.Combine(project, "bin"))
        Shell.deleteDir (Path.Combine(project, "obj"))


/// Runs `dotnet restore` against the solution file without using cache
let restoreSdk() = 
    printfn "Restoring Pulumi SDK packages"
    if Shell.Exec(Tools.dotnet, "restore --no-cache", sdk) <> 0
    then failwith "restore failed"

let buildSdk() = 
    cleanSdk()
    restoreSdk()
    printfn "Building Pulumi SDK"
    if Shell.Exec(Tools.dotnet, "build --configuration Release", sdk) <> 0
    then failwith "build failed"

let testPulumiSdk() = 
    cleanSdk()
    restoreSdk()
    printfn "Testing Pulumi SDK"
    if Shell.Exec(Tools.dotnet, "test --configuration Release", pulumiSdkTests) <> 0
    then failwith "tests failed"

let testPulumiAutomationSdk() = 
    cleanSdk()
    restoreSdk()
    printfn "Testing Pulumi Automation SDK"
    if Shell.Exec(Tools.dotnet, "test --configuration Release", pulumiAutomationSdkTests) <> 0
    then failwith "automation tests failed"

[<EntryPoint>]
let main(args: string[]) : int = 
    match args with
    | [| "clean-sdk" |] -> cleanSdk()
    | [| "build-sdk" |] -> buildSdk()
    | [| "test-sdk" |] -> testPulumiSdk()
    | [| "test-automation-sdk" |] -> testPulumiAutomationSdk()
    | otherwise -> printfn "%A" otherwise

    0
