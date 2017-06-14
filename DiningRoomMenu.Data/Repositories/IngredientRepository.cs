using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class IngredientRepository : RepositoryBase<IngredientEntity>, IIngredientRepository
    {
        public IngredientRepository(DiningRoomMenuDbContext context)
            : base(context) { }
    }
}
