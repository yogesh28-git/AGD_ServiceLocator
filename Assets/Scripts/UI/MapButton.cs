using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Events;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private int MapId;
        private EventService eventService;

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);
        }

        private void OnMapButtonClicked() =>  eventService.OnMapSelected.InvokeEvent(MapId);
    }
}