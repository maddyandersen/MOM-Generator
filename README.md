# M.O.M. Generator

The **M.O.M. Generator** is a domain-specific language that helps students at Williams College decide what to order from campus mobile ordering options: Lee Snack Bar, 82 Grill, or Fresh n Go. 
Users provide input specifying the day, meal, location, category, and item preferences, and the system outputs personalized meal suggestions, optionally considering gluten-free options.

The generator was develiped as the final project for CS 334: Principles of Programming Languages. It was built by Maddy Andersen and Stella Oh.

---

## Getting Started

To use the M.O.M. Generator, you need:

- .NET installed on your system.
- A text file containing one or more orders following the M.O.M. Generator syntax.

Each order consists of five required components, separated by commas:
`<day>,<meal>,<location>,<category>,<item>[,gluten-free]`

- `day` – Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday  
- `meal` – Breakfast, Lunch, MidDay, Dinner, LateNight  
- `location` – Lee, FnG, Grill, AnyLoc  
- `category` – Menu category (e.g., "breakfast sandwiches", "salads", "burgers", "any")  
- `item` – Specific menu item (e.g., "bagel supreme", "burger", "salad")  
- `gluten-free` – Optional tag to request a gluten-free item

---

## Example Programs

Sample example programs are included in the `code/examples` directory:

1. **example1.txt** – A fully specified order.  
2. **example2.txt** – Demonstrates the `any` keyword for randomized suggestions.  
3. **example3.txt** – Shows the optional `gluten-free` tag.  
4. **example4.txt** – Demonstrates invalid orders and error messages.

From the project root directory, run the following command to execute an example:
`dotnet run ../examples/<example#.txt>`

---

## Running the M.O.M. Generator

From the project root directory, run:
`dotnet run <file.txt>`

Here, `<file.txt>` can be any text file that follows the M.O.M. Generator order specifications

The output is printed to the terminal with suggested meals for each order.

---

## Language Specification

For the full specification, including formal syntax, semantics, and design principles, please refer to the [**M.O.M. Generator Specification**](https://github.com/maddyandersen/MOM-Generator/blob/main/docs/specification.pdf).


