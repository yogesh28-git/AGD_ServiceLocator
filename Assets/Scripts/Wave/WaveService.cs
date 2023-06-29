using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Bloon;
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
            SpawnBloons(waveDatas.Find(waveData => waveData.WaveID == currentWaveId));
        }

        private async void SpawnBloons(WaveData waveData)
        {
            foreach(BloonType bloonType in waveData.ListOfBloons)
            {
                BloonController bloon = bloonPool.GetBloon(bloonType);
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
                    // TODO:
                    // Level Won.
                    Debug.Log("Level Won");
                }
                else
                    GameService.Instance.UIService.SetNextWaveButton(true);
            }
        }

        public bool HasCurrentWaveEnded() => activeBloons.Count == 0;

        private bool IsLevelWon() => currentWaveId >= waveDatas.Count;
    }
}