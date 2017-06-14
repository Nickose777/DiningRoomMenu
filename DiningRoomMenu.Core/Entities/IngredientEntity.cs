using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Core.Entities
{
    public class IngredientEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<StockIngredientEntity> StockIngredients { get; set; }

        public virtual ICollection<RecipeIngredientEntity> RecipeIngredients { get; set; }
    }
}
