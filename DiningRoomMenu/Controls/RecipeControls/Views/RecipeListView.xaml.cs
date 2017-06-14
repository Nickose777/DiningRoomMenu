using System.Windows.Controls;
using DiningRoomMenu.Controls.RecipeControls.ViewModels;

namespace DiningRoomMenu.Controls.RecipeControls.Views
{
    /// <summary>
    /// Interaction logic for RecipeListView.xaml
    /// </summary>
    public partial class RecipeListView : UserControl
    {
        public RecipeListView(RecipeListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
