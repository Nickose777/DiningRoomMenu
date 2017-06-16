using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
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

namespace DiningRoomMenu.Controls.MenuControls.ViewModels
{
    public class MenuViewModel : ObservableObject, IObserver
    {
        private readonly IControllerFactory factory;

        public MenuViewModel(IControllerFactory factory, IMenuSubject subject)
        {
            this.factory = factory;

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

        public ObservableCollection<CategoryMenuDTO> Categories { get; private set; }
    }
}
