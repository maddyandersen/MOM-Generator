open Parser
open Evaluator
open System.IO

[<EntryPoint>]
let main argv : int =
    (* Check for proper usage *)
    if argv.Length <> 1 then
        printfn "Usage: dotnet run <file>"
        exit 1

    (* read in the input file *)
    let file = argv[0]
    let input = File.ReadAllText file

    (* try to parse what they gave us *)
    let ast_maybe = parse input

    (* try to evaluate what we parsed... or not *)
    match ast_maybe with
    | Some ast ->
        let str = prettyprint ast
        printfn "%s" str
        0
    | None     ->
        printfn "Invalid program."
        1
