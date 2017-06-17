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
        private IngredientListViewModel listViewModel;

        public IngredientViewController(IControllerFactory factory)
            : base(factory)
        {
            listViewModel = new IngredientListViewModel(factory, this);
        }

        public UIElement GetAddView()
        {
            IngredientAddViewModel viewModel = new IngredientAddViewModel();
            IngredientAddView view = new IngredientAddView(viewModel);

            viewModel.IngredientAdded += (s, e) => OnAdd(e.Data, viewModel);

            return view;
        }

        public UIElement GetEditView()
        {
            IngredientEditViewModel viewModel = new IngredientEditViewModel(factory, listViewModel);
            IngredientEditView view = new IngredientEditView(viewModel);

            viewModel.IngredientSaveRequest += (s, e) => OnSave(e.Data);
            viewModel.IngredientDeleteRequest += (s, e) => OnDelete(e.Data, viewModel);

            return view;
        }

        public UIElement GetListView()
        {
            return new IngredientListView(listViewModel);
        }

        private void OnAdd(string ingredientName, IngredientAddViewModel viewModel)
        {
            using (IIngredientController controller = factory.CreateIngredientController())
            {
                ControllerMessage controllerMessage = controller.Add(ingredientName);

                if (controllerMessage.IsSuccess)
                {
                    viewModel.Name = String.Empty;
                    Notify();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void OnSave(IngredientEditDTO ingredientEditDTO)
        {
            using (IIngredientController controller = factory.CreateIngredientController())
            {
                ControllerMessage controllerMessage = controller.Update(ingredientEditDTO);

                if (controllerMessage.IsSuccess)
                {
                    ingredientEditDTO.OldName = ingredientEditDTO.NewName;
                    Notify();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void OnDelete(IngredientEditDTO ingredientEditDTO, IngredientEditViewModel viewModel)
        {
            using (IIngredientController controller = factory.CreateIngredientController())
            {
                ControllerMessage controllerMessage = controller.Delete(ingredientEditDTO.OldName);

                if (controllerMessage.IsSuccess)
                {
                    Notify();
                    viewModel.Clear();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }
    }
}
