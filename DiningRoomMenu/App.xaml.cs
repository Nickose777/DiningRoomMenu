using System.Windows;
using DiningRoomMenu.Logic;
using DiningRoomMenu.Logic.Contracts;

namespace DiningRoomMenu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IControllerFactory factory = Factory.CreateFactory();

            MainWindow mainWindow = new MainWindow(factory);

            this.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            this.MainWindow = mainWindow;

            mainWindow.Show();
        }
    }
}
