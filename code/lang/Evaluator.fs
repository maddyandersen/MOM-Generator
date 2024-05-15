module Evaluator

open AST
open System

let dayprint(d: Day) : string =
    match d with
    | Monday -> "Monday"
    | Tuesday -> "Tuesday"
    | Wednesday -> "Wednesday"
    | Thursday -> "Thursday"
    | Friday -> "Friday"
    | Saturday -> "Saturday"
    | Sunday -> "Sunday"

let mealprint(m: Meal) : string =
    match m with
    | Breakfast -> "Breakfast"
    | Lunch -> "Lunch"
    | MidDay -> "Mid Day"
    | Dinner -> "Dinner"
    | LateNight -> "Late Night"

let locprint(l: Location) : string =
    match l with
    | Lee -> "Lee"
    | FnG -> "Fresh n Go"
    | Grill -> "82 Grill"
    | AnyLoc -> "Any Location"

let rec prettyprint(rs: Request) : string =
    match rs with
    | [] ->
        printfn "Please include at least one order of type <day> <meal> <location>."
        exit 1
    | [r] -> 
        let prettyday = dayprint r.day
        let prettymeal = mealprint r.meal
        let prettylocation = locprint r.location
        sprintf "%s %s %s %s" prettyday prettymeal prettylocation r.category
    | r::rs2 -> 
        let prettyday = dayprint r.day
        let prettymeal = mealprint r.meal
        let prettylocation = locprint r.location
        printfn "%s %s %s %s" prettyday prettymeal prettylocation r.category 
        prettyprint rs2  
(*
// update this to select category at random
let generateCategoriesHelper(o: Order) : string =
    match o.meal, o.location with 
    | Breakfast, Lee -> "\nrandom category from lee breakfast"
    | Lunch, Lee ->  "\nrandom category from lee lunch"
    | MidDay, Lee ->  "\nrandom category from lee mid day"
    | Dinner, Lee ->  "\nrandom category from lee dinner"
    | Lunch, FnG ->  "\nrandom category from fresh n go lunch"
    | Lunch, Grill ->  "\nrandom category from 82 grill lunch"
    | Dinner, Grill ->  "\nrandom category from 82 grill dinner"
    | LateNight, Grill -> "\nrandom category from 82 grill late night"
    | Breakfast, AnyLoc -> "\nrandom category from any breakfast"
    | Lunch, AnyLoc -> "\nrandom category from any lunch"
    | MidDay, AnyLoc -> "\nrandom category from any midday"
    | Dinner, AnyLoc -> "\nrandom category from any dinner"
    | LateNight, AnyLoc -> "\nrandom category from any latenight"
    | _, _ -> failwith "Location not available for given meal"
*)

let generateCategoriesHelper(o: Order) : string =
    let random = new Random()
    let getRandomCategory categories =
        let index = random.Next(0, List.length categories)
        List.item index categories

    match o.meal, o.location with 
    | Breakfast, Lee -> 
        sprintf "\nRandom category from Lee breakfast: %s" (getRandomCategory ["breakfast entrees"; "breakfast sandwiches"])
    | Lunch, Lee ->  
        sprintf "\nRandom category from Lee lunch: %s" (getRandomCategory ["burgers"; "hot sandwiches"; "breakfast sandwiches"; "GF burgers"; "GF hot sandwiches"; "salads"; "parfait"; "specials"])
    | MidDay, Lee ->  
        sprintf "\nRandom category from Lee midday: %s" (getRandomCategory ["burgers"; "hot sandwiches"; "breakfast sandwiches"; "GF burgers"; "GF hot sandwiches"; "salads"; "parfait"; "specials"])
    | Dinner, Lee ->  
        sprintf "\nRandom category from Lee dinner: %s" (getRandomCategory ["burgers"; "hot sandwiches"; "breakfast sandwiches"; "GF burgers"; "GF hot sandwiches"; "salads"; "parfait"; "specials"])
    | LateNight, Lee -> 
        sprintf "Lee does not have late night. Please request a new order."
    | Lunch, FnG ->  
        sprintf "\nRandom category from Fresh n Go lunch: %s" (getRandomCategory ["build your own"; "protein rich"; "GF"])
    | Breakfast, FnG -> 
        sprintf "Fresh n Go does not have breakfast. Please request a new order"
    | Dinner, FnG ->
        sprintf "Fresh n Go does not have dinner. Please request a new order."
    | MidDay, FnG ->
        sprintf "Fresh n Go does not have mid day. Please request a new order."
    | LateNight, FnG ->
        sprintf "Fresh n Go does not have late night. Please request a new order."
    | Breakfast, Grill -> 
        sprintf "82 Grill does not have breakfast. Please request a new order"
    | Lunch, Grill ->  
        sprintf "\nRandom category from 82 Grill lunch: %s" (getRandomCategory ["create your own"; "GF"; "wings"; "specials"])
    | Dinner, Grill ->  
        sprintf "\nRandom category from 82 Grill dinner: %s" (getRandomCategory ["create your own"; "GF"; "wings"; "specials"])
    | LateNight, Grill -> 
        sprintf "\nRandom category from 82 Grill late night: %s" (getRandomCategory ["create your own"])
    | Breakfast, AnyLoc ->  
        sprintf "\nRandom category from any breakfast: %s" (getRandomCategory ["breakfast entrees"; "breakfast sandwiches"])
    | Lunch, AnyLoc ->  
        sprintf "\nRandom category from any lunch: %s" (getRandomCategory ["burgers"; "hot sandwiches"; "breakfast sandwiches"; "GF burgers"; "GF hot sandwiches"; "salads"; "parfait"; "specials"; "build your own"; "protein rich"; "GF"; "create your own"; "wings"; "specials"])
    | MidDay, AnyLoc ->  
        sprintf "\nRandom category from any midday: %s" (getRandomCategory ["burgers"; "hot sandwiches"; "breakfast sandwiches"; "GF burgers"; "GF hot sandwiches"; "salads"; "parfait"; "specials"; "build your own"; "protein rich"; "GF"; "create your own"; "wings"; "specials"])
    | Dinner, AnyLoc ->  
        sprintf "\nRandom category from any dinner: %s" (getRandomCategory ["burgers"; "hot sandwiches"; "breakfast sandwiches"; "GF burgers"; "GF hot sandwiches"; "salads"; "parfait"; "specials"; "create your own"; "GF"; "wings"; "specials"])
    | LateNight, AnyLoc ->  
        sprintf "\nRandom category from any late night: %s" (getRandomCategory ["create your own"])
    | _, _ -> failwith "Location not available for given meal"
let rec generateCategories(rs: Request) : string =
    match rs with
    | [] ->
        printfn "Please include at least one order of type <day> <meal> <location>."
        exit 1
    | [r] -> 
        sprintf "%s" (generateCategoriesHelper(r))
    | r::rs2 -> 
        printfn "%s" (generateCategoriesHelper(r))
        generateCategories rs2