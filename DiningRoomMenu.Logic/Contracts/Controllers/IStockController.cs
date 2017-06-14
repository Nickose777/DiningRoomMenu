using System;
using DiningRoomMenu.Logic.Infrastructure;

namespace DiningRoomMenu.Logic.Contracts.Controllers
{
    public interface IStockController : IDisposable
    {
        ControllerMessage Add(int stockNo);
    }
}
