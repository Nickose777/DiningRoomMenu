using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Core.Entities
{
    public class StockEntity
    {
        public int Id { get; set; }

        public int StockNo { get; set; }

        public virtual ICollection<StockIngredientEntity> StockIngredients { get; set; }
    }
}
