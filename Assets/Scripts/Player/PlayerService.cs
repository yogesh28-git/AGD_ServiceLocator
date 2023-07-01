using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player.Projectile;
using ServiceLocator.UI;
using ServiceLocator.Map;
using ServiceLocator.Sound;

namespace ServiceLocator.Player
{
    public class PlayerService : MonoBehaviour
    {
        [SerializeField] private UIService uiService;
        [SerializeField] private MapService mapService;
        [SerializeField] private SoundService soundService;
        [SerializeField] private PlayerService playerService;

        [SerializeField] public PlayerScriptableObject playerScriptableObject;
        [SerializeField] public Transform projectileContainer;

        private ProjectilePool projectilePool;

        private List<MonkeyController> activeMonkeys;
        private MonkeyView selectedMonkeyView;
        private int health;
        private int money;
        public int Money => money;

        private void Start()
        {
            projectilePool = new ProjectilePool(playerService, playerScriptableObject.ProjectilePrefab, playerScriptableObject.ProjectileScriptableObjects, projectileContainer);
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            health = playerScriptableObject.Health;
            money = playerScriptableObject.Money;
            uiService.UpdateHealthUI(health);
            uiService.UpdateMoneyUI(money);
            activeMonkeys = new List<MonkeyController>();
        }

        public void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                UpdateSelectedMonkeyDisplay();
            }
        }

        private void UpdateSelectedMonkeyDisplay()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach(RaycastHit2D hit in hits)
            {
                if(IsMonkeyCollider(hit.collider))
                {
                    selectedMonkeyView?.MakeRangeVisible(false);
                    selectedMonkeyView = hit.collider.GetComponent<MonkeyView>();
                    selectedMonkeyView.MakeRangeVisible(true);
                    return;
                }
            }

            selectedMonkeyView?.MakeRangeVisible(false);
        }

        private bool IsMonkeyCollider(Collider2D collider) => collider != null && !collider.isTrigger && collider.GetComponent<MonkeyView>() != null;

        public void TrySpawningMonkey(MonkeyType monkeyType, int monkeyCost, Vector3 dropPosition)
        {
            if (monkeyCost > money)
                return;

            if (mapService.TryGetMonkeySpawnPosition(dropPosition, out Vector3 spawnPosition))
            {
                SpawnMonkey(monkeyType, spawnPosition);
                soundService.PlaySoundEffects(SoundType.SpawnMonkey);
            }
        }

        public void SpawnMonkey(MonkeyType monkeyType, Vector3 spawnPosition)
        {
            MonkeyScriptableObject monkeySO = playerScriptableObject.MonkeyScriptableObjects.Find(so => so.Type == monkeyType);
            MonkeyController monkey = new MonkeyController(monkeySO, projectilePool);
            monkey.SetPosition(spawnPosition);
            activeMonkeys.Add(monkey);

            money -= monkeySO.Cost;
            uiService.UpdateMoneyUI(money);
        }

        public void ReturnProjectileToPool(ProjectileController projectileToReturn) => projectilePool.ReturnItem(projectileToReturn);
        
        public void TakeDamage(int damageToTake)
        {
            health = health - damageToTake <= 0 ? 0 : health - damageToTake;
            uiService.UpdateHealthUI(health);
            if(health <= 0)
            {
                PlayerDeath();
            }
        }

        public void GetReward(int reward)
        {
            money += reward;
            uiService.UpdateMoneyUI(money);
        }

        private void PlayerDeath() => uiService.UpdateGameEndUI(false);
    }
}