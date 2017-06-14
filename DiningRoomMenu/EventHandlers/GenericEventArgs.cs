using System;

namespace DiningRoomMenu.EventHandlers
{
    public class GenericEventArgs<T> : EventArgs where T : class
    {
        public T Data { get; set; }

        public GenericEventArgs(T data)
        {
            this.Data = data;
        }
    }
}
