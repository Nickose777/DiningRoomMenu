using System.Data.Entity.ModelConfiguration;
using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Configurations
{
    class IngredientConfiguration : EntityTypeConfiguration<IngredientEntity>
    {
        public IngredientConfiguration()
        {
            this.ToTable("Ingredients");

            this.HasKey(e => e.Id);
        }
    }
}
