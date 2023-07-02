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

            string str = SearchTextBox.Text;

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
            string str = MaxCaloriesTextBox.Text;

            if (string.IsNullOrEmpty(str))
            {
                searchBarData.MaximumCalories = null;
                RecipeDatabase.Instance.SearchRecipes(searchBarData);
                return;
            }

            bool success = int.TryParse(str, out int number);
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

            if (SortBox.SelectedIndex != -1 && SortBox.SelectedIndex != 0)
            {
                FOODGROUP selectedFoodGroup = (FOODGROUP)Enum.GetValues(typeof(FOODGROUP)).GetValue(SortBox.SelectedIndex - 1);
                // Use the selectedFoodGroup enum value as needed
                Console.WriteLine(selectedFoodGroup);
                searchBarData.FoodGroup = selectedFoodGroup;
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

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {

            /*RecipeWindow window = new RecipeWindow(-1);
            if (window.ShowDialog() == true)
            {
                Repo.Instance.AddRecipe(window.Recipe);
            }*/
        }

        void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Recipe recipe = ((ListViewItem)sender).Content as Recipe;

            if (recipe == null)
            {
                return;
            }
            else
            {
               /* RecipeWindow window = new RecipeWindow(recipe.ID);
                if (window.ShowDialog() == true)
                {
                    if (Repo.Instance.Recipes.Count == window.Recipe.ID)
                    {
                        Repo.Instance.AddRecipe(window.Recipe);
                    }
                    else
                    {
                        Repo.Instance.UpdateRecipe(window.Recipe);
                    }
                }*/
            }
        }
    
    }
}
