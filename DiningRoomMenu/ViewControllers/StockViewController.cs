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
        private StockListViewModel listViewModel;

        public StockViewController(IControllerFactory factory)
            : base(factory)
        {
            listViewModel = new StockListViewModel(factory, this);
        }

        public UIElement GetAddView()
        {
            StockAddViewModel viewModel = new StockAddViewModel();
            StockAddView view = new StockAddView(viewModel);

            viewModel.StockAdded += (s, ea) => OnAdd(Convert.ToInt32(ea.Data), viewModel);

            return view;
        }

        public UIElement GetEditView()
        {
            StockEditViewModel viewModel = new StockEditViewModel(factory, listViewModel);
            StockEditView view = new StockEditView(viewModel);

            viewModel.StockSaveRequest += (s, e) => OnSave(e.Data);
            viewModel.StockDeleteRequest += (s, e) => OnDelete(e.Data, viewModel);

            return view;
        }

        public UIElement GetListView()
        {
            StockListView view = new StockListView(listViewModel);

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

        private void OnSave(StockEditDTO stockEditDTO)
        {
            using (IStockController controller = factory.CreateStockController())
            {
                ControllerMessage controllerMessage = controller.Update(stockEditDTO);
                if (controllerMessage.IsSuccess)
                {
                    stockEditDTO.OldStockNo = stockEditDTO.NewStockNo;
                    Notify();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }

        private void OnDelete(StockEditDTO stockEditDTO, StockEditViewModel viewModel)
        {
            using (IStockController controller = factory.CreateStockController())
            {
                ControllerMessage controllerMessage = controller.Delete(stockEditDTO.OldStockNo);
                if (controllerMessage.IsSuccess)
                {
                    Notify();
                    viewModel.Clear();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }
    }
}
