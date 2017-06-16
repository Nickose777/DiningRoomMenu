using DiningRoomMenu.Logic.DTO.Ingredient;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Contracts.Controllers
{
    public interface IIngredientController : IDisposable
    {
        ControllerMessage Add(string ingredientName);

        ControllerMessage Update(IngredientEditDTO ingredientEditDTO);

        DataControllerMessage<IngredientEditDTO> Get(string ingredientName);

        DataControllerMessage<IEnumerable<IngredientDisplayDTO>> GetAll();
    }
}
