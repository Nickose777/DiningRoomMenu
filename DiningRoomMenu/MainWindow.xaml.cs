using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.CategoryControls.ViewModels;
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
    public partial class MainWindow : Window, IObserver
    {
        private readonly ICategoryViewController categoryViewController;
        private readonly IDishViewController dishViewController;
        private readonly IIngredientViewController ingredientViewController;
        private readonly IMenuViewController menuViewController;
        private readonly IRecipeViewController recipeViewController;
        private readonly IStockViewController stockViewController;

        public MainWindow(IViewControllerFactory factory)
        {
            InitializeComponent();

            this.categoryViewController = factory.CreateCategoryViewController();
            this.dishViewController = factory.CreateDishViewController();
            this.ingredientViewController = factory.CreateIngredientViewController();
            this.menuViewController = factory.CreateMenuViewController();
            this.recipeViewController = factory.CreateRecipeViewController();
            this.stockViewController = factory.CreateStockViewController();

            categoryViewController.Subscribe(this);
            dishViewController.Subscribe(this);
        }

        public void Update()
        {
            menuViewController.Notify();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = categoryViewController.GetAddView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AddStock_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = stockViewController.GetAddView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AllStock_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = stockViewController.GetListView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AllCategories_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = categoryViewController.GetListView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = ingredientViewController.GetAddView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AllIngredients_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = ingredientViewController.GetListView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AddDish_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = dishViewController.GetAddView(categoryViewController);
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AllDishes_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = dishViewController.GetListView(ingredientViewController);
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = recipeViewController.GetAddView(dishViewController, ingredientViewController);
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void AllRecipes_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = recipeViewController.GetListView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }

        private void DisplayMenu_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = menuViewController.GetMenuView();
            Window window = WindowFactory.CreateByContentsSize(view);

            window.Show();
        }
    }
}
