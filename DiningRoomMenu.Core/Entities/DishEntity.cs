using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Core.Entities
{
    public class DishEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Sum { get; set; }

        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }

        public virtual ICollection<RecipeEntity> Recipes { get; set; }
    }
}
