using System.Collections.Generic;

namespace DiningRoomMenu.Logic.DTO.Ingredient
{
    public class IngredientEditDTO
    {
        public string OldName { get; set; }

        public string NewName { get; set; }

        public List<int> StocksAvailable { get; set; }

        public IngredientEditDTO()
        {
            StocksAvailable = new List<int>();
        }
    }
}
