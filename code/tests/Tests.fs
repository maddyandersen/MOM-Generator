namespace tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Parser
open Evaluator

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);

    [<TestMethod>]
    member this.TestGlutenFree () = // test gluten free and non-gluten free option
        let prog = System.IO.File.ReadAllText "/Users/stella/Desktop/Computer Science/cs334/cs334-project-mia1-sjo2/code/sample_prog/sample4.txt"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            Assert.IsTrue(true)
        | None ->
            Assert.IsTrue(false)
    
    [<TestMethod>]
    member this.TestEval () = 
        let prog = System.IO.File.ReadAllText "/Users/stella/Desktop/Computer Science/cs334/cs334-project-mia1-sjo2/code/sample_prog/sample4.txt"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            Assert.IsTrue(true)
        | None ->
            Assert.IsTrue(false)

