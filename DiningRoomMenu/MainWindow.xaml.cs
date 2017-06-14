using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.Infrastructure;
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

namespace DiningRoomMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IControllerFactory factory;

        public MainWindow(IControllerFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (IConnectionController controller = factory.CreateConnectionController())
            {
                ControllerMessage controllerMessage = controller.TestConnection();

                MessageBox.Show(controllerMessage.Message);
            }
        }
    }
}
