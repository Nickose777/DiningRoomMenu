using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Dish;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.DishControls.ViewModels
{
    public class DishListViewModel : ObservableObject, IObserver
    {
        public event GenericEventHandler<DishDisplayDTO> DishSelected;

        private readonly IControllerFactory factory;
        private DishDisplayDTO dish;

        public DishListViewModel(IControllerFactory factory, IDishSubject subject)
        {
            this.factory = factory;

            this.SelectCommand = new DelegateCommand(
                () => RaiseDishSelectedEvent(Dish),
                obj => Dish != null);

            this.Dishes = new ObservableCollection<DishDisplayDTO>();

            subject.Subscribe(this);
            Update();
        }

        public void Update()
        {
            Dishes.Clear();

            using (IDishController controller = factory.CreateDishController())
            {
                DataControllerMessage<IEnumerable<DishDisplayDTO>> controllerMessage = controller.GetAll();
                if (controllerMessage.IsSuccess)
                {
                    foreach (DishDisplayDTO dish in controllerMessage.Data)
                    {
                        Dishes.Add(dish);
                    }
                }
            }
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
