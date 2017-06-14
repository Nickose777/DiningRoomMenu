using System;
using DiningRoomMenu.Logic.Infrastructure;
using System.Collections.Generic;
using DiningRoomMenu.Logic.DTO.Stock;

namespace DiningRoomMenu.Logic.Contracts.Controllers
{
    public interface IStockController : IDisposable
    {
        ControllerMessage Add(int stockNo);

        DataControllerMessage<IEnumerable<StockDisplayDTO>> GetAll();
    }
}
