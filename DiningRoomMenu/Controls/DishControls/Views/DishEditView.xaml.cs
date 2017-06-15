using System.Windows.Controls;
using DiningRoomMenu.Controls.DishControls.ViewModels;

namespace DiningRoomMenu.Controls.DishControls.Views
{
    /// <summary>
    /// Interaction logic for DishEditView.xaml
    /// </summary>
    public partial class DishEditView : UserControl
    {
        public DishEditView(DishEditViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
