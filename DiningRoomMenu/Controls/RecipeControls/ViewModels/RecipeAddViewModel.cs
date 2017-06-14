using DiningRoomMenu.Controls.IngredientControls.ViewModels;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Dish;
using DiningRoomMenu.Logic.DTO.Recipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.RecipeControls.ViewModels
{
    public class RecipeAddViewModel : ObservableObject
    {
        public event GenericEventHandler<RecipeAddDTO> RecipeAdded;

        private RecipeAddDTO recipe;

        private DishDisplayDTO dish;
        private IngredientPortion ingredientPortion;
        private string portion;

        public RecipeAddViewModel(IEnumerable<DishDisplayDTO> dishes, IngredientListViewModel ingredientsViewModel)
        {
            this.recipe = new RecipeAddDTO();

            this.SaveCommand = new DelegateCommand(Save, CanSave);
            this.RemoveCommand = new DelegateCommand(
                () => Ingredients.Remove(IngredientPortion), 
                obj => IngredientPortion != null);

            this.IngredientsViewModel = ingredientsViewModel;

            this.Dishes = new ObservableCollection<DishDisplayDTO>(dishes);
            this.Ingredients = new ObservableCollection<IngredientPortion>();

            ingredientsViewModel.IngredientSelected += (s, e) =>
            {
                string ingredientName = e.Data.Name;
                if (!Ingredients.Any(ingredientPortion => ingredientPortion.Ingredient == ingredientName))
                {
                    Ingredients.Add(new IngredientPortion { Ingredient = ingredientName, Portion = Portion });
                }

                Portion = String.Empty;
            };
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand RemoveCommand { get; private set; }

        public IngredientListViewModel IngredientsViewModel { get; private set; }

        public string Name
        {
            get { return recipe.Name; }
            set 
            {
                recipe.Name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public string Description
        {
            get { return recipe.Description; }
            set
            {
                recipe.Description = value;
                RaisePropertyChangedEvent("Description");
            }
        }

        public string Portion
        {
            get { return portion; }
            set
            {
                portion = value;
                RaisePropertyChangedEvent("Portion");
            }
        }

        public DishDisplayDTO Dish
        {
            get { return dish; }
            set
            {
                dish = value;
                RaisePropertyChangedEvent("Dish");
            }
        }

        public IngredientPortion IngredientPortion
        {
            get { return ingredientPortion; }
            set
            {
                ingredientPortion = value;
                RaisePropertyChangedEvent("IngredientPortion");
            }
        }

        public ObservableCollection<DishDisplayDTO> Dishes { get; private set; }

        public ObservableCollection<IngredientPortion> Ingredients { get; private set; }

        private void Save()
        {
            recipe.DishName = Dish.Name;
            recipe.Ingredients.AddRange(Ingredients);

            RaiseRecipeAddedEvent(recipe);
        }

        private bool CanSave(object obj)
        {
            return
                Dish != null &&
                !String.IsNullOrEmpty(Name);
        }

        private void RaiseRecipeAddedEvent(RecipeAddDTO dish)
        {
            var handler = RecipeAdded;
            if (handler != null)
            {
                GenericEventArgs<RecipeAddDTO> e = new GenericEventArgs<RecipeAddDTO>(dish);
                handler(this, e);
            }
        }
    }
}
