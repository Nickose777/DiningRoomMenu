using DiningRoomMenu.Data.Contracts;

namespace DiningRoomMenu.Data
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new DiningRoomMenuDbContext());
        }
    }
}
