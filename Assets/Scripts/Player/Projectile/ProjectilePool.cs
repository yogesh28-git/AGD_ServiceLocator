using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Utilities;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectilePool : GenericObjectPool<ProjectileController>
    {
        List<ProjectileConfiguration> projectileConfigurations;

        public ProjectilePool(List<ProjectileConfiguration> projectileConfigurations) => this.projectileConfigurations = projectileConfigurations;

        public ProjectileController GetProjectile(ProjectileType projectileType)
        {
            ProjectileConfiguration configToUse = projectileConfigurations.Find(config => config.Type == projectileType);
            ProjectileController projectile = GetItem();
            projectile.Init(configToUse.Prefab, configToUse.ProjectileScriptableObject);
            return projectile;
        }

        protected override ProjectileController CreateItem() => new ProjectileController();
    }
}