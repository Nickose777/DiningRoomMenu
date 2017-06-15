using DiningRoomMenu.Logic.DTO;
using DiningRoomMenu.Logic.DTO.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.MenuControls.ViewModels
{
    public class MenuViewModel : ObservableObject
    {
        public MenuViewModel(MenuDTO menu)
        {
            Categories = new ObservableCollection<CategoryMenuDTO>(menu.Categories);
        }

        public ObservableCollection<CategoryMenuDTO> Categories { get; private set; }
    }
}
