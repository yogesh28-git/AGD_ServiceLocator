using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using ServiceLocator.Wave.Bloon;
using ServiceLocator.Utilities;
using ServiceLocator.Events;
using ServiceLocator.UI;
using ServiceLocator.Map;
using ServiceLocator.Sound;

namespace ServiceLocator.Wave
{
    public class WaveService : GenericMonoSingleton<WaveService>
    {
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private Transform bloonContainer;
        private BloonPool bloonPool;

        private int currentWaveId;
        private List<WaveData> waveDatas;
        private List<BloonController> activeBloons;

        private void Start()
        {
            bloonPool = new BloonPool(waveScriptableObject.BloonTypeDataMap.BloonPrefab, waveScriptableObject.BloonTypeDataMap.BloonScriptableObjects, bloonContainer);
            activeBloons = new List<BloonController>();
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => EventService.Instance.OnMapSelected.AddListener(LoadWaveDataForMap);

        private void LoadWaveDataForMap(int mapId)
        {
            currentWaveId = 0;
            waveDatas = waveScriptableObject.WaveConfigurations.Find(config => config.MapID == mapId).WaveDatas;
            UIService.Instance.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);
        }

        public void StarNextWave()
        {
            currentWaveId++;
            var bloonsToSpawn = waveDatas.Find(waveData => waveData.WaveID == currentWaveId).ListOfBloons;
            var spawnPosition = MapService.Instance.GetBloonSpawnPositionForCurrentMap();
            SpawnBloons(bloonsToSpawn, spawnPosition, 0);
        }

        public async void SpawnBloons(List<BloonType> bloonsToSpawn, Vector3 spawnPosition, int startingWaypointIndex)
        {
            foreach(BloonType bloonType in bloonsToSpawn)
            {
                BloonController bloon = bloonPool.GetBloon(bloonType);
                bloon.SetPosition(spawnPosition);
                bloon.SetWayPoints(MapService.Instance.GetWayPointsForCurrentMap(), startingWaypointIndex);
                
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
                SoundService.Instance.PlaySoundEffects(Sound.SoundType.WaveComplete);
                UIService.Instance.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);

                if(IsLevelWon())
                    UIService.Instance.UpdateGameEndUI(true);
                else
                    UIService.Instance.SetNextWaveButton(true);
            }
        }

        private bool HasCurrentWaveEnded() => activeBloons.Count == 0;

        private bool IsLevelWon() => currentWaveId >= waveDatas.Count;
    }
}