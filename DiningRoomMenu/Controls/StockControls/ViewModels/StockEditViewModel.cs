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
    public class StockEditViewModel : ObservableObject
    {
        public event GenericEventHandler<StockEditDTO> StockSaveRequest;
        public event GenericEventHandler<StockEditDTO> StockDeleteRequest;

        private readonly StockEditDTO stock;

        public StockEditViewModel(StockEditDTO stock)
        {
            this.stock = stock;

            this.SaveCommand = new DelegateCommand(
                () => RaiseStockSaveRequestEvent(stock),
                obj => CanSave());
            this.DeleteCommand = new DelegateCommand(() => RaiseStockDeleteRequestEvent(stock));

            this.IngredientCount = new ObservableCollection<IngredientCount>(stock.IngredientCount);
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public int StockNo
        {
            get { return stock.NewStockNo; }
            set
            {
                stock.NewStockNo = value;
                RaisePropertyChangedEvent("StockNo");
            }
        }

        public ObservableCollection<IngredientCount> IngredientCount { get; set; }

        private bool CanSave()
        {
            return StockNo > 0;
        }

        private void RaiseStockSaveRequestEvent(StockEditDTO stock)
        {
            var handler = StockSaveRequest;
            if (handler != null)
            {
                GenericEventArgs<StockEditDTO> e = new GenericEventArgs<StockEditDTO>(stock);
                handler(this, e);
            }
        }

        private void RaiseStockDeleteRequestEvent(StockEditDTO stock)
        {
            var handler = StockDeleteRequest;
            if (handler != null)
            {
                GenericEventArgs<StockEditDTO> e = new GenericEventArgs<StockEditDTO>(stock);
                handler(this, e);
            }
        }
    }
}
