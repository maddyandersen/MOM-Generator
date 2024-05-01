module Parser

open AST
open Combinator

(*
 *  <request> ::= <order> | <order> '\n' <request>
 *  <order>   ::= <day>␣<meal>
 *  <day>     ::= monday | tuesday | wednesday | thursday | friday | saturday | sunday
 *  <meal>    ::= breakfast | lunch | mid day | dinner | late night
 *)

let pad p = pbetween pws0 p pws0
let day =
    (pstr "monday" |>> (fun _ -> Monday)) <|>
    (pstr "tuesday" |>> (fun _ -> Tuesday)) <|>
    (pstr "wednesday" |>> (fun _ -> Wednesday)) <|>
    (pstr "thursday" |>> (fun _ -> Thursday))  <|>
    (pstr "friday" |>> (fun _ -> Friday)) <|>
    (pstr "saturday" |>> (fun _ -> Saturday)) <|>
    (pstr "sunday" |>> (fun _ -> Sunday))

let meal =
    (pstr "breakfast" |>> (fun _ -> Breakfast)) <|>
    (pstr "lunch" |>> (fun _ -> Lunch)) <|>
    (pstr "mid day" |>> (fun _ -> MidDay)) <|>
    (pstr "dinner" |>> (fun _ -> Dinner))  <|>
    (pstr "late night" |>> (fun _ -> LateNight)) 

let order =
    pseq
        (pleft day pws1)
        (meal)
        (fun (d, m) -> {day = d; meal = m})

let request = pmany1 (pad order)
  
let grammar = pleft request peof

let parse (input: string) : Request option =
    let i = prepare input
    match grammar i with
    | Success(ast, _) -> Some ast
    | Failure(_,_) -> None