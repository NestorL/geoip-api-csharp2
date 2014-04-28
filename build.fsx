//#I @"lib"
#r "FakeLib.dll"
//#r "Fake.Gallio.dll"
//#r "System.Xml.Linq"
//#load "fake.fsx"

open System
open System.IO
//open System.Xml.Linq
open Fake
open Fake.FileUtils

// Properties
let config = getBuildParamOrDefault "config" "release"
let target = getBuildParamOrDefault "target" "BuildAll"

let buildParams defaults =
        { defaults with
            Verbosity = Some(Quiet)
            Targets = ["ReBuild"]
            Properties =
                [
                    "Optimize", "True"
                    "DebugSymbols", "True"
                    "Configuration", config
                ]
        }

let slnBuild sln x = 
//    let strongName = 
//        if File.Exists keyFile
//            then ["SignAssembly","true"; "AssemblyOriginatorKeyFile",keyFile]
//            else []
    let strongName = []
    sln |> build (fun p -> { p with 
                                Targets = [x]
                                Properties = ["Configuration",config] @ strongName })

let mainSln = slnBuild "geoip-api-csharp2.sln"
//let sampleSln = slnBuild "SampleSolrApp.sln"

let outDir = "./drop/build/"
let buildDir = outDir + "/out/"
let testDir = outDir + "/test/"
let deployDir = outDir + "/deploy/"

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; deployDir]
    CleanDir outDir
    mainSln "Clean"
    //sampleSln "Clean"
)


// Default target
Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

//Target "BuildApp" (fun _ -> mainSln "Rebuild")

Target "BuildApp" (fun _ ->
    !! @"src\**\*.csproj"
      |> MSBuild buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildAll" DoNothing
Target "Merge" DoNothing
Target "BuildSample" DoNothing

// Dependencies
"Clean"
    ==> "Default"

//"BuildAll" <== ["BuildApp";"Merge";"BuildSample"]
"BuildAll" <== ["BuildApp"]

// start build
//RunTargetOrDefault "Default"
Run target