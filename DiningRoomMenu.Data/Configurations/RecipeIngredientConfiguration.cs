using System.Data.Entity.ModelConfiguration;
using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Configurations
{
    class RecipeIngredientConfiguration : EntityTypeConfiguration<RecipeIngredientEntity>
    {
        public RecipeIngredientConfiguration()
        {
            this.ToTable("RecipeIngredient");

            this.HasKey(e => new { e.IngredientId, e.RecipeId });

            this.HasRequired(e => e.Ingredient)
                .WithMany(e => e.RecipeIngredients)
                .HasForeignKey(e => e.IngredientId);
            this.HasRequired(e => e.Recipe)
                .WithMany(e => e.RecipeIngredients)
                .HasForeignKey(e => e.RecipeId);
        }
    }
}
