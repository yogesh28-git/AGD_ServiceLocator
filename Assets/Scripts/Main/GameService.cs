using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using ServiceLocator.Sound;
using ServiceLocator.Player;
using ServiceLocator.UI;
using ServiceLocator.Events;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        public MapService MapService { get; private set; }
        public WaveService WaveService { get; private set; }
        public SoundService SoundService { get; private set; }
        public PlayerService PlayerService { get; private set; }
        public EventService EventService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService;


        // Scriptable Objects:
        [SerializeField] private MapScriptableObject mapScriptableObject;
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Scene Referneces:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;
        [SerializeField] private Transform ProjectileContainer;
        [SerializeField] private Transform BloonContainer;

        private void Start()
        {
            EventService = new EventService();
            UIService.Init();
            MapService = new MapService(mapScriptableObject);
            WaveService = new WaveService(waveScriptableObject, BloonContainer);
            SoundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
            PlayerService = new PlayerService(playerScriptableObject, ProjectileContainer);
        }

        private void Update()
        {
            PlayerService.Update();
        }
    } 
}