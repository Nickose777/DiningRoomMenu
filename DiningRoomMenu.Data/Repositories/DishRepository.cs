using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class DishRepository : RepositoryBase<DishEntity>, IDishRepository
    {
        public DishRepository(DiningRoomMenuDbContext context)
            : base(context) { }

        public DishEntity Get(string name)
        {
            return context.Dishes.SingleOrDefault(dish => dish.Name == name);
        }
    }
}
