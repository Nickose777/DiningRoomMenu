using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.DTO.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.DTO
{
    public class MenuDTO
    {
        public List<CategoryMenuDTO> Categories { get; set; }

        public MenuDTO()
        {
            Categories = new List<CategoryMenuDTO>();
        }
    }
}
