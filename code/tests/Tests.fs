namespace tests

open System
open System.IO
open Microsoft.VisualStudio.TestTools.UnitTesting
open Parser
open Evaluator

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);

    (*
    [<TestMethod>]
    member this.TestGlutenFree () = // test gluten free and non-gluten free option
        let prog = File.ReadAllText "test_files/gluten_free_tests.txt"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            Assert.IsTrue(true)
        | None ->
            Assert.IsTrue(false)

    [<TestMethod>]
    member this.TestEval () = 
        let prog = File.ReadAllText "test_files/sample_tests.txt"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast ->
            let str = eval ast
            Assert.IsTrue(true)
        | None ->
            Assert.IsTrue(false)
    *)

    [<TestMethod>]
    member this.TestBadInput () = 
        let prog = "hello world"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast -> // should not parse
            Assert.IsTrue(false)
        | None ->
            Assert.IsTrue(true)

    [<TestMethod>]
    member this.TestIncomplete () = 
        let prog = "monday breakfast"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast -> // should not parse
            Assert.IsTrue(false)
        | None ->
            Assert.IsTrue(true)

    [<TestMethod>]
    member this.TestNoInput () = 
        let prog = ""
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast -> // should not parse
            Assert.IsTrue(false)
        | None ->
            Assert.IsTrue(true)
    
    [<TestMethod>]
    member this.TestInvalidOrder () = 
        let prog = "friday dinner fresh n go"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast -> // should not parse
            Assert.IsTrue(false)
        | None ->
            Assert.IsTrue(true)

    [<TestMethod>]
    member this.TestGivenOrder () = 
        let prog = "wednesday, breakfast, lee, breakfast sandwiches, mcwilliams"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast -> // should not parse
            let actual = eval ast
            let expected = "The Mcwilliams from the Breakfast Sandwiches category for Breakfast at Lee on Wednesday is a great choice!"
            Assert.AreEqual(actual,expected)
        | None ->
            Assert.IsTrue(false)

    [<TestMethod>]
    member this.TestNotOpen () = 
        let prog = "sunday, dinner, lee, any, any"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast -> // should not parse
            let actual = eval ast
            let expected = "Given location is not open for given day or meal."
            Assert.AreEqual(actual,expected)
        | None ->
            Assert.IsTrue(false)

    [<TestMethod>]
    member this.TestNoneOpen () = 
        let prog = "sunday, breakfast, any, breakfast sandwiches, any"
        let ast_maybe = parse prog
        match ast_maybe with
        | Some ast -> // should not parse
            let actual = eval ast
            let expected = "No locations are open for given day and meal combination."
            Assert.AreEqual(actual,expected)
        | None ->
            Assert.IsTrue(false)

