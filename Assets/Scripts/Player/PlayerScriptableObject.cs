using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player.Projectile;

namespace ServiceLocator.Player
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public int Health;
        public int Money;
        public List<MonkeyConfiguration> MonkeyConfigurations;
        public List<ProjectileConfiguration> ProjectileConfigurations;
    }

    [System.Serializable]
    public struct MonkeyConfiguration
    {
        public MonkeyType Type;
        public MonkeyView Prefab;
        public MonkeyScriptableObject MonkeyScriptableObject;
    }

    [System.Serializable]
    public struct ProjectileConfiguration
    {
        public ProjectileType Type;
        public ProjectileView Prefab;
        public ProjectileScriptableObject ProjectileScriptableObject;
    }
}
