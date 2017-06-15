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
    public class DishEditViewModel : ObservableObject
    {
        public event GenericEventHandler<DishEditDTO> DishSaveRequest;
        public event GenericEventHandler<DishEditDTO> DishDeleteRequest;

        private readonly DishEditDTO dish;

        public DishEditViewModel(DishEditDTO dish)
        {
            this.dish = dish;

            this.SaveCommand = new DelegateCommand(
                () => RaiseDishSaveRequestEvent(dish),
                obj => CanSave());
            this.DeleteCommand = new DelegateCommand(() => RaiseDishDeleteRequestEvent(dish));

            this.Recipes = new ObservableCollection<string>(dish.Recipes);
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public string Name
        {
            get { return dish.NewName; }
            set
            {
                dish.NewName = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public decimal Price
        {
            get { return dish.Price; }
            set
            {
                dish.Price = value;
                RaisePropertyChangedEvent("Price");
            }
        }

        public string CategoryName
        {
            get { return dish.CategoryName; }
        }

        public ObservableCollection<string> Recipes { get; set; }

        private bool CanSave()
        {
            return
                !String.IsNullOrEmpty(Name) &&
                Price >= 0;
        }

        private void RaiseDishSaveRequestEvent(DishEditDTO dish)
        {
            var handler = DishSaveRequest;
            if (handler != null)
            {
                GenericEventArgs<DishEditDTO> e = new GenericEventArgs<DishEditDTO>(dish);
                handler(this, e);
            }
        }

        private void RaiseDishDeleteRequestEvent(DishEditDTO dish)
        {
            var handler = DishDeleteRequest;
            if (handler != null)
            {
                GenericEventArgs<DishEditDTO> e = new GenericEventArgs<DishEditDTO>(dish);
                handler(this, e);
            }
        }
    }
}
