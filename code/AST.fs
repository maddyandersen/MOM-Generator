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

type Category =
| LeeBreakfast of string
| LeeLunch of string
| LeeMidDay of string
| LeeDinner of string
| FnGLunch of string
| GrillLunch of string
| GrillDinner of string
| GrillLateNight of string
| AnyCat

type Order = {day: Day; meal: Meal; location: Location}

type Request = Order list