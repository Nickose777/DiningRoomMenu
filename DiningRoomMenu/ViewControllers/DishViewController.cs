using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.DishControls.ViewModels;
using DiningRoomMenu.Controls.DishControls.Views;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;
using DiningRoomMenu.Controls.RecipeControls.ViewModels;
using DiningRoomMenu.EventHandlers;
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
        public event GenericEventHandler<string> DishDeleted;

        public DishViewController(IControllerFactory factory)
            : base(factory) { }

        public UIElement GetAddView(ICategorySubject subject)
        {
            DishAddViewModel viewModel = new DishAddViewModel(factory, subject);
            DishAddView view = new DishAddView(viewModel);

            viewModel.DishAdded += (s, e) => OnAdd(e.Data, viewModel);

            return view;
        }

        public UIElement GetListView(IIngredientSubject subject)
        {
            DishListViewModel viewModel = new DishListViewModel(factory, this);
            DishListView view = new DishListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.DishSelected += (s, e) => OnSelected(e.Data, subject);

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

        public UIElement GetEditView(IIngredientSubject subject, DishEditDTO dishEditDTO)
        {
            IngredientListViewModel ingredientListViewModel = new IngredientListViewModel(factory, subject);
            RecipeAddViewModel recipeAddViewModel = new RecipeAddViewModel(factory, this, ingredientListViewModel);
            recipeAddViewModel.MustSelectDish = false;

            DishEditViewModel viewModel = new DishEditViewModel(dishEditDTO, recipeAddViewModel);
            DishEditView view = new DishEditView(viewModel);

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
            viewModel.DishSaveRecipesRequest += (s, e) =>
            {
                using (IDishController controller = factory.CreateDishController())
                {
                    ControllerMessage controllerMessage = controller.UpdateRecipes(e.Data);
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
            viewModel.DishDeleteRequest += (s, e) =>
            {
                using (IDishController controller = factory.CreateDishController())
                {
                    ControllerMessage controllerMessage = controller.Delete(e.Data.OldName);
                    if (controllerMessage.IsSuccess)
                    {
                        Notify();
                        RaiseDishDeletedEvent(e.Data.OldName);
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            return view;
        }

        private void OnSelected(DishDisplayDTO dishDisplayDTO, IIngredientSubject subject)
        {
            using (IDishController controller = factory.CreateDishController())
            {
                DataControllerMessage<DishEditDTO> controllerMessage = controller.Get(dishDisplayDTO.Name);
                if (controllerMessage.IsSuccess)
                {
                    Edit(controllerMessage.Data, subject);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void Edit(DishEditDTO dishEditDTO, IIngredientSubject subject)
        {
            IngredientListViewModel ingredientListViewModel = new IngredientListViewModel(factory, subject);
            RecipeAddViewModel recipeAddViewModel = new RecipeAddViewModel(factory, this, ingredientListViewModel);
            recipeAddViewModel.MustSelectDish = false;

            DishEditViewModel viewModel = new DishEditViewModel(dishEditDTO, recipeAddViewModel);
            DishEditView view = new DishEditView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);
            window.MinWidth = view.MinWidth;

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
            viewModel.DishSaveRecipesRequest += (s, e) =>
            {
                using (IDishController controller = factory.CreateDishController())
                {
                    ControllerMessage controllerMessage = controller.UpdateRecipes(e.Data);
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


        private void RaiseDishDeletedEvent(string dishName)
        {
            var handler = DishDeleted;
            if (handler != null)
            {
                GenericEventArgs<string> e = new GenericEventArgs<string>(dishName);
                handler(this, e);
            }
        }
    }
}
