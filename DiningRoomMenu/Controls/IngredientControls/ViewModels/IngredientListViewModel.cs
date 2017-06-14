using DiningRoomMenu.Logic.DTO.Ingredient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.IngredientControls.ViewModels
{
    public class IngredientListViewModel : ObservableObject
    {
        public IngredientListViewModel(IEnumerable<IngredientDisplayDTO> ingredients)
        {
            this.Ingredients = new ObservableCollection<IngredientDisplayDTO>(ingredients);
        }

        public ObservableCollection<IngredientDisplayDTO> Ingredients { get; set; }
    }
}
