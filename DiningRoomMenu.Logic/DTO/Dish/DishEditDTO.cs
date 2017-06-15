using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.DTO.Dish
{
    public class DishEditDTO
    {
        public string OldName { get; set; }

        public string NewName { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public List<DishRecipeEditDTO> Recipes { get; set; }

        public DishEditDTO()
        {
            Recipes = new List<DishRecipeEditDTO>();
        }
    }
}
