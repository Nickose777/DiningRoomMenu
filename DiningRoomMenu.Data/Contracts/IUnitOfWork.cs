using DiningRoomMenu.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }

        IDishRepository Dishes { get; }

        IIngredientRepository Ingredients { get; }

        IRecipeIngredientRepository RecipeIngredients { get; }

        IRecipeRepository Recipes { get; }

        IStockIngredientRepository StockIngredients { get; }

        IStockRepository Stocks { get; }

        void TestConnection();

        void Commit();
    }
}
