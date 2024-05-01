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

let rec prettyprint(es: Expr) : string =
    match es with
    | [] ->
        printfn "Please include at least one order of type <day> <meal>."
        exit 1
    | [e] -> 
        let prettyday = dayprint e.day
        let prettymeal = mealprint e.meal
        sprintf "%s %s" prettyday prettymeal
    | e::es2 -> 
        let prettyday = dayprint e.day
        let prettymeal = mealprint e.meal
        printfn "%s %s" prettyday prettymeal        
        prettyprint es2  