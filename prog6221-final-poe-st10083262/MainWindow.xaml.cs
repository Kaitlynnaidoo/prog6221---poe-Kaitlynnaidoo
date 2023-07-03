using prog6221_final_poe_st10083262.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prog6221_final_poe_st10083262
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class SearchBarData
    {
        public FOODGROUP? FoodGroup = null;
        public string? RecipeName = null;
        public int? MaximumCalories = null;
    }
    public partial class MainWindow : Window
    {

        SearchBarData searchBarData = new SearchBarData();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = RecipeDatabase.Instance;

        }

        private void RecipeNameSearch(object sender, TextChangedEventArgs e)
        {

            string str = recipeNameTextBox.Text;

            if (string.IsNullOrEmpty(str))
            {
                searchBarData.RecipeName = null;
                RecipeDatabase.Instance.SearchRecipes(searchBarData);
                return;
            }

            searchBarData.RecipeName = str;
            RecipeDatabase.Instance.SearchRecipes(searchBarData);
        }

        private void CaloriesSearch(object sender, TextChangedEventArgs e)
        {
            string name = maximumCaloriesSearchBox.Text;

            if (string.IsNullOrEmpty(name))
            {
                searchBarData.MaximumCalories = null;
                RecipeDatabase.Instance.SearchRecipes(searchBarData);
                return;
            }

            bool success = int.TryParse(name, out int number);
            if (success)
            {
                searchBarData.MaximumCalories = number;
                RecipeDatabase.Instance.SearchRecipes(searchBarData);
            }
            else
            {
                RecipeDatabase.Instance.SearchRecipes(searchBarData);
            }

        }

        private void RecipeFoodGroupSearch(object sender, SelectionChangedEventArgs e)
        {

            if (foodGroupBox.SelectedIndex != -1 && foodGroupBox.SelectedIndex != 0)
            {
                // Cast into to enum
                FOODGROUP searchFoodGroupTmp = (FOODGROUP)
                    Enum.GetValues(typeof(FOODGROUP))
                    .GetValue(foodGroupBox.SelectedIndex - 1);

                searchBarData.FoodGroup = searchFoodGroupTmp;
                RecipeDatabase.Instance.SearchRecipes(searchBarData);
            }
            else
            {
                searchBarData.FoodGroup = null;
                RecipeDatabase.Instance.SearchRecipes(searchBarData);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recipe recipe = button.DataContext as Recipe;
            if (recipe != null)
            {
                RecipeDatabase.Instance.DeleteRecipe(recipe);
            }
        }

        void Recipe_OnClick(object sender, MouseButtonEventArgs e)
        {
            Recipe recipe = ((ListViewItem)sender).Content as Recipe;

            if (recipe == null)
            {
                return;
            }
            else
            {
                CreateOrEditRecipe window = new CreateOrEditRecipe(recipe.ID);
                if (window.ShowDialog() == true)
                {
                    if (RecipeDatabase.Instance.AllRecipes.Count == window.Recipe.ID)
                    {
                        RecipeDatabase.Instance.AddRecipe(window.Recipe);
                    }
                    else
                    {
                        RecipeDatabase.Instance.UpdateRecipe(window.Recipe);
                    }
                }
            }
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {

            CreateOrEditRecipe window = new CreateOrEditRecipe(-1);
            if (window.ShowDialog() == true)
            {
                RecipeDatabase.Instance.AddRecipe(window.Recipe);
            }
        }

    }
}
