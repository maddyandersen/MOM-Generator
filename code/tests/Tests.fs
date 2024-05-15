namespace tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Parser
open Evaluator

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestPrettyPrint () =
        let prog = "monday breakfast lee"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            let actual = prettyprint ast
            let expected = "Monday Breakfast Lee"
            Assert.AreEqual(expected, actual)
        | None ->
            Assert.IsTrue(false);

    [<TestMethod>]
    member this.TestReadFile () =
        let prog = IO.File.ReadAllText("../../../../sample_prog/sample1.txt")
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            Assert.IsTrue(true)
        | None ->
            Assert.IsTrue(false);

    (*
    [<TestMethod>]
    member this.TestGenerateCategory () =
        let prog = IO.File.ReadAllText("../../../../sample_prog/invalid_order.txt")
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            Assert.IsTrue(true)
        | None ->
            Assert.IsTrue(false);
    *)
    
