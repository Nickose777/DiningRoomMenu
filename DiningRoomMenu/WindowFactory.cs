using System.Windows;

namespace DiningRoomMenu
{
    static class WindowFactory
    {
        public static Window CreateByContentsSize(UIElement element)
        {
            return new Window
            {
                Content = element,
                SizeToContent = System.Windows.SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
        }
    }
}
