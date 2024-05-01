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

type Order = {day: Day; meal: Meal}

type Expr = Order list