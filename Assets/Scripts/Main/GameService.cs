using ServiceLocator.Utilities;
using ServiceLocator.Player;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService playerService { get; private set; }


    [Header("Player Service")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    void Start()
    {
        playerService = new PlayerService( playerScriptableObject );
    }

    void Update()
    {
        playerService.Update( );
    }
}
