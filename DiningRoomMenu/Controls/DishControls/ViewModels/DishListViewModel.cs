using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Dish;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.DishControls.ViewModels
{
    public class DishListViewModel : ObservableObject
    {
        public event GenericEventHandler<DishDisplayDTO> DishSelected;

        private DishDisplayDTO dish;

        public DishListViewModel(IEnumerable<DishDisplayDTO> dishes)
        {
            this.SelectCommand = new DelegateCommand(
                () => RaiseDishSelectedEvent(Dish),
                obj => Dish != null);

            this.Dishes = new ObservableCollection<DishDisplayDTO>(dishes);
        }

        public ICommand SelectCommand { get; private set; }

        public DishDisplayDTO Dish
        {
            get { return dish; }
            set
            {
                dish = value;
                RaisePropertyChangedEvent("Dish");
            }
        }

        public ObservableCollection<DishDisplayDTO> Dishes { get; set; }

        private void RaiseDishSelectedEvent(DishDisplayDTO dish)
        {
            var handler = DishSelected;
            if (handler != null)
            {
                GenericEventArgs<DishDisplayDTO> e = new GenericEventArgs<DishDisplayDTO>(dish);
                handler(this, e);
            }
        }
    }
}
