open Parser
open Evaluator
open System.IO

[<EntryPoint>]
let main argv : int =
    (* check for proper usage *)
    if argv.Length > 1 then
        printfn "Usage: dotnet run"
        exit 1
    
    (* print instructions *)
    

    (* determine the file to read *)
    let file = 
        if argv.Length = 1 then
            argv[0]
        else
            printf "Please enter the file with your request: "
            System.Console.ReadLine()

    (* read in the input file *)
    let input = File.ReadAllText file

    (* try to parse what they gave us *)
    let ast_maybe = parse input

    (* try to evaluate what we parsed... or not *)
    match ast_maybe with
    | Some ast ->
        let str = eval ast
        printfn "%s" str 
        0
    | None     ->
        printfn "Invalid program."
        1
