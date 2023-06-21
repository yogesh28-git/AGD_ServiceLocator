using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player.Projectile;

namespace ServiceLocator.Player
{
    [CreateAssetMenu(fileName = "MonkeyScriptableObject", menuName = "ScriptableObjects/MonkeyScriptableObject")]
    public class MonkeyScriptableObject : ScriptableObject
    {
        public float RotationSpeed;
        public ProjectileType projectileType;
        public int Cost;
    }
}