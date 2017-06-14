using System.Data.Entity.ModelConfiguration;
using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Configurations
{
    class StockConfiguration : EntityTypeConfiguration<StockEntity>
    {
        public StockConfiguration()
        {
            this.ToTable("Stocks");

            this.HasKey(e => e.Id);
        }
    }
}
