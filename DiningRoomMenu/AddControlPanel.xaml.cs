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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiningRoomMenu
{
    /// <summary>
    /// Interaction logic for AddControlPanel.xaml
    /// </summary>
    public partial class AddControlPanel : UserControl
    {
        private UIElement lastView;

        private readonly ICategoryViewController categoryViewController;
        private readonly IDishViewController dishViewController;

        public AddControlPanel(ICategoryViewController categoryViewController, IDishViewController dishViewController)
        {
            InitializeComponent();
            this.categoryViewController = categoryViewController;
            this.dishViewController = dishViewController;

            DisplayCategory();
        }

        private void DisplayCategoryView_Click(object sender, RoutedEventArgs e)
        {
            DisplayCategory();
        }

        private void DisplayCategory()
        {
            UIElement view = categoryViewController.GetAddView();
            DisplayOnGrid(view);
        }

        private void DisplayDishView_Click(object sender, RoutedEventArgs e)
        {
            UIElement view = dishViewController.GetAddView(categoryViewController);
            DisplayOnGrid(view);
        }

        private void DisplayOnGrid(UIElement view)
        {
            if (lastView != null)
            {
                mainGrid.Children.Remove(lastView);
            }

            Grid.SetRow(view, 1);
            mainGrid.Children.Add(view);
            lastView = view;
        }
    }
}
