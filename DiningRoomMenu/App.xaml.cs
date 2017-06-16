using System.Windows;
using DiningRoomMenu.Logic;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Contracts;

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
            IViewControllerFactory viewFactory = new ViewControllerFactory(factory);

            MenuWindow mainWindow = new MenuWindow(viewFactory);

            this.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            this.MainWindow = mainWindow;

            mainWindow.Show();
        }
    }
}
