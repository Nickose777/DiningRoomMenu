using System.Windows.Controls;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;

namespace DiningRoomMenu.Controls.IngredientControls.Views
{
    /// <summary>
    /// Interaction logic for IngredientAddView.xaml
    /// </summary>
    public partial class IngredientAddView : UserControl
    {
        public IngredientAddView(IngredientAddViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
