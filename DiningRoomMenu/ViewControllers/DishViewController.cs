using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.DishControls.ViewModels;
using DiningRoomMenu.Controls.DishControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
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
    class DishViewController : ViewControllerBase, IDishViewController
    {
        public DishViewController(IControllerFactory factory)
            : base(factory) { }

        public UIElement GetAddView(ICategorySubject subject)
        {
            DishAddViewModel viewModel = new DishAddViewModel(factory, subject);
            DishAddView view = new DishAddView(viewModel);

            viewModel.DishAdded += (s, e) => OnAdd(e.Data, viewModel);

            return view;
        }

        public UIElement GetListView()
        {
            DishListViewModel viewModel = new DishListViewModel(factory, this);
            DishListView view = new DishListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.DishSelected += (s, e) => OnSelected(e.Data);

            return view;
        }

        private void OnAdd(DishAddDTO dish, DishAddViewModel viewModel)
        {
            using (IDishController controller = factory.CreateDishController())
            {
                ControllerMessage controllerMessage = controller.Add(dish);
                if (controllerMessage.IsSuccess)
                {
                    viewModel.Name = String.Empty;
                    viewModel.Price = 0;

                    Notify();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void OnSelected(DishDisplayDTO dishDisplayDTO)
        {
            using (IDishController controller = factory.CreateDishController())
            {
                DataControllerMessage<DishEditDTO> controllerMessage = controller.Get(dishDisplayDTO.Name);
                if (controllerMessage.IsSuccess)
                {
                    Edit(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void Edit(DishEditDTO dishEditDTO)
        {
            DishEditViewModel viewModel = new DishEditViewModel(dishEditDTO);
            DishEditView view = new DishEditView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.DishSaveRequest += (s, e) =>
            {
                using (IDishController controller = factory.CreateDishController())
                {
                    ControllerMessage controllerMessage = controller.Update(e.Data);
                    if (controllerMessage.IsSuccess)
                    {
                        Notify();
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }
    }
}
