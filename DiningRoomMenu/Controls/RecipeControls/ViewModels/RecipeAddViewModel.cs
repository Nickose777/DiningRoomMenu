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

        public RecipeAddViewModel(IEnumerable<DishDisplayDTO> dishes)
        {
            this.recipe = new RecipeAddDTO();

            this.SaveCommand = new DelegateCommand(Save, CanSave);

            this.Dishes = new ObservableCollection<DishDisplayDTO>(dishes);
        }

        public ICommand SaveCommand { get; private set; }

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

        public DishDisplayDTO Dish
        {
            get { return dish; }
            set
            {
                dish = value;
                RaisePropertyChangedEvent("Dish");
            }
        }

        public ObservableCollection<DishDisplayDTO> Dishes { get; private set; }

        private void Save()
        {
            recipe.DishName = Dish.Name;
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
