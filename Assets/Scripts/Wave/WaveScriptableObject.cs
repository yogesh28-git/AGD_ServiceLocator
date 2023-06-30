using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Wave.Bloon;

namespace ServiceLocator.Wave
{
    [CreateAssetMenu(fileName = "WaveScriptableObject", menuName = "ScriptableObjects/WaveScriptableObject")]
    public class WaveScriptableObject : ScriptableObject
    {
        public float SpawnRate;
        public List<WaveConfiguration> WaveConfigurations;
        public BloonTypeDataMap BloonTypeDataMap;
    }
}
