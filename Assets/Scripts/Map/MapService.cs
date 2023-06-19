using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapService
{
    private MapScriptableObject mapScriptableObject;

    public MapService(MapScriptableObject mapScriptableObject) => this.mapScriptableObject = mapScriptableObject;

    public List<Vector3> GetWayPointsForMap(int mapId) => mapScriptableObject.MapDataByLevels.Find(mapData => mapData.MapID == mapId).WayPoints;

    public Vector3 GetSpawnPositionForMap(int mapId) => mapScriptableObject.MapDataByLevels.Find(mapData => mapData.MapID == mapId).SpawningPoint;
}