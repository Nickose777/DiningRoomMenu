using System.Data.Entity.ModelConfiguration;
using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Configurations
{
    class StockIngredientConfiguration : EntityTypeConfiguration<StockIngredientEntity>
    {
        public StockIngredientConfiguration()
        {
            this.ToTable("StockIngredient");

            this.HasKey(e => new { e.StockId, e.IngredientId });

            this.HasRequired(e => e.Ingredient)
                .WithMany(e => e.StockIngredients)
                .HasForeignKey(e => e.IngredientId);
            this.HasRequired(e => e.Stock)
                .WithMany(e => e.StockIngredients)
                .HasForeignKey(e => e.StockId);
        }
    }
}
