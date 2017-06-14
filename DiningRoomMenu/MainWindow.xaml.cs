using DiningRoomMenu.Controls.CategoryControls.ViewModels;
using DiningRoomMenu.Controls.CategoryControls.Views;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;
using DiningRoomMenu.Controls.IngredientControls.Views;
using DiningRoomMenu.Controls.StockControls.ViewModels;
using DiningRoomMenu.Controls.StockControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.DTO.Ingredient;
using DiningRoomMenu.Logic.DTO.Stock;
using DiningRoomMenu.Logic.Infrastructure;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
