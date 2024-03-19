using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ServiceLocator.Player;
using ServiceLocator.Events;

namespace ServiceLocator.Map
{
    public class MapService : MonoBehaviour
    {
        [SerializeField] private MapScriptableObject mapScriptableObject;

        private Grid currentGrid;
        private Tilemap currentTileMap;
        private MapData currentMapData;
        private SpriteRenderer tileOverlay;

        private static MapService instance;

        public static MapService Instance { get { return instance; } private set { } }

        private void Awake( )
        {
            if ( instance == null )
            {
                instance = this;
            }
            else
            {
                Destroy( gameObject );
            }
        }

        private void Start()
        {
            SubscribeToEvents();
            tileOverlay = Object.Instantiate(mapScriptableObject.TileOverlay).GetComponent<SpriteRenderer>();
            ResetTileOverlay();
        }

        private void SubscribeToEvents() => EventService.Instance.OnMapSelected.AddListener(LoadMap);

        private void LoadMap(int mapId)
        {
            currentMapData = mapScriptableObject.MapDatas.Find(mapData => mapData.MapID == mapId);
            currentGrid = Instantiate(currentMapData.MapPrefab);
            currentTileMap = currentGrid.GetComponentInChildren<Tilemap>();
        }

        public List<Vector3> GetWayPointsForCurrentMap() => currentMapData.WayPoints;

        public Vector3 GetBloonSpawnPositionForCurrentMap() => currentMapData.SpawningPoint;

        private void ResetTileOverlay() => SetTileOverlayColor(TileOverlayColor.TRANSPARENT);

        private void SetTileOverlayColor(TileOverlayColor colorToSet)
        {
            switch (colorToSet)
            {
                case TileOverlayColor.TRANSPARENT:
                    tileOverlay.color = mapScriptableObject.DefaultTileColor;
                    break;
                case TileOverlayColor.SPAWNABLE:
                    tileOverlay.color = mapScriptableObject.SpawnableTileColor;
                    break;
                case TileOverlayColor.NON_SPAWNABLE:
                    tileOverlay.color = mapScriptableObject.NonSpawnableTileColor;
                    break;
            }
        }

        public void ValidateSpawnPosition(Vector3 cursorPosition)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            Vector3Int cellPosition = GetCellPosition(mousePosition);
            Vector3 cellCenter = GetCenterOfCell(cellPosition);

            if (CanSpawnOnPosition(cellCenter, cellPosition))
            {
                tileOverlay.transform.position = cellCenter;
                SetTileOverlayColor(TileOverlayColor.SPAWNABLE);
            }
            else
            {
                tileOverlay.transform.position = cellCenter;
                SetTileOverlayColor(TileOverlayColor.NON_SPAWNABLE);
            }
        }

        public bool TryGetMonkeySpawnPosition(Vector3 cursorPosition, out Vector3 spawnPosition)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            Vector3Int cellPosition = GetCellPosition(mousePosition);
            Vector3 cellCenter = GetCenterOfCell(cellPosition);

            ResetTileOverlay();

            if (CanSpawnOnPosition(cellCenter, cellPosition))
            {
                spawnPosition = cellCenter;
                return true;
            }
            else
            {
                spawnPosition = Vector3.zero;
                return false;
            }
        }

        private Vector3Int GetCellPosition(Vector3 mousePosition) => currentGrid.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));

        private Vector3 GetCenterOfCell(Vector3Int cellPosition) => currentGrid.GetCellCenterWorld(cellPosition);

        private bool CanSpawnOnPosition(Vector3 cellCenter, Vector3Int cellPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(cellCenter, 0.1f);
            return InisdeTilemapBounds(cellPosition) && !HasClickedOnObstacle(colliders) && !IsOverLappingMonkey(colliders);
        }

        private bool InisdeTilemapBounds(Vector3Int mouseToCell)
        {
            BoundsInt tilemapBounds = currentTileMap.cellBounds;
            return tilemapBounds.Contains(mouseToCell);
        }

        private bool HasClickedOnObstacle(Collider2D[] colliders)
        {
            foreach (Collider2D collider in colliders)
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
                    return true;
            }
            return false;
        }

        private enum TileOverlayColor
        {
            TRANSPARENT,
            SPAWNABLE,
            NON_SPAWNABLE
        }
    }
}