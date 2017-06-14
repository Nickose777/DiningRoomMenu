using DiningRoomMenu.Data;
using DiningRoomMenu.Logic.Contracts;

namespace DiningRoomMenu.Logic
{
    public static class Factory
    {
        public static IControllerFactory CreateFactory()
        {
            return new ControllerFactory(() => UnitOfWorkFactory.CreateUnitOfWork());
        }
    }
}
