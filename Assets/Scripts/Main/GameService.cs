using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using ServiceLocator.Sound;
using ServiceLocator.Player;
using ServiceLocator.UI;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        public EventService EventService { get; private set; }
        public MapService MapService { get; private set; }
        public WaveService WaveService { get; private set; }
        public SoundService SoundService { get; private set; }
        public PlayerService PlayerService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;


        // Scriptable Objects:
        [SerializeField] private MapScriptableObject mapScriptableObject;
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Scene Referneces:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;

        private void Start()
        {
            CreateServices( );
            InitServices( );
        }

        private void CreateServices( )
        {
            EventService = new EventService( );
            MapService = new MapService( mapScriptableObject );
            WaveService = new WaveService( waveScriptableObject );
            SoundService = new SoundService( soundScriptableObject, SFXSource, BGSource );
            PlayerService = new PlayerService( playerScriptableObject );
        }

        private void InitServices( )
        {
            PlayerService.Init( UIService, MapService, SoundService );
            WaveService.Init(UIService, MapService, SoundService, EventService, PlayerService);
            MapService.Init( EventService );
            UIService.Init( WaveService, EventService, PlayerService );
        }

        private void Update()
        {
            PlayerService.Update();
        }
    }
}