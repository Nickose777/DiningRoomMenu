using DiningRoomMenu.Controls.DishControls.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiningRoomMenu.Controls.DishControls.Views
{
    /// <summary>
    /// Interaction logic for DishAddView.xaml
    /// </summary>
    public partial class DishAddView : UserControl
    {
        public DishAddView(DishAddViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
