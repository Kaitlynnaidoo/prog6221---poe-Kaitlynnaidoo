using prog6221_final_poe_st10083262.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prog6221_final_poe_st10083262
{
    /// <summary>
    /// Interaction logic for CreateOrEditRecipe.xaml
    /// </summary>
    public partial class CreateOrEditRecipe : Window
    {
        public Recipe Recipe { get; private set; }

        private int id;
        public CreateOrEditRecipe(int id)
        {
            InitializeComponent();

            this.id = id;

            // if this is a new Recipe.
            if (id == -1)
            {
                Recipe = new Recipe();
                BackButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                Recipe = RecipeDatabase.Instance.AllRecipes.FirstOrDefault(obj => obj.ID == id);
                RecipeNameTextBox.Text = Recipe.Name;

                // Do not show the buttons as it saves in real time, in a viewmodel.

                BackButton.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Collapsed;

            }

            DataContext = Recipe;
        }

        private void DeleteStepButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string step = button.DataContext as string;
            if (step != null)
            {
                Recipe.RemoveStep(step);
            }
        }

        private void DeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Ingredient ingredient = button.DataContext as Ingredient;
            if (ingredient != null)
            {
                Recipe.DeleteIngredient(ingredient);
            }
        }

        private void HalfScaleButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe.CurrentScale = SCALING.Half;
        }

        private void DoubleScaleButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe.CurrentScale = SCALING.Double;
        }

        private void TripleScaleButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe.CurrentScale = SCALING.Triple;
        }

        private void ResetScaleButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe.CurrentScale = SCALING.RESET;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RecipeNameTextBox.Text))
            {
                ErrorTextBlock.Text = "The Recipe Name cannot be empty.";
            }
            else
            {

                // If this is a new Recipe
                if (id == -1)
                {
                    Recipe.Name = RecipeNameTextBox.Text;
                    // Check for the highest used id and then increment it.
                    var max_value = RecipeDatabase.Instance.AllRecipes.Max(x => x.ID);
                    Recipe.ID = max_value + 1;
                    DialogResult = true;
                }
                else
                {
                    Recipe.Name = RecipeNameTextBox.Text;
                    DialogResult = true;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {

            bool isError = false;
            string error = "";

            string name = IngredientNameTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                error += "Name cannot be empty. ";
                isError = true;
            }

            string quantityStr = QuantityTextBox.Text;
            double quantity;
            if (!Double.TryParse(quantityStr, out quantity))
            {
                error += "The quantity is not valid. ";
                isError = true;
            }

            string unitOfMeasurement = UnitOfMeasurementTextBox.Text;
            if (string.IsNullOrEmpty(unitOfMeasurement))
            {
                error += "The unit of measurement cannot be empty. ";
                isError = true;
            }

            string caloriesStr = CaloriesMeasurementTextBox.Text;
            int calories;
            if (!int.TryParse(caloriesStr, out calories))
            {
                error += "The number of calories is not valid whole number. ";
                isError = true;
            }

            FOODGROUP foodGroup = FOODGROUP.WATER;
            if (Enum.IsDefined(typeof(FOODGROUP), FoodGroupComboBox.SelectedIndex))
            {
                foodGroup = (FOODGROUP)FoodGroupComboBox.SelectedIndex;

            }
            else
            {
                error += "The selected food group is not valid. ";
                isError = true;
            }

            if (!isError)
            {
                Recipe.AddIngredient(new Ingredient(name, quantity, unitOfMeasurement, calories, foodGroup));
            }
            else
            {
                IngredientErrorTextBox.Text = error;
            }

            isError = false;

        }

        private void SaveStepButton_Click(object sender, RoutedEventArgs e)
        {
            string error = "";

            if (string.IsNullOrEmpty(StepTextBox.Text))
            {
                error = "The step cannot be empty.";
            }
            else
            {
                Recipe.AddStep(StepTextBox.Text);
            }

            StepErrorTextBox.Text = error;
        }
    }
}
