﻿using DiningRoomMenu.Controls.CategoryControls.ViewModels;
using DiningRoomMenu.Controls.CategoryControls.Views;
using DiningRoomMenu.Controls.DishControls.ViewModels;
using DiningRoomMenu.Controls.DishControls.Views;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;
using DiningRoomMenu.Controls.IngredientControls.Views;
using DiningRoomMenu.Controls.MenuControls.ViewModels;
using DiningRoomMenu.Controls.MenuControls.Views;
using DiningRoomMenu.Controls.RecipeControls.ViewModels;
using DiningRoomMenu.Controls.RecipeControls.Views;
using DiningRoomMenu.Controls.StockControls.ViewModels;
using DiningRoomMenu.Controls.StockControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.DTO.Dish;
using DiningRoomMenu.Logic.DTO.Ingredient;
using DiningRoomMenu.Logic.DTO.Recipe;
using DiningRoomMenu.Logic.DTO.Stock;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DiningRoomMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IControllerFactory factory;

        public MainWindow(IControllerFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryAddViewModel viewModel = new CategoryAddViewModel();
            CategoryAddView view = new CategoryAddView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.CategoryAdded += (s, ea) =>
            {
                using (ICategoryController controller = factory.CreateCategoryController())
                {
                    ControllerMessage controllerMessage = controller.Add(ea.Data);

                    if (controllerMessage.IsSuccess)
                    {
                        viewModel.Name = String.Empty;
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AddStock_Click(object sender, RoutedEventArgs e)
        {
            StockAddViewModel viewModel = new StockAddViewModel();
            StockAddView view = new StockAddView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.StockAdded += (s, ea) =>
            {
                int stockNo = Convert.ToInt32(ea.Data);
                using (IStockController controller = factory.CreateStockController())
                {
                    ControllerMessage controllerMessage = controller.Add(stockNo);

                    if (controllerMessage.IsSuccess)
                    {
                        viewModel.StockNo = String.Empty;
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AllStock_Click(object sender, RoutedEventArgs e)
        {
            using (IStockController controller = factory.CreateStockController())
            {
                DataControllerMessage<IEnumerable<StockDisplayDTO>> controllerMessage = controller.GetAll();

                if (controllerMessage.IsSuccess)
                {
                    DisplayStocks(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void DisplayStocks(IEnumerable<StockDisplayDTO> stocks)
        {
            StockListViewModel viewModel = new StockListViewModel(stocks);
            StockListView view = new StockListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.StockSelected += (s, e) =>
            {
                using (IStockController controller = factory.CreateStockController())
                {
                    DataControllerMessage<StockEditDTO> controllerMessage = controller.Get(e.Data.StockNo);
                    if (controllerMessage.IsSuccess)
                    {
                        EditStock(controllerMessage.Data);
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void EditStock(StockEditDTO stock)
        {
            StockEditViewModel viewModel = new StockEditViewModel(stock);
            StockEditView view = new StockEditView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.StockSaveRequest += (s, e) =>
            {
                using (IStockController controller = factory.CreateStockController())
                {
                    ControllerMessage controllerMessage = controller.Update(e.Data);
                    if (controllerMessage.IsSuccess)
                    {
                        //TODO
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AllCategories_Click(object sender, RoutedEventArgs e)
        {
            using (ICategoryController controller = factory.CreateCategoryController())
            {
                DataControllerMessage<IEnumerable<CategoryDisplayDTO>> controllerMessage = controller.GetAll();

                if (controllerMessage.IsSuccess)
                {
                    DisplayCategories(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void DisplayCategories(IEnumerable<CategoryDisplayDTO> categories)
        {
            CategoryListViewModel viewModel = new CategoryListViewModel(categories);
            CategoryListView view = new CategoryListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.CategorySelected += (s, e) =>
            {
                using (ICategoryController controller = factory.CreateCategoryController())
                {
                    DataControllerMessage<CategoryEditDTO> controllerMessage = controller.Get(e.Data);
                    if (controllerMessage.IsSuccess)
                    {
                        DisplayCategory(controllerMessage.Data);
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
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
                        //TODO
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            IngredientAddViewModel viewModel = new IngredientAddViewModel();
            IngredientAddView view = new IngredientAddView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.IngredientAdded += (s, ea) =>
            {
                using (IIngredientController controller = factory.CreateIngredientController())
                {
                    ControllerMessage controllerMessage = controller.Add(ea.Data);

                    if (controllerMessage.IsSuccess)
                    {
                        viewModel.Name = String.Empty;
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AllIngredients_Click(object sender, RoutedEventArgs e)
        {
            using (IIngredientController controller = factory.CreateIngredientController())
            {
                DataControllerMessage<IEnumerable<IngredientDisplayDTO>> controllerMessage = controller.GetAll();

                if (controllerMessage.IsSuccess)
                {
                    DisplayIngredients(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void DisplayIngredients(IEnumerable<IngredientDisplayDTO> ingredients)
        {
            IngredientListViewModel viewModel = new IngredientListViewModel(ingredients);
            IngredientListView view = new IngredientListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AddDish_Click(object sender, RoutedEventArgs e)
        {
            using (ICategoryController controller = factory.CreateCategoryController())
            {
                DataControllerMessage<IEnumerable<CategoryDisplayDTO>> controllerMessage = controller.GetAll();
                if (controllerMessage.IsSuccess)
                {
                    AddDish(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void AddDish(IEnumerable<CategoryDisplayDTO> categories)
        {
            DishAddViewModel viewModel = new DishAddViewModel(categories);
            DishAddView view = new DishAddView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.DishAdded += (s, e) =>
            {
                using (IDishController controller = factory.CreateDishController())
                {
                    ControllerMessage controllerMessage = controller.Add(e.Data);
                    if (controllerMessage.IsSuccess)
                    {
                        viewModel.Name = String.Empty;
                        viewModel.Price = 0;
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            using (IDishController dishController = factory.CreateDishController())
            {
                using (IIngredientController ingredientController = factory.CreateIngredientController())
                {
                    DataControllerMessage<IEnumerable<IngredientDisplayDTO>> controllerMessage1 = ingredientController.GetAll();

                    if (controllerMessage1.IsSuccess)
                    {
                        IngredientListViewModel viewModel = new IngredientListViewModel(controllerMessage1.Data);
                        DataControllerMessage<IEnumerable<DishDisplayDTO>> controllerMessage2 = dishController.GetAll();
                        if (controllerMessage2.IsSuccess)
                        {
                            AddRecipe(controllerMessage2.Data, viewModel);
                        }
                        else
                        {
                            MessageBox.Show(controllerMessage2.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage1.Message);
                    }
                }
            }
        }

        private void AddRecipe(IEnumerable<DishDisplayDTO> dishes, IngredientListViewModel ingredientsViewModel)
        {
            RecipeAddViewModel viewModel = new RecipeAddViewModel(dishes, ingredientsViewModel);
            RecipeAddView view = new RecipeAddView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.RecipeAdded += (s, ea) =>
            {
                using (IRecipeController controller = factory.CreateRecipeController())
                {
                    ControllerMessage controllerMessage = controller.Add(ea.Data);
                    if (controllerMessage.IsSuccess)
                    {
                        viewModel.Name = String.Empty;
                        viewModel.Description = String.Empty;
                        viewModel.Ingredients.Clear();
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AllDishes_Click(object sender, RoutedEventArgs e)
        {
            using (IDishController controller = factory.CreateDishController())
            {
                DataControllerMessage<IEnumerable<DishDisplayDTO>> controllerMessage = controller.GetAll();
                if (controllerMessage.IsSuccess)
                {
                    DisplayDishes(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void DisplayDishes(IEnumerable<DishDisplayDTO> dishes)
        {
            DishListViewModel viewModel = new DishListViewModel(dishes);
            DishListView view = new DishListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.DishSelected += (s, e) =>
            {
                using (IDishController controller = factory.CreateDishController())
                {
                    DataControllerMessage<DishEditDTO> controllerMessage = controller.Get(e.Data.Name);
                    if (controllerMessage.IsSuccess)
                    {
                        Edit(controllerMessage.Data);
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
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
                        //TODO
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }

        private void AllRecipes_Click(object sender, RoutedEventArgs e)
        {
            using (IRecipeController controller = factory.CreateRecipeController())
            {
                DataControllerMessage<IEnumerable<RecipeDisplayDTO>> controllerMessage = controller.GetAll();
                if (controllerMessage.IsSuccess)
                {
                    DisplayDishes(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void DisplayDishes(IEnumerable<RecipeDisplayDTO> recipes)
        {
            RecipeListViewModel viewModel = new RecipeListViewModel(recipes);
            RecipeListView view = new RecipeListView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void DisplayMenu_Click(object sender, RoutedEventArgs e)
        {
            using (IMenuController controller = factory.CreateMenuController())
            {
                DataControllerMessage<MenuDTO> controllerMessage = controller.GetMenu();
                if (controllerMessage.IsSuccess)
                {
                    DisplayMenu(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void DisplayMenu(MenuDTO menuDTO)
        {
            MenuViewModel viewModel = new MenuViewModel(menuDTO);
            MenuView view = new MenuView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }
    }
}
