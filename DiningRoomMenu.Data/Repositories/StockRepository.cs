using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class StockRepository : RepositoryBase<StockEntity>, IStockRepository
    {
        public StockRepository(DiningRoomMenuDbContext context)
            : base(context) { }

        public StockEntity GetByNo(int stockNo)
        {
            return context.Stocks.SingleOrDefault(stock => stock.StockNo == stockNo);
        }
    }
}
