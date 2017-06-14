namespace DiningRoomMenu.Logic.Infrastructure
{
    public class ControllerMessage
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public ControllerMessage(bool success, string message)
        {
            this.IsSuccess = success;
            this.Message = message;
        }
    }
}
