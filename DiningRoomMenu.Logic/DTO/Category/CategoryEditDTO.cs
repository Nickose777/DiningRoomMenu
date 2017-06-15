using DiningRoomMenu.Logic.DTO.Dish;
using System.Collections.Generic;

namespace DiningRoomMenu.Logic.DTO.Category
{
    public class CategoryEditDTO
    {
        public string OldName { get; set; }

        public string NewName { get; set; }

        public List<DishDisplayDTO> Dishes { get; set; }

        public CategoryEditDTO()
        {
            Dishes = new List<DishDisplayDTO>();
        }
    }
}
