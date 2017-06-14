using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Core.Entities
{
    public class StockIngredientEntity
    {
        public int StockId { get; set; }
        public virtual StockEntity Stock { get; set; }

        public int IngredientId { get; set; }
        public virtual IngredientEntity Ingredient { get; set; }
    }
}
