/**
 * This script demonstrates implementation of the Observer Pattern.
 *  If you're interested in learning about Observer Pattern, 
 *  you can find a dedicated course on Outscal's website.
 *  Link: https://outscal.com/lms/course/horror-escape-game-using-observer-pattern-using-unity/module/6465e1084c39da1772f30cf9?chapterId=6465e17f4c39da1772f30d09&materialId=6465e3374c39da1772f31bc3
 **/

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