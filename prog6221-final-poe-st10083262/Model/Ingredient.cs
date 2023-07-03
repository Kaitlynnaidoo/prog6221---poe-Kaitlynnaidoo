using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog6221_final_poe_st10083262.Model
{
    public enum FOODGROUP
    {
        STARCHY_FOODS,
        VEGETABLES_AND_FRUITS,
        DRY_BEANS_PEAS_LENTILS_AND_SOYA,
        CHICKEN_FISH_MEAT_AND_EGGS,
        MILK_AND_DAIRY_PRODUCTS,
        FATS_AND_OIL,
        WATER
    }

    // Implement INotifyPropertyChanged to get real time ui changes when shown in a list.
    public class Ingredient : INotifyPropertyChanged
    {
        private string ingredientName;
        private double quantity;
        private string unitOfMeasurement;
        private double calories;
        private FOODGROUP foodGroup;


        public double Calories
        {
            get { return calories; }
            set
            {
                if (calories != value)
                {
                    calories = value;
                    OnPropertyChanged(nameof(Calories));
                }
            }
        }

        public string Name
        {
            get { return ingredientName; }
            set
            {
                if (ingredientName != value)
                {
                    ingredientName = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public double Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public string UnitOfMeasurement
        {
            get { return unitOfMeasurement; }
            set
            {
                if (unitOfMeasurement != value)
                {
                    unitOfMeasurement = value;
                    OnPropertyChanged(nameof(UnitOfMeasurement));
                }
            }
        }

        public FOODGROUP FoodGroup
        {
            get { return foodGroup; }
            set
            {
                if (foodGroup != value)
                {
                    foodGroup = value;
                    OnPropertyChanged(nameof(FoodGroup));
                }
            }
        }

        public Ingredient(string name, double quantity, string unitOfMeasurement, int calories, FOODGROUP foodGroup)
        {
            Name = name;
            Quantity = quantity;
            UnitOfMeasurement = unitOfMeasurement;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        public override string ToString()
        {
            return $"{Quantity} {UnitOfMeasurement} Of {Name}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
