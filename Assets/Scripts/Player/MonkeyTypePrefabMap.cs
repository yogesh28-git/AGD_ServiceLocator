using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Player
{
    [CreateAssetMenu(fileName = "MonkeyTypePrefabMap", menuName = "ScriptableObjects/MonkeyTypePrefabMap")]
    public class MonkeyTypePrefabMap : ScriptableObject
    {
        public List<MonkeyConfiguration> monkeyConfigurations;
    }

    public struct MonkeyConfiguration
    {
        public MonkeyType Type;
        public MonkeyView Prefab;
        public MonkeyScriptableObject MonkeyScriptableObject;
    }
}