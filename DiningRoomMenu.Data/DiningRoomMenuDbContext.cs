using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Data
{
    public class DiningRoomMenuDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<StockEntity> Stocks { get; set; }

        public DbSet<IngredientEntity> Ingredients { get; set; }

        public DbSet<DishEntity> Dishes { get; set; }

        public DbSet<RecipeEntity> Recipes { get; set; }

        public DbSet<RecipeIngredientEntity> RecipeIngredients { get; set; }

        public DbSet<StockIngredientEntity> StockIngredients { get; set; }

        public DiningRoomMenuDbContext()
            : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new DishConfiguration());
            modelBuilder.Configurations.Add(new IngredientConfiguration());
            modelBuilder.Configurations.Add(new RecipeConfiguration());
            modelBuilder.Configurations.Add(new RecipeIngredientConfiguration());
            modelBuilder.Configurations.Add(new StockConfiguration());
            modelBuilder.Configurations.Add(new StockIngredientConfiguration());
        }
    }
}
