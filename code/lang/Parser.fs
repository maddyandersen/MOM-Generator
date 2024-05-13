module Parser

open AST
open Combinator

(*
<request>  ::= <order> | <order> '\n' <request>
<order>    ::= <day>␣<meal>␣<location>␣<category>
<day>      ::= monday | tuesday | wednesday | thursday | friday | saturday | sunday
<meal>     ::= breakfast | lunch | mid day | dinner | late night
<location> ::= lee | fng | grill | any
<category> ::= 
| <lee_breakfast_categories>
| <lee_midday_categories> 
    | <lee_lunch_categories> 
    | <lee_dinner_categories> 
    | <fng_lunch_categories> 
    | <grill_lunch_categories>   
    | <grill_dinner_categories> 
    | <grill_latenight_categories>

<lee_breakfast_categories> ::= breakfast entrees | breakfast sandwiches
<lee_lunch_categories> ::= burgers | hot sandwiches | breakfast sandwiches | GF burgers | GF hot sandwiches | salads | parfait | specials
<lee_midday_categories> ::= burgers | hot sandwiches | breakfast sandwiches | GF burgers | GF hot sandwiches | salads | parfait | specials
<lee_dinner_categories> ::= burgers | hot sandwiches | breakfast sandwiches | GF burgers | GF hot sandwiches | salads | parfait | specials

<fng_lunch_categories> ::= build your own | protein rich | GF

<grill_lunch_categories> ::= create your own | GF| wings | specials
<grill_dinner_categories> ::= create your own | GF | wings | specials
<grill_latenight_categories> ::= create your own
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

let location =
    (pstr "lee" |>> (fun _ -> Lee)) <|>
    (pstr "fresh n go" |>> (fun _ -> FnG)) <|>
    (pstr "82 grill" |>> (fun _ -> Grill)) <|>
    (pstr "any" |>> (fun _ -> AnyLoc))    

let order =
    pseq
        (pseq
            (pleft day pws1)
            (pleft meal pws1)
            (fun (d, m) -> (d, m)))
        location
        (fun ((d, m), l) -> { day = d; meal = m; location = l })  

let request = pmany1 (pad order)
  
let grammar = pleft request peof

let parse (input: string) : Request option =
    let i = prepare input
    match grammar i with
    | Success(ast, _) -> Some ast
    | Failure(_,_) -> None