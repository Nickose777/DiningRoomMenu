using DiningRoomMenu.Contracts.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Contracts
{
    public interface IViewControllerFactory
    {
        ICategoryViewController CreateCategoryViewController();

        IDishViewController CreateDishViewController();

        IIngredientViewController CreateIngredientViewController();

        IMenuViewController CreateMenuViewController();

        IRecipeViewController CreateRecipeViewController();

        IStockViewController CreateStockViewController();
    }
}
