module Evaluator

open AST

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
        printfn "Please include at least one order of type <day> <meal>."
        exit 1
    | [r] -> 
        let prettyday = dayprint r.day
        let prettymeal = mealprint r.meal
        let prettylocation = locprint r.location
        sprintf "%s %s %s" prettyday prettymeal prettylocation
    | r::rs2 -> 
        let prettyday = dayprint r.day
        let prettymeal = mealprint r.meal
        let prettylocation = locprint r.location
        printfn "%s %s %s" prettyday prettymeal prettylocation     
        prettyprint rs2  

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
let rec generateCategories(rs: Request) : string =
    match rs with
    | [] ->
        printfn "Please include at least one order of type <day> <meal>."
        exit 1
    | [r] -> 
        sprintf "%s" (generateCategoriesHelper(r))
    | r::rs2 -> 
        printfn "%s" (generateCategoriesHelper(r))
        generateCategories rs2