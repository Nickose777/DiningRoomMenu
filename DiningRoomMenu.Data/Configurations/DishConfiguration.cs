using System.Data.Entity.ModelConfiguration;
using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Configurations
{
    class DishConfiguration : EntityTypeConfiguration<DishEntity>
    {
        public DishConfiguration()
        {
            this.ToTable("Dishes");

            this.HasKey(e => e.Id);

            this.HasRequired(e => e.Category)
                .WithMany(e => e.Dishes)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
