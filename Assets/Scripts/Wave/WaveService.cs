using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Bloon;

namespace ServiceLocator.Wave
{
    public class WaveService
    {
        private WaveScriptableObject waveScriptableObject;
        private BloonPool bloonPool;

        private List<WaveData> waveDatas;
        private int currentWaveId;
        private int activeBloons;

        public WaveService(WaveScriptableObject waveScriptableObject, List<BloonConfiguration> bloonConfigurations)
        {
            this.waveScriptableObject = waveScriptableObject;
            bloonPool = new BloonPool(bloonConfigurations);
        }

        public void LoadWaveDataForMap(int mapId) => waveDatas = waveScriptableObject.WaveConfigurations.Find(config => config.MapID == mapId).WaveDatas;

        public void StarWave(int waveId)
        {

        }

        public bool HasCurrentWaveEnded()
        {
            throw new System.NotImplementedException();
        }

        private void SpawnBloonsForCurrentWave()
        {

        }

        private bool IsLevelWon()
        {
            throw new System.NotImplementedException();
        }
    }
}