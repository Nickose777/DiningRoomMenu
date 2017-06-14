using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Contracts.Repositories
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        CategoryEntity Get(string name);
    }
}
