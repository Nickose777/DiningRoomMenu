using DiningRoomMenu.Controls.CategoryControls.ViewModels;
using System.Windows.Controls;

namespace DiningRoomMenu.Controls.CategoryControls.Views
{
    /// <summary>
    /// Interaction logic for CategoryAddView.xaml
    /// </summary>
    public partial class CategoryAddView : UserControl
    {
        public CategoryAddView(CategoryAddViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
