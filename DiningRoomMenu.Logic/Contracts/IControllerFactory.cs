using DiningRoomMenu.Logic.Contracts.Controllers;

namespace DiningRoomMenu.Logic.Contracts
{
    public interface IControllerFactory
    {
        IConnectionController CreateConnectionController();

        ICategoryController CreateCategoryController();

        IStockController CreateStockController();
    }
}
