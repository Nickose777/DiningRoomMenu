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

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StockListViewModel viewModel = this.DataContext as StockListViewModel;
            if (viewModel != null)
            {
                if (viewModel.SelectCommand.CanExecute(null))
                {
                    viewModel.SelectCommand.Execute(null);
                }
            }
        }
    }
}
