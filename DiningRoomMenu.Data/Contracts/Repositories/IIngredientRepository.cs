using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Contracts.Repositories
{
    public interface IIngredientRepository : IRepository<IngredientEntity>
    {
        IngredientEntity Get(string ingredientName);
    }
}
