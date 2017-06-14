using DiningRoomMenu.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.StockControls.ViewModels
{
    public class StockAddViewModel : ObservableObject
    {
        public event GenericEventHandler<string> StockAdded;

        private string stockNo;

        public StockAddViewModel()
        {
            this.SaveCommand = new DelegateCommand(
                () => RaiseStockAddedEvent(stockNo), 
                CanSave);
        }

        public ICommand SaveCommand { get; private set; }

        public string StockNo
        {
            get { return stockNo; }
            set 
            {
                stockNo = value;
                RaisePropertyChangedEvent("StockNo");
            }
        }

        private bool CanSave(object parameter)
        {
            int n;
            return Int32.TryParse(stockNo, out n);
        }

        private void RaiseStockAddedEvent(string stockNo)
        {
            var handler = StockAdded;
            if (handler != null)
            {
                GenericEventArgs<string> e = new GenericEventArgs<string>(stockNo);
                handler(this, e);
            }
        }
    }
}
