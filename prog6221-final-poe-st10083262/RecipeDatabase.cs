using prog6221_final_poe_st10083262.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prog6221_final_poe_st10083262
{
    public class RecipeManager
    {
        private static RecipeManager _instance;
        private RecipeManager() {

            Recipe r1 = new Recipe(0, "Prawns");
            r1.AddIngredient(new Ingredient("Water", 1, "litres", 10, FOODGROUP.WATER));
            r1.AddIngredient(new Ingredient("Prawns", 1, "Kilograms", 1000, FOODGROUP.CHICKEN_FISH_MEAT_AND_EGGS));
            r1.AddStep("Add 1 litres of water to pan and boil it.");
            r1.AddStep("Place 1 Kilograms of prawns into the pan and cook until ready.");

            Recipe r2 = new Recipe(1, "Pancakes");
            r2.AddIngredient(new Ingredient("Flour", 100, "Grams", 250, FOODGROUP.STARCHY_FOODS));
            r2.AddIngredient(new Ingredient("Syrup", 100, "ml", 1000, FOODGROUP.FATS_AND_OIL));
            r2.AddIngredient(new Ingredient("Butter", 1, "stick", 1000, FOODGROUP.FATS_AND_OIL));
            r2.AddStep("Cook 100 Grams of Flour until it forms a disc");
            r2.AddStep("Squirt out 100 ml of Syrup ontop of the pancake batter.");
            r2.AddStep("If you want to, add 1 stick of Butter to the dish.s");

            Recipes.Add(r1);
            Recipes.Add(r2);
        }
        private ObservableCollection<Recipe> _recipes;
        private ObservableCollection<Recipe> _filteredRecipes;

        public static RecipeManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RecipeManager();
                return _instance;
            }
        }

        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set { _recipes = value; }
        }

        public ObservableCollection<Recipe> FilteredRecipes
        {
            get { return _filteredRecipes; }
            set { _filteredRecipes = value; }
        }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
            FilterRecipes();
        }

        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
            FilterRecipes();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            // Find the recipe in the list and update its properties
            Recipe existingRecipe = Recipes.FirstOrDefault(r => r.ID == recipe.ID);
            if (existingRecipe != null)
            {
                existingRecipe.Name = recipe.Name;
                existingRecipe.Ingredients = recipe.Ingredients;
                existingRecipe.Steps = recipe.Steps;
                existingRecipe.CurrentScale = recipe.CurrentScale;
                FilterRecipes();
            }
        }

        public void FilterRecipes(FOODGROUP? foodGroup = null, string? recipeName = null, int? maxCalories = null)
        {
            FilteredRecipes.Clear();

            if (foodGroup == null && recipeName == null && maxCalories == null)
            {
                // No filter criteria specified, add all recipes to the filtered list
                foreach (Recipe recipe in Recipes)
                {
                    FilteredRecipes.Add(recipe);
                }
            }
            else
            {
                // Filter the recipes based on the selected criteria
                foreach (Recipe recipe in Recipes)
                {
                    bool addRecipe = true;

                    if (foodGroup != null && !recipe.Ingredients.Any(i => i.FoodGroup == foodGroup))
                    {
                        addRecipe = false;
                    }

                    if (recipeName != null && !recipe.Name.Contains(recipeName))
                    {
                        addRecipe = false;
                    }

                    if (maxCalories != null && recipe.Calories > maxCalories)
                    {
                        addRecipe = false;
                    }

                    if (addRecipe)
                    {
                        FilteredRecipes.Add(recipe);
                    }
                }
            }
        }
    }
}
