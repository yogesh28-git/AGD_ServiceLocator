using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Main;

namespace ServiceLocator.Bloon
{
    public class BloonController
    {
        private BloonView bloonView;
        private BloonScriptableObject bloonScriptableObject;

        private const float waypointThreshold = 0.1f;
        private List<Vector3> waypoints;
        private int currentHealth;
        private int currentWaypointIndex;

        public Vector3 Position => bloonView.transform.position;

        public BloonController(BloonView bloonPrefab)
        {
            bloonView = Object.Instantiate(bloonPrefab);
            bloonView.Controller = this;
            waypoints = new List<Vector3>();
        }

        public void Init(BloonScriptableObject bloonScriptableObject)
        {
            this.bloonScriptableObject = bloonScriptableObject;
            currentHealth = bloonScriptableObject.Health;
            bloonView.SetRenderer(bloonScriptableObject.Sprite);
            bloonView.gameObject.SetActive(true);
            SetWayPoints();
        }

        public void SetWayPoints()
        {
            bloonView.transform.position = GameService.Instance.MapService.GetBloonSpawnPositionForCurrentMap();
            waypoints = GameService.Instance.MapService.GetWayPointsForCurrentMap();
            currentWaypointIndex = 0;
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth = currentHealth - damageToTake <= 0 ? 0 : currentHealth -= damageToTake;
            if(currentHealth <= 0)
            {
                PopBloon();
                GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.BloonPop);
            }
        }

        public void FollowWayPoints()
        {
            if(currentWaypointIndex < waypoints.Count)
            {
                Vector3 direction = waypoints[currentWaypointIndex] - bloonView.transform.position;
                bloonView.transform.Translate(direction.normalized * bloonScriptableObject.Speed * Time.deltaTime);
                if (direction.magnitude < waypointThreshold)
                    currentWaypointIndex++;
            }
            else
            {
                ReachedFinalWayPoint();
            }
        }

        private void ReachedFinalWayPoint()
        {
            GameService.Instance.WaveService.RemoveBloon(this);
            GameService.Instance.PlayerService.TakeDamage(bloonScriptableObject.Damage);
            bloonView.gameObject.SetActive(false);
        }

        private void PopBloon()
        {
            bloonView.PopBloon();
            bloonView.gameObject.SetActive(false);
            GameService.Instance.WaveService.RemoveBloon(this);
            GameService.Instance.PlayerService.GetReward(bloonScriptableObject.Reward);
        }

        public BloonType GetBloonType() => bloonScriptableObject.Type;
    }
}