using DiningRoomMenu.Logic.DTO.Recipe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.RecipeControls.ViewModels
{
    public class RecipeListViewModel : ObservableObject
    {
        public RecipeListViewModel(IEnumerable<RecipeDisplayDTO> recipes)
        {
            this.Recipes = new ObservableCollection<RecipeDisplayDTO>(recipes);
        }

        public ObservableCollection<RecipeDisplayDTO> Recipes { get; set; }
    }
}
