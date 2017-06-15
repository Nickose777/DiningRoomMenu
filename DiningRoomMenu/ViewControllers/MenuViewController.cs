using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.MenuControls.ViewModels;
using DiningRoomMenu.Controls.MenuControls.Views;
using DiningRoomMenu.Logic.Contracts;
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

        public UIElement GetMenuView(ICategorySubject categorySubject, IDishSubject dishSubject)
        {
            MenuViewModel viewModel = new MenuViewModel(factory, categorySubject, dishSubject);
            MenuView view = new MenuView(viewModel);

            return view;
        }
    }
}
