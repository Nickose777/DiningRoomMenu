using DiningRoomMenu.Logic.DTO.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.DTO.Dish
{
    public class DishRecipeEditDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<IngredientPortion> Ingredients { get; set; }

        public DishRecipeEditDTO()
        {
            Ingredients = new List<IngredientPortion>();
        }
    }
}
