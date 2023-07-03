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
    public class RecipeDatabase
    {
        private static RecipeDatabase _instance;

        private ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();
        private ObservableCollection<Recipe> _filteredRecipes = new ObservableCollection<Recipe>();

        public static RecipeDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecipeDatabase();
                    Recipe r1 = new Recipe(0, "Prawns");
                    r1.AddIngredient(new Ingredient("Water", 1, "litres", 10, FOODGROUP.WATER));
                    r1.AddIngredient(new Ingredient("Prawns", 1, "Kilograms", 1000, FOODGROUP.CHICKEN_FISH_MEAT_AND_EGGS));
                    r1.AddStep("Add 1 litres of Water to pan and boil it.");
                    r1.AddStep("Place 1 Kilograms of Prawns into the pan and cook until ready.");

                    Recipe r2 = new Recipe(1, "Pancakes");
                    r2.AddIngredient(new Ingredient("Flour", 100, "Grams", 250, FOODGROUP.STARCHY_FOODS));
                    r2.AddIngredient(new Ingredient("Syrup", 100, "ml", 1000, FOODGROUP.FATS_AND_OIL));
                    r2.AddIngredient(new Ingredient("Butter", 1, "stick", 1000, FOODGROUP.FATS_AND_OIL));
                    r2.AddStep("Cook 100 Grams of Flour until it forms a disc");
                    r2.AddStep("Squirt out 100 ml of Syrup ontop of the pancake batter.");
                    r2.AddStep("If you want to, add 1 stick of Butter to the dish.");

                    _instance.AllRecipes.Add(r1);
                    _instance.AllRecipes.Add(r2);
                }
                return _instance;
            }
        }

        private RecipeDatabase()
        {

        }

        public ObservableCollection<Recipe> AllRecipes
        {
            get { return _recipes; }
            set { _recipes = value; }
        }

        public ObservableCollection<Recipe> SearchResultsRecipes
        {
            get { return _filteredRecipes; }
            set { _filteredRecipes = value; }
        }

        public void AddRecipe(Recipe recipe)
        {
            AllRecipes.Add(recipe);
            // Re-add all recipes
            SearchRecipes(new SearchBarData());
        }

        public void DeleteRecipe(Recipe recipe)
        {
            AllRecipes.Remove(recipe);
            // Re-add all recipes
            SearchRecipes(new SearchBarData());
        }

        public void UpdateRecipe(Recipe recipe)
        {
            // Find the recipe
            Recipe existingRecipe = AllRecipes.FirstOrDefault(r => r.ID == recipe.ID);
            if (existingRecipe != null)
            {
                existingRecipe.Name = recipe.Name;
                existingRecipe.Ingredients = recipe.Ingredients;
                existingRecipe.Steps = recipe.Steps;
                existingRecipe.CurrentScale = recipe.CurrentScale;
                SearchRecipes(new SearchBarData());
            }
        }

        public void SearchRecipes(SearchBarData searchBar)
        {
            SearchResultsRecipes.Clear();

            if (searchBar.FoodGroup == null && searchBar.RecipeName == null && searchBar.MaximumCalories == null)
            {
                // Do not do any adding, just force refresh the list
                foreach (Recipe recipe in AllRecipes)
                {
                    SearchResultsRecipes.Add(recipe);
                }
            }
            else
            {
                // use the searchbar data to filter the recipes.
                foreach (Recipe recipe in AllRecipes)
                {
                    bool addRecipe = true;

                    if (searchBar.FoodGroup != null && !recipe.Ingredients.Any(i => i.FoodGroup == searchBar.FoodGroup))
                    {
                        addRecipe = false;
                    }

                    if (searchBar.RecipeName != null && !recipe.Name.Contains(searchBar.RecipeName))
                    {
                        addRecipe = false;
                    }

                    if (searchBar.MaximumCalories != null && recipe.Calories > searchBar.MaximumCalories)
                    {
                        addRecipe = false;
                    }

                    if (addRecipe)
                    {
                        SearchResultsRecipes.Add(recipe);
                    }
                }
            }
        }
    }
}
