using System.Windows.Controls;
using DiningRoomMenu.Controls.CategoryControls.ViewModels;

namespace DiningRoomMenu.Controls.CategoryControls.Views
{
    /// <summary>
    /// Interaction logic for CategoryEditView.xaml
    /// </summary>
    public partial class CategoryEditView : UserControl
    {
        public CategoryEditView(CategoryEditViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
