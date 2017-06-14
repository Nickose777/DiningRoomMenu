using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.DTO.Recipe
{
    public class RecipeDisplayDTO
    {
        public string Name { get; set; }

        public string DishName { get; set; }

        public string CategoryName { get; set; }

        public List<IngredientPortion> IngredientPortion { get; set; }

        public RecipeDisplayDTO()
        {
            IngredientPortion = new List<IngredientPortion>();
        }
    }
}
