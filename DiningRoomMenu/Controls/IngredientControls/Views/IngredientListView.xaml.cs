using System.Windows.Controls;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;

namespace DiningRoomMenu.Controls.IngredientControls.Views
{
    /// <summary>
    /// Interaction logic for IngredientListView.xaml
    /// </summary>
    public partial class IngredientListView : UserControl
    {
        public IngredientListView()
        {
            InitializeComponent();
        }

        public IngredientListView(IngredientListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IngredientListViewModel viewModel = this.DataContext as IngredientListViewModel;
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
