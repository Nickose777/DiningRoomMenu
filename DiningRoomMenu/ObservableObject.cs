using System.ComponentModel;

namespace DiningRoomMenu
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string property)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(property);
                handler(this, e);
            }
        }
    }
}
