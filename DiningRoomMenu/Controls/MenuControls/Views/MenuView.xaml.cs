using System.Windows.Controls;
using DiningRoomMenu.Controls.MenuControls.ViewModels;

namespace DiningRoomMenu.Controls.MenuControls.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView(MenuViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
