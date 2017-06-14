namespace DiningRoomMenu.EventHandlers
{
    public delegate void GenericEventHandler<T>(object sender, GenericEventArgs<T> e) where T : class;
}
