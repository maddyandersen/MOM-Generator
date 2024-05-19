module AST

type Day =
| Monday
| Tuesday
| Wednesday
| Thursday
| Friday
| Saturday
| Sunday

type Meal =
| Breakfast
| Lunch
| MidDay
| Dinner
| LateNight

type Location =
| Lee
| FnG
| Grill
| AnyLoc

type isGlutenFree =
| True
| False

type Order = {
    day: Day; 
    meal: Meal; 
    location: Location; 
    category: string; 
    item: string;
    isGlutenFree: isGlutenFree;
}

type Request = Order list