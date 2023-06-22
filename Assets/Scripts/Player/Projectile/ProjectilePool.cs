using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Utilities;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectilePool : GenericObjectPool<ProjectileController>
    {
        private ProjectileView projectilePrefab;
        private List<ProjectileScriptableObject> projectileScriptableObjects;

        public ProjectilePool(ProjectileView projectilePrefab, List<ProjectileScriptableObject> projectileScriptableObjects)
        {
            this.projectilePrefab = projectilePrefab;
            this.projectileScriptableObjects = projectileScriptableObjects;
        }

        public ProjectileController GetProjectile(ProjectileType projectileType)
        {
            ProjectileController projectile = GetItem();
            ProjectileScriptableObject scriptableObjectToUse = projectileScriptableObjects.Find(so => so.Type == projectileType);
            projectile.Init(scriptableObjectToUse);
            return projectile;
        }

        protected override ProjectileController CreateItem() => new ProjectileController(projectilePrefab);
    }
}