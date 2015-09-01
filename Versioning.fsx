#r @".\tools\FakeLib.dll"
#r @".\tools\FSharp.Data.dll"

open Fake
open Fake.Git
open Fake.SemVerHelper
open Fake.AssemblyInfoFile
open FSharp.Data
open System
open System.IO

type VersionConfig = JsonProvider< """ { "version":"0.1.0", "build":1} """ >

let assemblyInfoFile = getBuildParamOrDefault "asmInfo" "../AssemblyInfo.Version.cs"
let configFile = getBuildParamOrDefault "config" "./version.json"

Target "SetVersion" (fun _ -> 
    try
        let gitInfo = getCurrentHash() |> showName (".")
        let config = VersionConfig.Load configFile 
        let version = config.Version + "." + string config.Build
        let versionDetailed = version + "-" + gitInfo    

        trace version
        trace versionDetailed

        CreateCSharpAssemblyInfo assemblyInfoFile 
                [ Attribute.Version version
                  Attribute.FileVersion version
                  Attribute.InformationalVersion versionDetailed]  
    with
        | _ as ex -> trace ex.Message 
    )

Target "IncBuild" (fun _ ->
    let config = VersionConfig.Load configFile
    let modified = VersionConfig.Root(version = config.Version, build = config.Build + 1)
    let json = modified.JsonValue.ToString()
    trace json
    File.WriteAllText(configFile, json))

Target "Default" DoNothing

RunTargetOrDefault "Default"
