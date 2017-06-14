using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class DishRepository : RepositoryBase<DishEntity>, IDishRepository
    {
        public DishRepository(DiningRoomMenuDbContext context)
            : base(context) { }
    }
}
