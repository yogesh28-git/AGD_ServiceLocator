using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Wave.Bloon;
using ServiceLocator.Player.Projectile;
using ServiceLocator.Sound;

namespace ServiceLocator.Player
{
    public class MonkeyController
    {
        private MonkeyView monkeyView;
        private MonkeyScriptableObject monkeyScriptableObject;
        private ProjectilePool projectilePool;

        private float attackTimer;

        public MonkeyController(MonkeyScriptableObject monkeyScriptableObject, ProjectilePool projectilePool)
        {
            monkeyView = Object.Instantiate(monkeyScriptableObject.Prefab);
            monkeyView.SetController(this);
            monkeyView.SetTriggerRadius(monkeyScriptableObject.Range);
            
            this.monkeyScriptableObject = monkeyScriptableObject;
            this.projectilePool = projectilePool;
            ResetAttackTimer();
        }

        public void SetPosition(Vector3 positionToSet) => monkeyView.transform.position = positionToSet;

        public bool CanAttackBloon(BloonType bloonType) => monkeyScriptableObject.AttackableBloons.Contains(bloonType);

        private void ResetAttackTimer() => attackTimer = monkeyScriptableObject.AttackRate;
    }
}