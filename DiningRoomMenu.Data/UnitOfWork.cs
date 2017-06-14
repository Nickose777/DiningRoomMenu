using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Data.Contracts.Repositories;
using DiningRoomMenu.Data.Repositories;

namespace DiningRoomMenu.Data
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly DiningRoomMenuDbContext context;

        private ICategoryRepository categories;
        private IDishRepository dishes;
        private IIngredientRepository ingredients;
        private IRecipeIngredientRepository recipeIngredients;
        private IRecipeRepository recipes;
        private IStockIngredientRepository stockIngredients;
        private IStockRepository stocks;

        public UnitOfWork(DiningRoomMenuDbContext context)
        {
            this.context = context;
        }

        public ICategoryRepository Categories
        {
            get { return categories ?? (categories = new CategoryRepository(context)); }
        }

        public IDishRepository Dishes
        {
            get { return dishes ?? (dishes = new DishRepository(context)); }
        }

        public IIngredientRepository Ingredients
        {
            get { return ingredients ?? (ingredients = new IngredientRepository(context)); }
        }

        public IRecipeIngredientRepository RecipeIngredients
        {
            get { return recipeIngredients ?? (recipeIngredients = new RecipeIngredientRepository(context)); }
        }

        public IRecipeRepository Recipes
        {
            get { return recipes ?? (recipes = new RecipeRepository(context)); }
        }

        public IStockIngredientRepository StockIngredients
        {
            get { return stockIngredients ?? (stockIngredients = new StockIngredientRepository(context)); }
        }

        public IStockRepository Stocks
        {
            get { return stocks ?? (stocks = new StockRepository(context)); }
        }

        public void TestConnection()
        {
            context.Database.Connection.Open();
            context.Database.Connection.Close();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
