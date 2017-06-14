using DiningRoomMenu.Logic.DTO.Dish;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.DishControls.ViewModels
{
    public class DishListViewModel : ObservableObject
    {
        public DishListViewModel(IEnumerable<DishDisplayDTO> dishes)
        {
            this.Dishes = new ObservableCollection<DishDisplayDTO>(dishes);
        }

        public ObservableCollection<DishDisplayDTO> Dishes { get; set; }
    }
}
