using System.Windows.Controls;
using DiningRoomMenu.Controls.StockControls.ViewModels;

namespace DiningRoomMenu.Controls.StockControls.Views
{
    /// <summary>
    /// Interaction logic for StockListView.xaml
    /// </summary>
    public partial class StockListView : UserControl
    {
        public StockListView(StockListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
