using UnityEngine;

/**  This script demonstrates implementation of the Observer Pattern.
  *  If you're interested in learning about Observer Pattern, 
  *  you can find a dedicated course on Outscal's website.
  *  Link: https://outscal.com/courses
  **/

namespace ServiceLocator.Events
{
    public class EventService : MonoBehaviour
    {
        private static EventService instance;

        public static EventService Instance { get { return instance; } private set { } }
        public GameEventController<int> OnMapSelected { get; private set; }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy( gameObject );
            }

            OnMapSelected = new GameEventController<int>();
        }
        
    }
}