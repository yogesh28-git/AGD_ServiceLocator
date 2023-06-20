using UnityEngine;

namespace ServiceLocator.Bloon
{
    [CreateAssetMenu(fileName = "BloonScriptableObject", menuName = "ScriptableObects/BloonScriptableObject")]
    public class BloonScriptableObject : ScriptableObject
    {
        public int Health;
        public int Damage;
        public int Reward;
        public float Speed;
    }
}