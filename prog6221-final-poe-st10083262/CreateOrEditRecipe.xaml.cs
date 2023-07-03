﻿using prog6221_final_poe_st10083262.Model;
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
            if (id == -1)
            {
                Recipe = new Recipe();
            }
            else
            {
                Recipe = RecipeDatabase.Instance.AllRecipes.FirstOrDefault(obj => obj.ID == id);
                RecipeNameTextBox.Text = Recipe.Name;

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

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            /*CreateIngredientWindow window = new CreateIngredientWindow();
            if (window.ShowDialog() == true)
            {
                Recipe.AddIngredient(window.Ingredient);
            }*/
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            /*CreateStepWindow window = new CreateStepWindow();
            if (window.ShowDialog() == true)
            {
                Recipe.AddStep(window.Step);
            }*/
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
                if (id == -1)
                {
                    Recipe.Name = RecipeNameTextBox.Text;
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
    }
}
