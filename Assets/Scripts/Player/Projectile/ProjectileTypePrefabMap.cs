using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Player.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileTypePrefabMap", menuName = "ScriptableObjects/ProjectileTypePrefabMap")]
    public class ProjectileTypePrefabMap : ScriptableObject
    {
        public List<ProjectileConfiguration> projectileConfigurations;
    }

    [System.Serializable]
    public struct ProjectileConfiguration
    {
        public ProjectileType Type;
        public ProjectileView Prefab;
        public ProjectileScriptableObject ProjectileScriptableObject;
    }
}