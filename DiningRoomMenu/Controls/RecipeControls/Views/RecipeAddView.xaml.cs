using System.Windows.Controls;
using DiningRoomMenu.Controls.RecipeControls.ViewModels;

namespace DiningRoomMenu.Controls.RecipeControls.Views
{
    /// <summary>
    /// Interaction logic for RecipeAddView.xaml
    /// </summary>
    public partial class RecipeAddView : UserControl
    {
        public RecipeAddView()
        {
            InitializeComponent();
        }

        public RecipeAddView(RecipeAddViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
