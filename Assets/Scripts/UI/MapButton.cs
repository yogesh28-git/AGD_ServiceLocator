using ServiceLocator.Events;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private EventService eventService;
        [SerializeField] private int MapId;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);

        private void OnMapButtonClicked() => eventService.OnMapSelected.InvokeEvent(MapId);
    }
}