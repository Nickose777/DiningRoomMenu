using System.Windows.Controls;
using DiningRoomMenu.Controls.DishControls.ViewModels;

namespace DiningRoomMenu.Controls.DishControls.Views
{
    /// <summary>
    /// Interaction logic for DishListView.xaml
    /// </summary>
    public partial class DishListView : UserControl
    {
        public DishListView(DishListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
