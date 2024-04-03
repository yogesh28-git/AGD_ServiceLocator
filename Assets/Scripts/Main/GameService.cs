using ServiceLocator.Utilities;
using ServiceLocator.Player;
using UnityEngine;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Wave;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService playerService { get; private set; }
    public SoundService soundService { get; private set; }
    public EventService eventService { get; private set; }
    public MapService mapService { get; private set; }
    public WaveService waveService { get; private set; }

    [Header( "Sound Service" )]
    [SerializeField] private UIService uiService;
    public UIService UIService { get { return uiService; } }

    [Space(10)]

    [Header("Player Service")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    [Space(10)]

    [Header("Sound Service")]
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    [Header( "Map Service" )]
    [SerializeField] private MapScriptableObject mapScriptableObject;

    [Header( "Wave Service" )]
    [SerializeField] private WaveScriptableObject waveScriptableObject;

    protected override void Awake( )
    {
        base.Awake( );
        eventService = new EventService( );
    }

    private void Start( )
    {
        playerService = new PlayerService( playerScriptableObject );
        mapService = new MapService( mapScriptableObject );
        waveService = new WaveService( waveScriptableObject );
        soundService = new SoundService( soundScriptableObject, audioEffects, backgroundMusic );
    }

    private void Update()
    {
        playerService.Update( );
    }
}
