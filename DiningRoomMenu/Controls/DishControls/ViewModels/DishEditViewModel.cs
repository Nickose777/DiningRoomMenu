using DiningRoomMenu.Controls.RecipeControls.ViewModels;
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

namespace DiningRoomMenu.Controls.DishControls.ViewModels
{
    public class DishEditViewModel : ObservableObject
    {
        public event GenericEventHandler<DishEditDTO> DishSaveRequest;
        public event GenericEventHandler<DishEditDTO> DishSaveRecipesRequest;
        public event GenericEventHandler<DishEditDTO> DishDeleteRequest;

        private readonly DishEditDTO dish;
        private DishRecipeEditDTO recipe;

        public DishEditViewModel(DishEditDTO dish, RecipeAddViewModel viewModel)
        {
            this.dish = dish;
            this.RecipeAddViewModel = viewModel;

            this.RemoveRecipeCommand = new DelegateCommand(
                () => Recipes.Remove(Recipe),
                obj => Recipe != null
                );
            this.SaveRecipesCommand = new DelegateCommand(
                () => Save(dish),
                obj => CanSave()
                );
            this.SaveCommand = new DelegateCommand(
                () => RaiseDishSaveRequestEvent(dish),
                obj => CanSave()
                );
            this.DeleteCommand = new DelegateCommand(() => RaiseDishDeleteRequestEvent(dish));

            this.Recipes = new ObservableCollection<DishRecipeEditDTO>(dish.Recipes);

            viewModel.RecipeAdded += (s, e) =>
            {
                var addedRecipe = e.Data;
                if (!Recipes.Select(recipe => recipe.Name).Contains(addedRecipe.Name))
                {
                    DishRecipeEditDTO recipe = new DishRecipeEditDTO
                    {
                        Name = addedRecipe.Name,
                        Description = addedRecipe.Description
                    };
                    recipe.Ingredients.AddRange(addedRecipe.Ingredients);

                    Recipes.Add(recipe);
                }
            };
        }

        public ICommand RemoveRecipeCommand { get; private set; }

        public ICommand SaveRecipesCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public DishRecipeEditDTO Recipe
        {
            get { return recipe; }
            set
            {
                recipe = value;
                RaisePropertyChangedEvent("Recipe");
            }
        }

        public RecipeAddViewModel RecipeAddViewModel { get; set; }

        public string Name
        {
            get { return dish.NewName; }
            set
            {
                dish.NewName = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public decimal Price
        {
            get { return dish.Price; }
            set
            {
                dish.Price = value;
                RaisePropertyChangedEvent("Price");
            }
        }

        public ObservableCollection<DishRecipeEditDTO> Recipes { get; set; }

        private void Save(DishEditDTO dish)
        {
            dish.Recipes.Clear();
            dish.Recipes.AddRange(Recipes);
            RaiseDishSaveRecipesRequestEvent(dish);
        }

        private bool CanSave()
        {
            return
                !String.IsNullOrEmpty(Name) &&
                Price >= 0;
        }

        private void RaiseDishSaveRequestEvent(DishEditDTO dish)
        {
            var handler = DishSaveRequest;
            if (handler != null)
            {
                GenericEventArgs<DishEditDTO> e = new GenericEventArgs<DishEditDTO>(dish);
                handler(this, e);
            }
        }

        private void RaiseDishSaveRecipesRequestEvent(DishEditDTO dish)
        {
            var handler = DishSaveRecipesRequest;
            if (handler != null)
            {
                GenericEventArgs<DishEditDTO> e = new GenericEventArgs<DishEditDTO>(dish);
                handler(this, e);
            }
        }

        private void RaiseDishDeleteRequestEvent(DishEditDTO dish)
        {
            var handler = DishDeleteRequest;
            if (handler != null)
            {
                GenericEventArgs<DishEditDTO> e = new GenericEventArgs<DishEditDTO>(dish);
                handler(this, e);
            }
        }
    }
}
