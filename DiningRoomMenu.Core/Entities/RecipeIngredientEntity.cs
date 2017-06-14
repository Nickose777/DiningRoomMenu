using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Core.Entities
{
    public class RecipeIngredientEntity
    {
        public int RecipeId { get; set; }
        public virtual RecipeEntity Recipe { get; set; }

        public int IngredientId { get; set; }
        public virtual IngredientEntity Ingredient { get; set; }
    }
}
