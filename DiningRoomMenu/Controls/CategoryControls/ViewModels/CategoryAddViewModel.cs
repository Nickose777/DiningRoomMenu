using System;
using System.Windows.Input;
using DiningRoomMenu.EventHandlers;

namespace DiningRoomMenu.Controls.CategoryControls.ViewModels
{
    public class CategoryAddViewModel : ObservableObject
    {
        public event GenericEventHandler CategoryAdded;

        private string name;

        public CategoryAddViewModel()
        {
            this.SaveCommand = new DelegateCommand(
                () => RaiseCategoryAddedEvent(name), 
                obj => !String.IsNullOrEmpty(name));
        }

        public ICommand SaveCommand { get; private set; }

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        private void RaiseCategoryAddedEvent(string name)
        {
            var handler = CategoryAdded;
            if (handler != null)
            {
                GenericEventArgs<string> e = new GenericEventArgs<string>(name);
                handler(this, e);
            }
        }
    }
}
