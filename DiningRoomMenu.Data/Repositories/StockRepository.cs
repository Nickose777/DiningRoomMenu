using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class StockRepository : RepositoryBase<StockEntity>, IStockRepository
    {
        public StockRepository(DiningRoomMenuDbContext context)
            : base(context) { }
    }
}
