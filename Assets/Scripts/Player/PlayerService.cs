using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player.Projectile;
using UnityEngine.Tilemaps;
using ServiceLocator.Main;
using ServiceLocator.Player.Input;

namespace ServiceLocator.Player
{
    public class PlayerService
    {
        private PlayerScriptableObject playerScriptableObject;
        private ProjectilePool projectilePool;
        private InputService inputService;
        public InputService InputService => inputService;

        private List<MonkeyController> activeMonkeys;
        private int health;
        private int money;

        public PlayerService(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            projectilePool = new ProjectilePool(playerScriptableObject.ProjectilePrefab, playerScriptableObject.ProjectileScriptableObjects);
            inputService = new InputService();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            health = playerScriptableObject.Health;
            money = playerScriptableObject.Money;
            GameService.Instance.UIService.UpdateHealthUI(health);
            GameService.Instance.UIService.UpdateMoneyUI(money);
            activeMonkeys = new List<MonkeyController>();
        }

        public void Update()
        {
            foreach(MonkeyController monkey in activeMonkeys)
            {
                monkey?.UpdateMonkey();
            }

            inputService?.UpdateInputs();
        }

        public void SpawnMonkey(MonkeyType monkeyType, Vector3 spawnPosition)
        {
            MonkeyScriptableObject monkeySO = playerScriptableObject.MonkeyScriptableObjects.Find(so => so.Type == monkeyType);
            MonkeyController monkey = new MonkeyController(monkeySO, projectilePool);
            monkey.SetPosition(spawnPosition);
            activeMonkeys.Add(monkey);

            money -= monkeySO.Cost;
            GameService.Instance.UIService.UpdateMoneyUI(money);
        }

        public void ReturnProjectileToPool(ProjectileController projectileToReturn) => projectilePool.ReturnItem(projectileToReturn);
        
        public void TakeDamage(int damageToTake)
        {
            health = health - damageToTake <= 0 ? 0 : health - damageToTake;
            GameService.Instance.UIService.UpdateHealthUI(health);
            if(health <= 0)
            {
                PlayerDeath();
            }
        }

        public void GetReward(int reward)
        {
            money += reward;
            GameService.Instance.UIService.UpdateMoneyUI(money);
        }

        private void PlayerDeath()
        {
            // Game Over UI.
            // Stop Bloon Spawning.
        }
    }
}