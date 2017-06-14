using System.Windows.Controls;
using DiningRoomMenu.Controls.CategoryControls.ViewModels;

namespace DiningRoomMenu.Controls.CategoryControls.Views
{
    /// <summary>
    /// Interaction logic for CategoryListView.xaml
    /// </summary>
    public partial class CategoryListView : UserControl
    {
        public CategoryListView(CategoryListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
