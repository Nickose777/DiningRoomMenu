using System.Windows.Controls;
using DiningRoomMenu.Controls.StockControls.ViewModels;

namespace DiningRoomMenu.Controls.StockControls.Views
{
    /// <summary>
    /// Interaction logic for StockAddView.xaml
    /// </summary>
    public partial class StockAddView : UserControl
    {
        public StockAddView(StockAddViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
