using DiningRoomMenu.Data;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic
{
    class ControllerFactory : IControllerFactory
    {
        private readonly Func<IUnitOfWork> CreateUnitOfWork;

        public ControllerFactory(Func<IUnitOfWork> CreateUnitOfWork)
        {
            this.CreateUnitOfWork = CreateUnitOfWork;
        }

        public IConnectionController CreateConnectionController()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new ConnectionController(unitOfWork);
        }

        public ICategoryController CreateCategoryController()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new CategoryController(unitOfWork);
        }

        public IStockController CreateStockController()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new StockController(unitOfWork);
        }
    }
}
