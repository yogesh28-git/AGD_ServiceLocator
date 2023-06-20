using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectileController
    {
        private ProjectileView projectileView;
        private ProjectileScriptableObject projectileScriptableObject;

        public void Init(ProjectileView projectilePrefab, ProjectileScriptableObject projectileScriptableObject)
        {
            ResetProjectile();
            projectileView = Object.Instantiate(projectilePrefab);
            projectileView.SetController(this);
            this.projectileScriptableObject = projectileScriptableObject;
        }

        private void ResetProjectile()
        {
            if(projectileView != null)
            {
                Object.Destroy(projectileView.gameObject);
            }
        }

        public void SetPosition(Vector3 spawnPosition) => projectileView.transform.position = spawnPosition;

        public void UpdateProjectileMotion()
        {
            
        }

        public void OnProjectileEnteredTrigger(GameObject collidedObject)
        {
            
        }
    }
}