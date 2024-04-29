open Evaluator

[<EntryPoint>]
let main args =
    let input = args[0]
    let ast_maybe = parse input
    match ast_maybe with
    | Some ast -> 
        printfn "%A" (prettyprint ast)
    | None ->
        printfn "Invalid program."

    0