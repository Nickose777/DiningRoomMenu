using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.StockControls.ViewModels;
using DiningRoomMenu.Controls.StockControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Stock;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiningRoomMenu.ViewControllers
{
    class StockViewController : ViewControllerBase, IStockViewController
    {
        public StockViewController(IControllerFactory factory)
            : base(factory) { }

        public UIElement GetAddView()
        {
            StockAddViewModel viewModel = new StockAddViewModel();
            StockAddView view = new StockAddView(viewModel);

            viewModel.StockAdded += (s, ea) => OnAdd(Convert.ToInt32(ea.Data), viewModel);

            return view;
        }

        public UIElement GetListView()
        {
            StockListViewModel viewModel = new StockListViewModel(factory, this);
            StockListView view = new StockListView(viewModel);

            viewModel.StockSelected += (s, e) => OnSelect(e.Data.StockNo);

            return view;
        }

        private void OnAdd(int stockNo, StockAddViewModel viewModel)
        {
            using (IStockController controller = factory.CreateStockController())
            {
                ControllerMessage controllerMessage = controller.Add(stockNo);
                if (controllerMessage.IsSuccess)
                {
                    viewModel.StockNo = String.Empty;
                    Notify();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void OnSelect(int stockNo)
        {
            using (IStockController controller = factory.CreateStockController())
            {
                DataControllerMessage<StockEditDTO> controllerMessage = controller.Get(stockNo);
                if (controllerMessage.IsSuccess)
                {
                    EditStock(controllerMessage.Data);
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void EditStock(StockEditDTO stock)
        {
            StockEditViewModel viewModel = new StockEditViewModel(stock);
            StockEditView view = new StockEditView(viewModel);
            Window window = WindowFactory.CreateByContentsSize(view);

            viewModel.StockSaveRequest += (s, e) =>
            {
                using (IStockController controller = factory.CreateStockController())
                {
                    ControllerMessage controllerMessage = controller.Update(e.Data);
                    if (controllerMessage.IsSuccess)
                    {
                        Notify();
                    }
                    else
                    {
                        MessageBox.Show(controllerMessage.Message);
                    }
                }
            };

            window.Show();
        }
    }
}
