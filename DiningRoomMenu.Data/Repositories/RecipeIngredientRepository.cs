using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class RecipeIngredientRepository : RepositoryBase<RecipeIngredientEntity>, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(DiningRoomMenuDbContext context)
            : base(context) { }

        public override RecipeIngredientEntity Get(int id)
        {
            throw new System.InvalidOperationException();
        }
    }
}
