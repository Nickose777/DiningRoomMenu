using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.MenuControls.ViewModels;
using DiningRoomMenu.Controls.MenuControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiningRoomMenu.ViewControllers
{
    class MenuViewController : ViewControllerBase, IMenuViewController
    {
        public MenuViewController(IControllerFactory factory)
            : base(factory) { }

        public UIElement GetMenuView()
        {
            MenuViewModel viewModel = new MenuViewModel(factory, this);
            MenuView view = new MenuView(viewModel);

            viewModel.MenuChanged += (s, e) => OnMenuUpdate(e.Data, viewModel);

            return view;
        }

        private void OnMenuUpdate(MenuDTO menu, MenuViewModel viewModel)
        {
            using (IMenuController controller = factory.CreateMenuController())
            {
                ControllerMessage controllerMessage = controller.UpdateMenu(menu);
                if (!controllerMessage.IsSuccess)
                {
                    MessageBox.Show(controllerMessage.Message);
                    Notify();
                }
            }
        }
    }
}
