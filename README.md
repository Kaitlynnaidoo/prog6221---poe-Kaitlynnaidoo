# Recipe Application
This is a standalone recipe management application that allows users to create and manage recipes
through a command-line interface (CLI) and a graphical user interface (GUI). The application is built
using C# and Windows Presentation Foundation (WPF) and follows object-oriented programming
principles.
# Features
The Recipe Application offers the following features:
Part 1: Object-Oriented Programming (CLI)
1. Recipe Details: Users can enter the details for a single recipe, including the number of
ingredients, ingredient details (name, quantity, unit of measurement), the number of steps,
and step descriptions.
2. Display Recipe: The application displays the full recipe, including the ingredients and steps,
in a neat format.
3. Scaling: Users can request to scale the recipe by a factor of 0.5 (half), 2 (double), or 3
(triple), with ingredient quantities adjusted accordingly.
4. Reset Quantities: Users can reset the ingredient quantities to their original values.
5. Clear Data: Users can clear all entered data to enter a new recipe.
Part 2: Advanced C# Features (CLI)
6. Multiple Recipes: Users can enter an unlimited number of recipes, each with a unique name.
7. Recipe List: The application displays a list of all recipes in alphabetical order by name.
8. Recipe Selection: Users can choose a recipe from the list to view its details.
9. Extended Ingredient Details: Users can enter additional information for each ingredient,
including the number of calories and the food group.
10. Total Calories: The application calculates and displays the total calories of all the ingredients
in a recipe.
11. Calorie Notification: Users are notified when the total calories of a recipe exceed 300.
Part 3: WPF GUI (GUI)
12. Filtering: Users can filter the recipe list by entering an ingredient name, selecting a food
group, or setting a maximum number of calories.
# Prerequisites
 Windows operating system (for WPF GUI)
 .NET Framework or .NET Core runtime environment
# Getting Started
1. Clone or download the repository to your local machine.
2. Open the solution file (RecipeApplication.sln) in Visual Studio.
3. Build the solution to restore dependencies and compile the project.
# CLI Application
To run the CLI application:
1. Set the CLI project (RecipeApplication.CLI) as the startup project.
2. Press F5 or select "Start Debugging" to run the application.
# GUI Application
To run the GUI application:
1. Set the GUI project (RecipeApplication.GUI) as the startup project.
2. Press F5 or select "Start Debugging" to launch the application with the graphical interface.
Usage
CLI Application
1. Follow the prompts in the console to enter recipe details, view the recipe list, select a recipe,
and perform other actions.
2. Use the keyboard to input values and navigate the application.
GUI Application
1. In the GUI, navigate through the available screens using buttons and input fields.
2. Enter recipe details, filter the recipe list, select a recipe, and perform other actions using the
provided controls.
3. View recipe details, including ingredients, steps, and total calories, in a user-friendly manner.
4. Receive notifications if a recipe exceeds 300 calories.
# Contributing
Contributions to the Recipe Application are welcome! If you would like to contribute, please follow
these steps:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes with descriptive commit messages.
4. Push your branch to your forked repository.
5. Submit a pull request to the main repository.
Please ensure that your contributions align with the code style, conventions, and standards followed
in the proje
