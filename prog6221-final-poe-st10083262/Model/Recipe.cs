using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog6221_final_poe_st10083262.Model
{
    public enum SCALING
    {
        Half,
        RESET,
        Double,
        Triple
    }

    public class Recipe : INotifyPropertyChanged
    {
        private int recipeId;
        private string recipeName = "";
        private ObservableCollection<Ingredient> ingredients;
        private ObservableCollection<string> steps;
        private SCALING currentScale = SCALING.RESET;
        private ReadOnlyDictionary<SCALING, double> scalingValues = new ReadOnlyDictionary<SCALING, double>(new Dictionary<SCALING, double>
        {
            { SCALING.Half, 0.5 },
            { SCALING.RESET, 1.0 },
            { SCALING.Double, 2.0 },
            { SCALING.Triple, 3.0 }
        });


        public int ID
        {

            get { return recipeId; }
            set { recipeId = value; }
        }

        public string Name
        {
            get { return recipeName; }
            set
            {
                if (recipeName != value)
                {
                    recipeName = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Calories
        {
            get
            {
                return Ingredients.Sum(i => i.Calories);
            }
        }

        public ObservableCollection<string> Steps
        {
            get { return steps; }
            set
            {
                if (steps != value)
                {
                    steps = value;
                    OnPropertyChanged(nameof(Steps));
                }
            }
        }

        public SCALING CurrentScale
        {
            get { return currentScale; }
            set
            {
                if (currentScale != value)
                {

                    // Search the steps for ingredients and scale those.
                    for (int i = 0; i < Steps.Count; i++)
                    {
                        string step = Steps[i];
                        foreach (Ingredient ingredient in Ingredients)
                        {
                            step = step.Replace($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} of {ingredient.Name}", $"{ingredient.Quantity / scalingValues[currentScale] * scalingValues[value]} {ingredient.UnitOfMeasurement} of {ingredient.Name}");
                        }
                        Steps[i] = step;
                    }

                    // Scale the Ingredients
                    foreach (Ingredient ingredient in Ingredients)
                    {

                        //The first divide removes all scaling and the second applys the new scaling
                        // This ensures that we dont scale an already scaled value.

                        ingredient.Quantity /= scalingValues[currentScale];
                        ingredient.Calories /= (int)scalingValues[currentScale];
                        ingredient.Quantity *= scalingValues[value];
                        ingredient.Calories *= (int)scalingValues[currentScale];
                    }

                    currentScale = value;

                    OnPropertyChanged(nameof(CurrentScale));
                    OnPropertyChanged(nameof(Steps));
                    OnPropertyChanged(nameof(Calories));
                    OnPropertyChanged(nameof(Ingredients));

                }
            }
        }

        public Recipe()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            Steps = new ObservableCollection<string>();
            CurrentScale = SCALING.RESET;
        }

        public Recipe(int id, string name)
        {
            ID = id;
            Name = name;
            Ingredients = new ObservableCollection<Ingredient>();
            Steps = new ObservableCollection<string>();
            CurrentScale = SCALING.RESET;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);


            // Check all steps for if they contain this ingredient and then scale to current proportions.

            for (int i = 0; i < Steps.Count; i++)
            {
                Steps[i] = Steps[i].Replace($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} of {ingredient.Name}", $"{ingredient.Quantity * scalingValues[CurrentScale]} {ingredient.UnitOfMeasurement} of {ingredient.Name}");
            }

            // Scale the ingredient quantity
            ingredient.Quantity *= scalingValues[currentScale];
            ingredient.Calories *= (int)scalingValues[currentScale];
            OnPropertyChanged(nameof(Ingredients));
            OnPropertyChanged(nameof(Calories));
        }
        public void AddStep(string step)
        {
            // Scale the step
            foreach (Ingredient ingredient in Ingredients)
            {
                step = step.Replace($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} of {ingredient.Name}", $"{ingredient.Quantity * scalingValues[CurrentScale]} {ingredient.UnitOfMeasurement} of {ingredient.Name}");
            }
            Steps.Add(step);
            OnPropertyChanged(nameof(Steps));
        }

        public void RemoveStep(string step)
        {
            Steps.Remove(step);
            OnPropertyChanged(nameof(Steps));
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
            OnPropertyChanged(nameof(Ingredients));
            OnPropertyChanged(nameof(Calories));
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    OnPropertyChanged(nameof(Ingredients));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


