using System.Collections.Generic;

namespace DiningRoomMenu.Logic.DTO.Stock
{
    public class StockEditDTO
    {
        public int OldStockNo { get; set; }

        public int NewStockNo { get; set; }

        public List<IngredientCount> IngredientCount { get; set; }

        public StockEditDTO()
        {
            IngredientCount = new List<IngredientCount>();
        }
    }
}
