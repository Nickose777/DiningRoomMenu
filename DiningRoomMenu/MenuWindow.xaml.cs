using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiningRoomMenu
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window, IObserver
    {
        private UIElement lastView;

        private readonly ICategoryViewController categoryViewController;
        private readonly IDishViewController dishViewController;
        private readonly IIngredientViewController ingredientViewController;
        private readonly IMenuViewController menuViewController;
        private readonly IRecipeViewController recipeViewController;
        private readonly IStockViewController stockViewController;

        public MenuWindow(IViewControllerFactory factory)
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

            DisplayMenu();
        }

        public void Update()
        {
            menuViewController.Notify();
        }

        private void DisplayMenu_Click(object sender, RoutedEventArgs e)
        {
            DisplayMenu();
        }

        private void DisplayMenu()
        {
            AddControlPanel controlPanel = new AddControlPanel(categoryViewController, dishViewController);
            UIElement view = menuViewController.GetMenuView();

            Grid grid = new Grid();
            ColumnDefinition c1 = new ColumnDefinition();
            ColumnDefinition c2 = new ColumnDefinition();

            c1.Width = new GridLength(1, GridUnitType.Star);
            c2.Width = new GridLength(1, GridUnitType.Star);

            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);

            Grid.SetColumn(view, 0);
            Grid.SetColumn(controlPanel, 1);

            grid.Children.Add(view);
            grid.Children.Add(controlPanel);

            DisplayOnGrid(grid);
        }

        private void DisplayIngredients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisplayStocks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisplayOnGrid(UIElement view)
        {
            if (lastView != null)
            {
                mainGrid.Children.Remove(lastView);
            }

            Grid.SetColumn(view, 1);
            mainGrid.Children.Add(view);
            lastView = view;
        }
    }
}
