using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class RecipeRepository : RepositoryBase<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(DiningRoomMenuDbContext context)
            : base(context) { }
    }
}
