module Evaluator

open Combinator

(* START AST DEFINITION *)
type Expr =
| Variable of char
| Abstraction of char * Expr
| Application of Expr * Expr
(* END AST DEFINITION *)

(* START PARSER DEFINITION *)
let expr, exprImpl = recparser() (* declares that an expr parser exists *)

let variable : Parser<Expr> = (* defines a variable parser *)
    pletter |>> (fun c -> Variable c)

let abstraction : Parser<Expr> = (* defines an abstraction parser *)
    pbetween
        (pstr "(L")
        (pseq 
            (pleft pletter (pstr "."))
             expr
            (fun (var,exp) -> Abstraction (var,exp)))
        (pchar ')')
    

let application : Parser<Expr> = (* defines an application parser *)
    pbetween
        (pchar '(')
        (pseq expr expr (fun (exp1, exp2) -> Application (exp1, exp2)))
        (pchar ')')

exprImpl := variable <|> abstraction <|> application (* defines the expr parser *)

let grammar = pleft expr peof

(* END PARSER DEFINITION *)

let rec prettyprint(exp: Expr) : string = (* turns an abstract syntax tree into a string *)
    match exp with
    | Variable x -> sprintf "Variable(%c)" x
    | Abstraction (x, expr) -> sprintf "Abstraction(Variable(%c), %s)" x (prettyprint expr)
    | Application (expr1, expr2) -> sprintf "Application(%s, %s)" (prettyprint expr1) (prettyprint expr2)

let parse input = (* parse input *)
    let i = prepare input
    match grammar i with
    | Success(ast,_) -> Some ast
    | Failure(_,_) -> None