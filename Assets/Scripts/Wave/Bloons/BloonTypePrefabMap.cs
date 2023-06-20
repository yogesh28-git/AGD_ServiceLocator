using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Bloon;

namespace ServiceLocator.Bloon
{
    [CreateAssetMenu(fileName = "BloonTypePrefabMap", menuName = "ScriptableObjects/BloonTypePrefabMap")]
    public class BloonTypePrefabMap : ScriptableObject
    {
        public List<BloonConfiguration> BloonConfigurations;
    }

    [System.Serializable]
    public struct BloonConfiguration
    {
        public BloonType BloonType;
        public BloonView BloonPrefab;
        public BloonScriptableObject BloonScriptableObject;
    }
}