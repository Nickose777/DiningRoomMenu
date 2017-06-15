using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.CategoryControls.ViewModels;
using DiningRoomMenu.Controls.CategoryControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiningRoomMenu.ViewControllers
{
    class CategoryViewController : ViewControllerBase, ICategoryViewController
    {
        public CategoryViewController(IControllerFactory factory)
            : base(factory) { }

        public UIElement GetAddView()
        {
            CategoryAddViewModel viewModel = new CategoryAddViewModel();
            CategoryAddView view = new CategoryAddView(viewModel);

            viewModel.CategoryAdded += (s, e) => OnAdd(e.Data, viewModel);

            return view;
        }

        public UIElement GetListView()
        {
            CategoryListViewModel viewModel = new CategoryListViewModel(factory, this);
            CategoryListView view = new CategoryListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.CategorySelected += (s, e) => OnSelect(e.Data);

            return view;
        }

        private void OnSelect(CategoryDisplayDTO categoryDisplayDTO)
        {
            using (ICategoryController controller = factory.CreateCategoryController())
            {
                DataControllerMessage<CategoryEditDTO> controllerMessage = controller.Get(categoryDisplayDTO);
                if (controllerMessage.IsSuccess)
                {
                    DisplayCategory(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void DisplayCategory(CategoryEditDTO categoryEditDTO)
        {
            CategoryEditViewModel viewModel = new CategoryEditViewModel(categoryEditDTO);
            CategoryEditView view = new CategoryEditView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.CategorySaveRequest += (s, e) =>
            {
                using (ICategoryController controller = factory.CreateCategoryController())
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

        private void OnAdd(string categoryName, CategoryAddViewModel viewModel)
        {
            using (ICategoryController controller = factory.CreateCategoryController())
            {
                ControllerMessage controllerMessage = controller.Add(categoryName);

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
    }
}
