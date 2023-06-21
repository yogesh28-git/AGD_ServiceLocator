using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Bloon;

namespace ServiceLocator.Bloon
{
    [CreateAssetMenu(fileName = "BloonTypePrefabMap", menuName = "ScriptableObjects/BloonTypePrefabMap")]
    public class BloonTypeDataMap : ScriptableObject
    {
        public BloonView BloonPrefab;
        public List<BloonConfiguration> BloonConfigurations;
    }

    [System.Serializable]
    public struct BloonConfiguration
    {
        public BloonType BloonType;
        public BloonScriptableObject BloonScriptableObject;
    }
}