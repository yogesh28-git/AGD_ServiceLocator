using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Bloon;
using ServiceLocator.Player.Projectile;

namespace ServiceLocator.Player
{
    public class MonkeyController
    {
        private MonkeyView monkeyView;
        private MonkeyScriptableObject monkeyScriptableObject;
        private ProjectilePool projectilePool;

        private BloonController targetedBloon;

        public MonkeyController(MonkeyView monkeyPrefab, MonkeyScriptableObject monkeyScriptableObject, ProjectilePool projectilePool)
        {
            monkeyView = Object.Instantiate(monkeyPrefab);
            monkeyView.SetController(this);
            this.monkeyScriptableObject = monkeyScriptableObject;
            this.projectilePool = projectilePool;
        }

        public void SetPosition(Vector3 positionToSet)
        {
            monkeyView.transform.position = positionToSet;
        }

        public void SetTargetBloon(BloonController bloonToSetTarget)
        {
            targetedBloon = bloonToSetTarget;
        }

        public void UpdateMonkey()
        {
            // Rotate towards the target if any.
            // Shoot at the target according toi the spawn rate.
        }

        private void ShootAtTarget()
        {
            // Use Projectile Pool to shoot.
        }
    }
}