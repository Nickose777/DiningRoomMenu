using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;
using System.Collections.Generic;

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

        public IEnumerable<StockEntity> GetAllWithIngredient(string ingredientName)
        {
            return context.Stocks
                .Where(stock => 
                    stock.StockIngredients
                    .Where(si => si.Count > 0)
                    .Where(si => si.Ingredient.Name == ingredientName)
                    .Count() > 0
                    );
        }
    }
}
