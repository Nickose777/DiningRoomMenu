using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Contracts.Repositories
{
    public interface IDishRepository : IRepository<DishEntity>
    {
        DishEntity Get(string name);
    }
}
