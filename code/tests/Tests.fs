namespace tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Parser
open Evaluator

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        let prog = "monday breakfast lee"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            let actual = prettyprint ast
            let expected = "Monday Breakfast Lee"
            Assert.AreEqual(expected, actual)
        | None ->
            Assert.IsTrue(false);
