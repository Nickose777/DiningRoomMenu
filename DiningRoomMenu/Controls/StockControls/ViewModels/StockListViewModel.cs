using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.StockControls.ViewModels
{
    public class StockListViewModel : ObservableObject
    {
        public event GenericEventHandler<StockDisplayDTO> StockSelected;

        private StockDisplayDTO stock;

        public StockListViewModel(IEnumerable<StockDisplayDTO> stocks)
        {
            this.SelectCommand = new DelegateCommand(
                () => RaiseStockSelectedEvent(Stock),
                obj => Stock != null);

            this.Stocks = new ObservableCollection<StockDisplayDTO>(stocks);
        }

        public ICommand SelectCommand { get; private set; }

        public StockDisplayDTO Stock
        {
            get { return stock; }
            set
            {
                stock = value;
                RaisePropertyChangedEvent("Stock");
            }
        }

        public ObservableCollection<StockDisplayDTO> Stocks { get; set; }

        private void RaiseStockSelectedEvent(StockDisplayDTO stock)
        {
            var handler = StockSelected;
            if (handler != null)
            {
                GenericEventArgs<StockDisplayDTO> e = new GenericEventArgs<StockDisplayDTO>(stock);
                handler(this, e);
            }
        }
    }
}
