using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Ingredient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.IngredientControls.ViewModels
{
    public class IngredientListViewModel : ObservableObject
    {
        public event GenericEventHandler<IngredientDisplayDTO> IngredientSelected;

        private IngredientDisplayDTO ingredient;

        public IngredientListViewModel(IEnumerable<IngredientDisplayDTO> ingredients)
        {
            this.SelectCommand = new DelegateCommand(
                () => RaiseIngredientSelectedEvent(ingredient),
                obj => Ingredient != null);

            this.Ingredients = new ObservableCollection<IngredientDisplayDTO>(ingredients);
        }

        public ICommand SelectCommand { get; private set; }

        public IngredientDisplayDTO Ingredient
        {
            get { return ingredient; }
            set
            {
                ingredient = value;
                RaisePropertyChangedEvent("Ingredient");
            }
        }

        public ObservableCollection<IngredientDisplayDTO> Ingredients { get; set; }

        private void RaiseIngredientSelectedEvent(IngredientDisplayDTO ingredient)
        {
            var handler = IngredientSelected;
            if (handler != null)
            {
                GenericEventArgs<IngredientDisplayDTO> e = new GenericEventArgs<IngredientDisplayDTO>(ingredient);
                handler(this, e);
            }
        }
    }
}
