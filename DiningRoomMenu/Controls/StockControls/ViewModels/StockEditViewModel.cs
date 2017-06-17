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
    public class StockEditViewModel : ObservableObject
    {
        public event GenericEventHandler<StockEditDTO> StockSaveRequest;
        public event GenericEventHandler<StockEditDTO> StockDeleteRequest;

        private readonly IControllerFactory factory;
        private StockEditDTO stock;

        public StockEditViewModel(IControllerFactory factory, StockListViewModel viewModel, StockEditDTO stockEditDTO = null)
        {
            this.factory = factory;
            this.stock = stockEditDTO;

            this.SaveCommand = new DelegateCommand(
                () => RaiseStockSaveRequestEvent(stock),
                obj => CanSave());
            this.DeleteCommand = new DelegateCommand(
                () => RaiseStockDeleteRequestEvent(stock), 
                obj => stock != null);

            this.IngredientCount = new ObservableCollection<IngredientCount>();

            viewModel.StockSelected += (s, e) => ChangeStock(e.Data.StockNo);
        }

        public void ChangeStock(int stockNo)
        {
            using (IStockController controller = factory.CreateStockController())
            {
                DataControllerMessage<StockEditDTO> controllerMessage = controller.Get(stockNo);
                if (controllerMessage.IsSuccess)
                {
                    this.stock = controllerMessage.Data;

                    this.IngredientCount.Clear();
                    foreach (IngredientCount ingredientCount in stock.IngredientCount)
                    {
                        this.IngredientCount.Add(ingredientCount);
                    }

                    RaisePropertyChangedEvent("StockNo");
                }
            }
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
            return
                stock != null &&
                StockNo > 0;
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
