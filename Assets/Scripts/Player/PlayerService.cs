using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player.Projectile;

namespace ServiceLocator.Player
{
    public class PlayerService
    {
        private ProjectilePool projectilePool;
        private List<MonkeyConfiguration> monkeyConfigurations;
        
        private List<MonkeyController> activeMonkeys;
        private int health;
        private int money;

        public PlayerService(List<MonkeyConfiguration> monkeyConfigurations, List<ProjectileConfiguration> projectileConfigurations)
        {
            this.monkeyConfigurations = monkeyConfigurations;
            projectilePool = new ProjectilePool(projectileConfigurations);
        }

        public void Update()
        {
            foreach(MonkeyController monkey in activeMonkeys)
            {
                monkey?.UpdateMonkey();
            }
        }

        public bool CanSpawnMonkeyAt(Vector3 spawnPosition)
        {
            // Check if a monkey can be spawned at the given position or not.
            throw new System.NotImplementedException();
        }

        public void SpawnMonkey(MonkeyType monkeyType, Vector3 spawnPosition)
        {
            // Get configuration of given monkey type and spawn it at the given position.
        }

        public void TakeDamage(int damageToTake)
        {
            health = health - damageToTake <= 0 ? 0 : health - damageToTake;
            // Update Health UI.
            if(health <=0)
            {
                PlayerDeath();
            }
        }

        private void PlayerDeath()
        {
            // Game Over UI.
            // Stop Bloon Spawning.
        }
    }
}