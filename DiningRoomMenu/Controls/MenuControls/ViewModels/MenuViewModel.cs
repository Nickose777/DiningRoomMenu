using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.MenuControls.ViewModels
{
    public class MenuViewModel : ObservableObject, IObserver
    {
        public event GenericEventHandler<MenuDTO> MenuChanged;

        private readonly IControllerFactory factory;

        public MenuViewModel(IControllerFactory factory, IMenuSubject subject)
        {
            this.factory = factory;

            this.SaveCommand = new DelegateCommand(
                () => Save(),
                obj => CanSave());

            this.Categories = new ObservableCollection<CategoryMenuDTO>();

            subject.Subscribe(this);
            Update();
        }

        public void Update()
        {
            Categories.Clear();

            using (IMenuController controller = factory.CreateMenuController())
            {
                DataControllerMessage<MenuDTO> controllerMessage = controller.GetMenu();
                if (controllerMessage.IsSuccess)
                {
                    foreach (CategoryMenuDTO categoryMenu in controllerMessage.Data.Categories)
                    {
                        Categories.Add(categoryMenu);
                    }
                }
            }
        }

        public ICommand SaveCommand { get; set; }

        public ObservableCollection<CategoryMenuDTO> Categories { get; private set; }

        private void Save()
        {
            MenuDTO menu = new MenuDTO();
            menu.Categories.AddRange(Categories);

            RaiseMenuChangedEvent(menu);
        }

        private bool CanSave()
        {
            return
                Categories.All(category => !String.IsNullOrEmpty(category.NewName)) &&
                Categories.All(category => category.Dishes.All(dish => !String.IsNullOrEmpty(dish.NewName)));
        }

        private void RaiseMenuChangedEvent(MenuDTO menu)
        {
            var handler = MenuChanged;
            if (handler != null)
            {
                GenericEventArgs<MenuDTO> e = new GenericEventArgs<MenuDTO>(menu);
                handler(this, e);
            }
        }
    }
}
