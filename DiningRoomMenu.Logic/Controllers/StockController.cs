using System;
using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.Infrastructure;

namespace DiningRoomMenu.Logic.Controllers
{
    class StockController : ControllerBase, IStockController
    {
        public StockController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public ControllerMessage Add(int stockNo)
        {
            string message = String.Empty;
            bool success = Validate(stockNo, ref message);

            if (success)
            {
                try
                {
                    StockEntity stockEntity = new StockEntity
                    {
                        StockNo = stockNo
                    };

                    unitOfWork.Stocks.Add(stockEntity);
                    unitOfWork.Commit();

                    message = "Stock added";
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ControllerMessage(success, message);
        }

        private bool Validate(int stockNo, ref string message)
        {
            bool isValid = true;

            if (stockNo < 0)
            {
                isValid = false;
                message = "Stock's number cannot be negative";
            }
            else if (unitOfWork.Stocks.GetAll().Any(stock => stock.StockNo == stockNo))
            {
                isValid = false;
                message = "Stock with such number already exists";
            }

            return isValid;
        }
    }
}
