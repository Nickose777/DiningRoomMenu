using DiningRoomMenu.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.IngredientControls.ViewModels
{
    public class IngredientAddViewModel : ObservableObject
    {
        public event GenericEventHandler IngredientAdded;

        private string name;

        public IngredientAddViewModel()
        {
            this.SaveCommand = new DelegateCommand(
                () => RaiseIngredientAddedEvent(name), 
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

        private void RaiseIngredientAddedEvent(string name)
        {
            var handler = IngredientAdded;
            if (handler != null)
            {
                GenericEventArgs<string> e = new GenericEventArgs<string>(name);
                handler(this, e);
            }
        }
    }
}
