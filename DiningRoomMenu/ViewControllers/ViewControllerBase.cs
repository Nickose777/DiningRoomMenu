using System.Collections.Generic;
using DiningRoomMenu.Contracts;
using DiningRoomMenu.Logic.Contracts;

namespace DiningRoomMenu.ViewControllers
{
    public abstract class ViewControllerBase
    {
        private List<IObserver> observers;
        protected readonly IControllerFactory factory;

        public ViewControllerBase(IControllerFactory factory)
        {
            this.factory = factory;
            observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update();
            }
        }
    }
}
