using System.Collections.Generic;

namespace DiningRoomMenu.Logic.DTO.Category
{
    public class CategoryEditDTO
    {
        public string OldName { get; set; }

        public string NewName { get; set; }

        public List<string> Dishes { get; set; }

        public CategoryEditDTO()
        {
            Dishes = new List<string>();
        }
    }
}
