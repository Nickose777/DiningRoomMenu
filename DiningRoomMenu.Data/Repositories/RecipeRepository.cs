using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class RecipeRepository : RepositoryBase<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(DiningRoomMenuDbContext context)
            : base(context) { }

        public bool Exists(string recipeName)
        {
            return Get(recipeName) != null;
        }

        public RecipeEntity Get(string recipeName)
        {
            return context.Recipes.SingleOrDefault(recipe => recipe.Name == recipeName);
        }
    }
}
