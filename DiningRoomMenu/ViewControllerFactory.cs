using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu
{
    class ViewControllerFactory : IViewControllerFactory
    {
        private readonly IControllerFactory factory;

        public ViewControllerFactory(IControllerFactory factory)
        {
            this.factory = factory;
        }

        public IStockViewController CreateStockViewController()
        {
            return new StockViewController(factory);
        }

        public ICategoryViewController CreateCategoryViewController()
        {
            return new CategoryViewController(factory);
        }

        public IDishViewController CreateDishViewController()
        {
            return new DishViewController(factory);
        }

        public IIngredientViewController CreateIngredientViewController()
        {
            return new IngredientViewController(factory);
        }

        public IMenuViewController CreateMenuViewController()
        {
            return new MenuViewController(factory);
        }

        public IRecipeViewController CreateRecipeViewController()
        {
            return new RecipeViewController(factory);
        }
    }
}
