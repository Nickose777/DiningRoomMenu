using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Contracts.Repositories
{
    public interface IRecipeRepository : IRepository<RecipeEntity>
    {

        RecipeEntity Get(string recipeName);

        bool Exists(string recipeName);
    }
}
