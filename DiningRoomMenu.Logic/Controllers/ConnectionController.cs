using System;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.Infrastructure;

namespace DiningRoomMenu.Logic.Controllers
{
    class ConnectionController : ControllerBase, IConnectionController
    {
        public ConnectionController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public ControllerMessage TestConnection()
        {
            bool success = true;
            string message = "Connected to database";

            try
            {
                unitOfWork.TestConnection();
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new ControllerMessage(success, message);
        }
    }
}
