using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.DishControls.ViewModels;
using DiningRoomMenu.Controls.DishControls.Views;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;
using DiningRoomMenu.Controls.MenuControls.ViewModels;
using DiningRoomMenu.Controls.MenuControls.Views;
using DiningRoomMenu.Controls.RecipeControls.ViewModels;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO;
using DiningRoomMenu.Logic.DTO.Dish;
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

        public UIElement GetMenuView(IDishViewController dishViewController, IIngredientSubject ingredientSubject)
        {
            MenuViewModel viewModel = new MenuViewModel(factory, this);
            MenuView view = new MenuView(viewModel);

            viewModel.MenuChanged += (s, e) => OnMenuUpdate(e.Data, viewModel);
            viewModel.DishSelected += (s, e) => OnSelect(e.Data, dishViewController, ingredientSubject);

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
                }

                Notify();
            }
        }

        private void OnSelect(string dishName, IDishViewController dishViewController, IIngredientSubject ingredientSubject)
        {
            using (IDishController controller = factory.CreateDishController())
            {
                DataControllerMessage<DishEditDTO> controllerMessage = controller.Get(dishName);
                if (controllerMessage.IsSuccess)
                {
                    UIElement view = dishViewController.GetEditView(ingredientSubject, controllerMessage.Data);
                    Window window = WindowFactory.CreateByContentsSize(view);

                    window.Title = String.Format("{0} - {1}", dishName, controllerMessage.Data.CategoryName);

                    dishViewController.DishDeleted += (s, e) => 
                    {
                        if (dishName == e.Data)
                        {
                            window.Close();
                        }
                    };

                    window.Show();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }
    }
}
