using DiningRoomMenu.Logic.DTO.Recipe;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Contracts.Controllers
{
    public interface IRecipeController : IDisposable
    {
        ControllerMessage Add(RecipeAddDTO recipeAddDTO);
    }
}
