open Parser
open Evaluator
open System.IO

[<EntryPoint>]
let main argv : int =
    (* check for proper usage *)
    if argv.Length > 1 then
        printfn "Usage: dotnet run"
        exit 1

    (* determine the file to read *)
    let file = 
        if argv.Length = 1 then
            argv[0]
        else
            (* print instructions *)
            let instructions = """
Welcome to the Mobile Order Meal (aka M.O.M.) Generator!

Please provide a file containing your request. A request consists of one or more orders. Each order should be placed on a new line and should take the form <day>,<meal>,<location>,<category>,<item>. If you want an order to be gluten-free, please include the gluten-free tag at the end of the order.

For <day>, please choose either Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, or Sunday.
For <meal>, please choose either breakfast, lunch, midday, dinner, or latenight.
For <location>, please choose Lee, Fresh n Go, 82 Grill, or any.

Here are the options for categories and items:

Breakfast from Lee:
• breakfast entrees: rise & shine, create your own omelet, yogurt & fresh berry parfait
• breakfast sandwiches: bagel supreme, mcwilliams, sausage egg & cheese biscuit
• gf: rise & shine, create your own omelet, yogurt & fresh berry parfait

Lunch, Midday, and Dinner from Lee:
• breakfast sandwiches: bagel supreme, mcwilliams, sausage egg & cheese biscuit
• burgers: burger
• gf: yogurt & fresh berry parfait, garden salad, grilled chicken salad, burger, grilled chicken breast, tuna melt, grilled cheese
• hot sandwiches: tuna melt, grilled cheese, grilled chicken breast, fried chicken, blt club, chicken snack wrap
• parfait: yogurt & fresh berry parfait
• salads: garden salad, grilled chicken salad, crispy chicken salad
• specials: deluxe disco fries, spinach caprese grilled cheese

Lunch from Fresh n Go:
• build your own: deli sandwich, salad, grain bowl, kosher sandwich
• protein rich: grain bowl: PR, salad: PR
• gf: deli sandwich, salad, grain bowl

Lunch and Dinner from 82 Grill:
• create your own: brick oven pizza, pasta bowl, mac & cheese bowl, nachos, toasted sandwich
• wings: asian style sesame wings, garlic parmesan wings, buffalo hot wings, plain wings
• specials: pickle pizza
• gf: brick oven pizza, pasta bowl, nachos

Latenight from 82 Grill: 
• create your own: brick oven pizza, pasta bowl, mac & cheese bowl, nachos
• gf: brick oven pizza, pasta bowl, nachos
"""
            printfn "%s" instructions
            printf "Please provide the path to the file containing your request: "
            System.Console.ReadLine()


    try
        (* read in the input file *)
        let input = File.ReadAllText file

        (* try to parse what they gave us *)
        let ast_maybe = parse input

        (* try to evaluate what we parsed... or not *)
        match ast_maybe with
        | Some ast ->
            let str = eval ast
            printfn "%s" str 
            0
        | None     ->
            printfn "Invalid program. Please review the syntax of your request."
            1

    with
    | :? FileNotFoundException ->
        printfn "Cannot find the file '%s'." file
        exit 1
    | e ->
        printfn "An error occurred: %s" e.Message
        exit 1
    