using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;

public class GameService : GenericMonoSingleton<GameService>
{
    // Services:
    private MapService mapService;

    // Scriptable Objects:
    [SerializeField] private MapScriptableObject mapScriptableObject;

    private void Start()
    {
        mapService = new MapService(mapScriptableObject);
    }
}