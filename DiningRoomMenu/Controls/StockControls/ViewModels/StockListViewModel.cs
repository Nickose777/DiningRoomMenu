using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Stock;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.StockControls.ViewModels
{
    public class StockListViewModel : ObservableObject, IObserver
    {
        public event GenericEventHandler<StockDisplayDTO> StockSelected;

        private readonly IControllerFactory factory;
        private StockDisplayDTO stock;

        public StockListViewModel(IControllerFactory factory, IStockSubject subject)
        {
            this.factory = factory;
            subject.Subscribe(this);

            this.SelectCommand = new DelegateCommand(
                () => RaiseStockSelectedEvent(Stock),
                obj => Stock != null);

            this.Stocks = new ObservableCollection<StockDisplayDTO>();

            Update();
        }

        public void Update()
        {
            Stocks.Clear();

            using (IStockController controller = factory.CreateStockController())
            {
                DataControllerMessage<IEnumerable<StockDisplayDTO>> message = controller.GetAll();
                if (message.IsSuccess)
                {
                    foreach (StockDisplayDTO stock in message.Data)
                    {
                        Stocks.Add(stock);
                    }
                }
            }
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
