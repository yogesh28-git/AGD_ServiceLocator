using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Map
{
    public class MapService
    {
        private MapScriptableObject mapScriptableObject;

        private MapData currentMapData;
        public int CurrentMapId => currentMapData.MapID;


        public MapService(MapScriptableObject mapScriptableObject) => this.mapScriptableObject = mapScriptableObject;

        public void LoadMap(int mapId)
        {
            currentMapData = mapScriptableObject.MapDataByLevels.Find(mapData => mapData.MapID == mapId);
            Object.Instantiate(currentMapData.MapPrefab);
        }

        public List<Vector3> GetWayPointsForCurrentMap() => currentMapData.WayPoints;

        public Vector3 GetSpawnPositionForCurrentMap() => currentMapData.SpawningPoint;

    }
}