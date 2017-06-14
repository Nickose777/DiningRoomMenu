using DiningRoomMenu.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Controllers
{
    public abstract class ControllerBase : IDisposable
    {
        protected readonly IUnitOfWork unitOfWork;

        public ControllerBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
