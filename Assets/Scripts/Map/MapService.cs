using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ServiceLocator.Player;

namespace ServiceLocator.Map
{
    public class MapService
    {
        private MapScriptableObject mapScriptableObject;

        private Grid currentMap;
        private MapData currentMapData;

        public MapService(MapScriptableObject mapScriptableObject) => this.mapScriptableObject = mapScriptableObject;

        public void LoadMap(int mapId)
        {
            currentMapData = mapScriptableObject.MapDataByLevels.Find(mapData => mapData.MapID == mapId);
            currentMap = Object.Instantiate(currentMapData.MapPrefab);
        }

        public List<Vector3> GetWayPointsForCurrentMap() => currentMapData.WayPoints;

        public Vector3 GetBloonSpawnPositionForCurrentMap() => currentMapData.SpawningPoint;

        public bool TryGetSpawnPosition(Vector3 cursorPosition, out Vector3 spawnPosition)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            Vector3Int mouseToCell = currentMap.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
            Vector3 centerCell = currentMap.GetCellCenterWorld(mouseToCell);

            if (CanSpawnOnPosition(centerCell))
            {
                spawnPosition = centerCell;
                return true;
            }
            else
            {
                spawnPosition = default;
                return false;
            }
        }

        private bool CanSpawnOnPosition(Vector3 cellPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(cellPosition, 0.1f);
            return HasClickedOnTileMap(colliders) && !IsOverLappingMonkey(colliders);
        }

        private bool HasClickedOnTileMap(Collider2D[] colliders)
        {
            foreach(Collider2D collider in colliders)
            {
                if (collider.GetComponent<TilemapCollider2D>() != null)
                    return true;
            }
            return false;
        }

        private bool IsOverLappingMonkey(Collider2D[] colliders)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.GetComponent<MonkeyView>() != null && !collider.isTrigger)
                {
                    return true;
                }
            }
            return false;
        }
    }
}