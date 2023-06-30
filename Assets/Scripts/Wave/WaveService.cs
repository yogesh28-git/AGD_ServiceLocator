using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Wave.Bloon;
using System.Threading.Tasks;
using ServiceLocator.Main;

namespace ServiceLocator.Wave
{
    public class WaveService
    {
        private WaveScriptableObject waveScriptableObject;
        private BloonPool bloonPool;

        private int currentMapID;
        private List<WaveData> waveDatas;
        private int currentWaveId;
        private List<BloonController> activeBloons;

        public int CurrentWaveId => currentWaveId;
        public List<WaveData> WaveDatas { get { return waveDatas; } set { waveDatas = value; } }

        public WaveService(WaveScriptableObject waveScriptableObject, Transform bloonContainer)
        {
            this.waveScriptableObject = waveScriptableObject;
            bloonPool = new BloonPool(waveScriptableObject.BloonTypeDataMap.BloonPrefab, waveScriptableObject.BloonTypeDataMap.BloonScriptableObjects, bloonContainer);
            activeBloons = new List<BloonController>();
        }

        public void LoadWaveDataForMap(int mapId)
        {
            currentMapID = mapId;
            waveDatas = waveScriptableObject.WaveConfigurations.Find(config => config.MapID == mapId).WaveDatas;
            currentWaveId = 0;
            GameService.Instance.UIService.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);
        }

        public void StarNextWave()
        {
            currentWaveId++;
            var bloonsToSpawn = waveDatas.Find(waveData => waveData.WaveID == currentWaveId).ListOfBloons;
            var spawnPosition = GameService.Instance.MapService.GetBloonSpawnPositionForCurrentMap();
            SpawnBloons(bloonsToSpawn, spawnPosition, 0);
        }

        public async void SpawnBloons(List<BloonType> bloonsToSpawn, Vector3 spawnPosition, int startingWaypointIndex)
        {
            foreach(BloonType bloonType in bloonsToSpawn)
            {
                BloonController bloon = bloonPool.GetBloon(bloonType);
                bloon.SetPosition(spawnPosition);
                bloon.SetWayPoints(GameService.Instance.MapService.GetWayPointsForCurrentMap(), startingWaypointIndex);
                
                activeBloons.Add(bloon);
                await Task.Delay(Mathf.RoundToInt(waveScriptableObject.SpawnRate * 1000));
            }
        }

        public void RemoveBloon(BloonController bloon)
        {
            bloonPool.ReturnItem(bloon);
            activeBloons.Remove(bloon);
            if (HasCurrentWaveEnded())
            {
                GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.WaveComplete);
                GameService.Instance.UIService.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);
                if(IsLevelWon())
                {
                    GameService.Instance.UIService.UpdateGameEndUI(true);
                }
                else
                    GameService.Instance.UIService.SetNextWaveButton(true);
            }
        }

        public bool HasCurrentWaveEnded() => activeBloons.Count == 0;

        private bool IsLevelWon() => currentWaveId >= waveDatas.Count;
    }
}