using System.Windows.Controls;
using DiningRoomMenu.Controls.StockControls.ViewModels;

namespace DiningRoomMenu.Controls.StockControls.Views
{
    /// <summary>
    /// Interaction logic for StockEditView.xaml
    /// </summary>
    public partial class StockEditView : UserControl
    {
        public StockEditView(StockEditViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
