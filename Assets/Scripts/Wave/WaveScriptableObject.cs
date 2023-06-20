using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Wave
{
    [CreateAssetMenu(fileName = "WaveScriptableObject", menuName = "ScriptableObjects/WaveScriptableObject")]
    public class WaveScriptableObject : ScriptableObject
    {
        public float spawnRate;
        public List<WaveConfiguration> WaveConfigurations;
    }
}
