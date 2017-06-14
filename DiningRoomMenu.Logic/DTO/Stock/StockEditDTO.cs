using System.Collections.Generic;

namespace DiningRoomMenu.Logic.DTO.Stock
{
    public class StockEditDTO
    {
        public int StockNo { get; set; }
        public List<IngredientCount> IngredientCount { get; set; }

        public StockEditDTO()
        {
            IngredientCount = new List<IngredientCount>();
        }
    }
}
