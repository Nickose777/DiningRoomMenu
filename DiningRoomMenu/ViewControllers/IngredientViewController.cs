using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;
using DiningRoomMenu.Controls.IngredientControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Ingredient;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiningRoomMenu.ViewControllers
{
    class IngredientViewController : ViewControllerBase, IIngredientViewController
    {
        public IngredientViewController(IControllerFactory factory)
            : base(factory) { }

        public UIElement GetAddView()
        {
            IngredientAddViewModel viewModel = new IngredientAddViewModel();
            IngredientAddView view = new IngredientAddView(viewModel);

            viewModel.IngredientAdded += (s, e) => OnAdd(e.Data, viewModel);

            return view;
        }

        public UIElement GetListView()
        {
            IngredientListViewModel viewModel = new IngredientListViewModel(factory, this);
            IngredientListView view = new IngredientListView(viewModel);

            viewModel.IngredientSelected += (s, e) => OnSelected(e.Data);

            return view;
        }

        private void OnSelected(IngredientDisplayDTO ingredientDisplayDTO)
        {
            MessageBox.Show("TODO");
        }

        private void OnAdd(string ingredientName, IngredientAddViewModel viewModel)
        {
            using (IIngredientController controller = factory.CreateIngredientController())
            {
                ControllerMessage controllerMessage = controller.Add(ingredientName);

                if (controllerMessage.IsSuccess)
                {
                    viewModel.Name = String.Empty;
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }
    }
}
