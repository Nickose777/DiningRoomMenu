using System;
using DiningRoomMenu.Logic.Infrastructure;
using System.Collections.Generic;
using DiningRoomMenu.Logic.DTO.Stock;

namespace DiningRoomMenu.Logic.Contracts.Controllers
{
    public interface IStockController : IDisposable
    {
        ControllerMessage Add(int stockNo);

        ControllerMessage Update(StockEditDTO stockEditDTO);

        DataControllerMessage<StockEditDTO> Get(int stockNo);

        DataControllerMessage<IEnumerable<StockDisplayDTO>> GetAll();
    }
}
