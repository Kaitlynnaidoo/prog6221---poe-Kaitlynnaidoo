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
    public partial class MainWindow : Window
    {
        // Sort params
        FOODGROUP? sortFoodGroup = null;
        string? sortRecipeName = null;
        int? sortMaxCalories = null;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = Repo.Instance;

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string str = SearchTextBox.Text;

            if (string.IsNullOrEmpty(str))
            {
                sortRecipeName = null;
                Repo.Instance.FilterRecipes(sortFoodGroup, sortRecipeName, sortMaxCalories);
                return;
            }

            sortRecipeName = str;
            Repo.Instance.FilterRecipes(sortFoodGroup, sortRecipeName, sortMaxCalories);
        }

        private void CaloriesSortBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = MaxCaloriesTextBox.Text;

            if (string.IsNullOrEmpty(str))
            {
                sortMaxCalories = null;
                Repo.Instance.FilterRecipes(sortFoodGroup, sortRecipeName, sortMaxCalories);
                return;
            }

            bool success = int.TryParse(str, out int number);
            if (success)
            {
                sortMaxCalories = number;
                Repo.Instance.FilterRecipes(sortFoodGroup, sortRecipeName, sortMaxCalories);
            }
            else
            {
                Repo.Instance.FilterRecipes(sortFoodGroup, sortRecipeName, sortMaxCalories);
            }

        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SortBox.SelectedIndex != -1 && SortBox.SelectedIndex != 0)
            {
                FoodGroup selectedFoodGroup = (FoodGroup)Enum.GetValues(typeof(FoodGroup)).GetValue(SortBox.SelectedIndex - 1);
                // Use the selectedFoodGroup enum value as needed
                Console.WriteLine(selectedFoodGroup);
                sortFoodGroup = selectedFoodGroup;
                Repo.Instance.FilterRecipes(sortFoodGroup, sortRecipeName, sortMaxCalories);
            }
            else
            {
                sortFoodGroup = null;
                Repo.Instance.FilterRecipes(sortFoodGroup, sortRecipeName, sortMaxCalories);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recipe recipe = button.DataContext as Recipe;
            if (recipe != null)
            {
                Repo.Instance.RemoveRecipe(recipe);
            }
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {

            RecipeWindow window = new RecipeWindow(-1);
            if (window.ShowDialog() == true)
            {
                Repo.Instance.AddRecipe(window.Recipe);
            }
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
                RecipeWindow window = new RecipeWindow(recipe.ID);
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
                }
            }
        }
    
}
}
