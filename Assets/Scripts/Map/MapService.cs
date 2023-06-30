using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ServiceLocator.Main;
using ServiceLocator.Player;

namespace ServiceLocator.Map
{
    public class MapService
    {
        private MapScriptableObject mapScriptableObject;

        private Grid currentGrid;
        private Tilemap currentTileMap;
        private MapData currentMapData;

        public MapService(MapScriptableObject mapScriptableObject)
        {
            this.mapScriptableObject = mapScriptableObject;
            GameService.Instance.EventService.OnMapSelected.AddListener(LoadMap);
        }

        public void LoadMap(int mapId)
        {
            currentMapData = mapScriptableObject.MapDataByLevels.Find(mapData => mapData.MapID == mapId);
            currentGrid = Object.Instantiate(currentMapData.MapPrefab);
            currentTileMap = currentGrid.GetComponentInChildren<Tilemap>();
        }

        public List<Vector3> GetWayPointsForCurrentMap() => currentMapData.WayPoints;

        public Vector3 GetBloonSpawnPositionForCurrentMap() => currentMapData.SpawningPoint;

        public bool TryGetSpawnPosition(Vector3 cursorPosition, out Vector3 spawnPosition)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            Vector3Int mouseToCell = currentGrid.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
            Vector3 centerCell = currentGrid.GetCellCenterWorld(mouseToCell);

            if (CanSpawnOnPosition(centerCell, mouseToCell))
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

        private bool CanSpawnOnPosition(Vector3 centerCell, Vector3Int cellPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(centerCell, 0.1f);
            return InisdeTilemapBounds(cellPosition) && HasClickedOnEmptyTile(colliders) && !IsOverLappingMonkey(colliders);
        }

        private bool InisdeTilemapBounds(Vector3Int mouseToCell)
        {
            BoundsInt tilemapBounds = currentTileMap.cellBounds;
            return tilemapBounds.Contains(mouseToCell);
        }

        private bool HasClickedOnEmptyTile(Collider2D[] colliders)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<TilemapCollider2D>() != null)
                    return false;
            }
            return true;
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