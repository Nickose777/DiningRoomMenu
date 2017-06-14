using System.Data.Entity.ModelConfiguration;
using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Configurations
{
    class CategoryConfiguration : EntityTypeConfiguration<CategoryEntity>
    {
        public CategoryConfiguration()
        {
            this.ToTable("Categories");

            this.HasKey(e => e.Id);
        }
    }
}
