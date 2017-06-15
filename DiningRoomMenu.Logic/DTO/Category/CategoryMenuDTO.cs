using DiningRoomMenu.Logic.DTO.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.DTO.Category
{
    public class CategoryMenuDTO
    {
        public string OldName { get; set; }

        public string NewName { get; set; }

        public List<DishMenuDTO> Dishes { get; set; }

        public CategoryMenuDTO()
        {
            Dishes = new List<DishMenuDTO>();
        }
    }
}
