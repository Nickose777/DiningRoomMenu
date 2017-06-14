using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class StockIngredientRepository : RepositoryBase<StockIngredientEntity>, IStockIngredientRepository
    {
        public StockIngredientRepository(DiningRoomMenuDbContext context)
            : base(context) { }

        public override StockIngredientEntity Get(int id)
        {
            throw new System.InvalidOperationException();
        }
    }
}
