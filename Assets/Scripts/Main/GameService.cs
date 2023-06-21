using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using ServiceLocator.Sound;
using ServiceLocator.Bloon;
using ServiceLocator.Player;
using ServiceLocator.Player.Projectile;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        private MapService mapService;
        public MapService MapService => mapService;

        private WaveService waveService;
        public WaveService WaveService => waveService;
        
        private SoundService soundService;
        public SoundService SoundService => soundService;

        private PlayerService playerService;
        public PlayerService PlayerService => playerService;


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
            mapService = new MapService(mapScriptableObject);
            waveService = new WaveService(waveScriptableObject);
            soundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
            playerService = new PlayerService(playerScriptableObject);

        }
    } 
}