using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Contracts.Controllers
{
    public interface ICategoryController : IDisposable
    {
        ControllerMessage Add(string categoryName);

        DataControllerMessage<IEnumerable<CategoryDisplayDTO>> GetAll();
    }
}
