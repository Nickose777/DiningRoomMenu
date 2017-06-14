using System.Data.Entity.ModelConfiguration;
using DiningRoomMenu.Core.Entities;

namespace DiningRoomMenu.Data.Configurations
{
    class RecipeConfiguration : EntityTypeConfiguration<RecipeEntity>
    {
        public RecipeConfiguration()
        {
            this.ToTable("Recipes");

            this.HasKey(e => e.Id);

            this.HasRequired(e => e.Dish)
                .WithMany(e => e.Recipes)
                .HasForeignKey(e => e.DishId);
        }
    }
}
