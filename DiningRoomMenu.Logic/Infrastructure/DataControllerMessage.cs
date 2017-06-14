namespace DiningRoomMenu.Logic.Infrastructure
{
    public class DataControllerMessage<T> : ControllerMessage where T : class
    {
        public T Data { get; private set; }

        public DataControllerMessage(bool success, string message, T data)
            : base(success, message)
        {
            this.Data = data;
        }
    }
}
