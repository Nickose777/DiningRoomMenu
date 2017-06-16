using System.Windows.Controls;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;

namespace DiningRoomMenu.Controls.IngredientControls.Views
{
    /// <summary>
    /// Interaction logic for IngredientEditView.xaml
    /// </summary>
    public partial class IngredientEditView : UserControl
    {
        public IngredientEditView(IngredientEditViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
