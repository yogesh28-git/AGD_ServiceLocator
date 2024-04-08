using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private int MapId;
        [SerializeField] private GameService gameService;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);

        // To Learn more about Events and Observer Pattern, check out the course list here: https://outscal.com/courses
        private void OnMapButtonClicked() =>  gameService.EventService.OnMapSelected.InvokeEvent(MapId);
    }
}