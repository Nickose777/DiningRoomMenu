using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Contracts.Repositories
{
    public interface IStockRepository : IRepository<StockEntity>
    {
        StockEntity GetByNo(int stockNo);
    }
}
