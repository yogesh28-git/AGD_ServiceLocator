

namespace ServiceLocator.Events
{
    public class EventService
    {
        public GameEventController<int> OnMapSelected { get; private set; }

        public EventService()
        {
            OnMapSelected = new GameEventController<int>();
        }

    }
}