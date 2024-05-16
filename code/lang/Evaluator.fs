module Evaluator

open AST
open System
open System.IO

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

let filePathHelperMeal(m: Meal) : string = 
    match m with
    | Breakfast -> "breakfast"
    | Lunch -> "lunch"
    | MidDay -> "midday"
    | Dinner -> "dinner"
    | LateNight -> "latenight"

let filePathHelperLocation(l: Location) : string = 
    match l with
    | Lee -> "lee"
    | FnG -> "fng"
    | Grill -> "82grill"
    | _ -> failwith "not a valid location"


let filePathHelperCategory(c : string) : string =
    // can probably rename files in such a way that this is unnecessary
    // then would have something like:
    (* let updatedC = c.Replace(" ", "").ToLower()
    updatedC *)
    match c with 
    | "breakfast entrees" -> "entrees"
    | "breakfast sandwiches" -> "sandwiches"
    | "burgers" -> "burgers"
    | "hot sandwiches" -> "hot_sandwiches"
    | "GF burgers" -> "gf_burgers"
    | "GF hot sandwiches" -> "gf_hot_sandwiches"
    | "salads" -> "entree_salads"
    | "parfait" -> "parfait"
    | "specials" -> "specials"
    | "build your own" -> "byo"
    | "protein rich" -> "pr"
    | "GF" -> "gf"
    | "create your own" -> "cyo"
    | "wings" -> "wings"
    | "any" -> "any"
    | _ -> failwith "not a valid category"

let loadDayMealConstraintsFromFile(filePath: string) =
    let constraints = File.ReadAllLines(filePath) |> List.ofArray
    List.map (fun (line : string) ->
        match line.Split(',') with
        | [|loc; day; meal|] ->
            let loc' =
                match loc.ToLower().Trim() with
                | "lee" -> Lee
                | "fresh n go" -> FnG
                | "82 grill" -> Grill
                | _ -> failwith "Invalid location"

            let day' =
                match day.ToLower().Trim() with
                | "monday" -> Monday
                | "tuesday" -> Tuesday
                | "wednesday" -> Wednesday
                | "thursday" -> Thursday
                | "friday" -> Friday
                | "saturday" -> Saturday
                | "sunday" -> Sunday
                | _ -> failwith $"Invalid day: {day}"

            let meal' =
                match meal.ToLower().Trim() with
                | "breakfast" -> Breakfast
                | "lunch" -> Lunch
                | "midday" -> MidDay
                | "dinner" -> Dinner
                | "latenight" -> LateNight
                | _ -> failwith $"Invalid meal: {meal}"

            (loc', day', meal')
        | _ -> failwith "Invalid format in constraints file"
    ) constraints

let dayMealConstraints = loadDayMealConstraintsFromFile("../constraints/constraints.txt") 
let checkDayMealConstraints(order: Order) : bool =
    List.exists (fun (loc, day, meal) -> loc = order.location && day = order.day && meal = order.meal) dayMealConstraints

let getCategories(o: Order) =
    let filePath = 
        "../locations/" + 
        filePathHelperLocation(o.location) + "/" + 
        filePathHelperLocation(o.location) + "_" + 
        filePathHelperMeal(o.meal) + "/" + 
        filePathHelperMeal(o.meal) + "_categories.txt"
    let categories = File.ReadAllLines(filePath) |> List.ofArray
    categories

let getItems(o: Order) =
    let filePath = 
        "../locations/" + 
        filePathHelperLocation(o.location) + "/" + 
        filePathHelperLocation(o.location) + "_" + 
        filePathHelperMeal(o.meal) + "/" + 
        filePathHelperMeal(o.meal) + "_items/" + filePathHelperCategory(o.category) + ".txt"
    let items = File.ReadAllLines(filePath) |> List.ofArray
    items

let validateCategory(o: Order) : bool =
    let categories = getCategories(o)
    List.exists (fun category -> category = o.category) categories

let validateItem(o: Order) : bool =
    let items = getItems(o)
    List.exists (fun item -> item = o.item) items

let rec generateCategory(o: Order) =
    let random = new Random()
    let categories = getCategories(o)
    let index = random.Next(0, List.length categories)
    let category = List.item index categories
    category

let rec generateItem(o : Order) = 
    let random = new Random()
    let items = getItems(o)
    let index = random.Next(0, List.length items)
    let item = List.item index items
    item
    
let updateCategory newCategory order = { order with category = newCategory }

let evalHelper(r: Order) =
    if (checkDayMealConstraints r) then
        if (r.category = "any") then 
            let cat = generateCategory r
            let rUpdated = updateCategory cat r
            
            if (rUpdated.item <> "any") then
                sprintf "Item cannot be specified if category is not specified."
            else
                let item = generateItem rUpdated
                let prettyday = dayprint r.day
                let prettymeal = mealprint r.meal
                let prettylocation = locprint r.location
                sprintf "For %s at %s on %s, we recommend %s from the %s category." prettymeal prettylocation prettyday item rUpdated.category
        elif (validateCategory r) then
            if (r.item = "any") then
                let item = generateItem r
                let prettyday = dayprint r.day
                let prettymeal = mealprint r.meal
                let prettylocation = locprint r.location
                sprintf "For %s at %s on %s from the %s category, we recommend %s." prettymeal prettylocation prettyday r.category item
            elif (validateItem r) then
                let prettyday = dayprint r.day
                let prettymeal = mealprint r.meal
                let prettylocation = locprint r.location
                sprintf "%s from the %s category for %s at %s on %s is a great choice!" r.item r.category prettymeal prettylocation prettyday
            else 
                sprintf "Given item is not in given category."
        else 
            sprintf "Given category does not exist for given meal for given location."
    else
        sprintf "Given location is not open for given day or meal."
let rec eval(rs: Request) : string =
    match rs with
    | [] ->
        printfn "Please include at least one order of type <day> <meal> <location> <category> <item>."
        exit 1
    | [r] -> 
        evalHelper(r)
    | r::rs2 -> 
        printfn "%s" (evalHelper(r))
        eval rs2