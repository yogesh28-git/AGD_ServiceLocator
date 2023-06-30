using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Main;

namespace ServiceLocator.Wave.Bloon
{
    public class BloonController
    {
        private BloonView bloonView;
        private BloonScriptableObject bloonScriptableObject;

        private const float waypointThreshold = 0.1f;
        private List<Vector3> waypoints;
        private int currentHealth;
        private int currentWaypointIndex;
        private BloonState currentState;

        public Vector3 Position => bloonView.transform.position;
        public BloonView View => bloonView;

        public BloonController(BloonView bloonPrefab, Transform bloonContainer)
        {
            bloonView = Object.Instantiate(bloonPrefab, bloonContainer);
            bloonView.Controller = this;
            waypoints = new List<Vector3>();
        }

        public void Init(BloonScriptableObject bloonScriptableObject)
        {
            this.bloonScriptableObject = bloonScriptableObject;
            currentHealth = bloonScriptableObject.Health;
            bloonView.SetRenderer(bloonScriptableObject.Sprite);
            currentState = BloonState.ACTIVE;
        }

        public void SetPosition(Vector3 spawnPosition)
        {
            bloonView.transform.position = spawnPosition;
            bloonView.gameObject.SetActive(true);
        }

        public void SetWayPoints(List<Vector3> waypointsToSet, int startingWaypointIndex)
        {
            waypoints = waypointsToSet;
            currentWaypointIndex = startingWaypointIndex;
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth = currentHealth - damageToTake <= 0 ? 0 : currentHealth -= damageToTake;
            if(currentHealth <= 0 && currentState == BloonState.ACTIVE)
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
            currentState = BloonState.POPPED;
            bloonView.PopBloon();
        }

        public void OnPopAnimationPlayed()
        {
            if (bloonScriptableObject.LayeredBloons.Count > 0)
                GameService.Instance.WaveService.SpawnBloons(bloonScriptableObject.LayeredBloons, bloonView.transform.position, currentWaypointIndex);

            GameService.Instance.PlayerService.GetReward(bloonScriptableObject.Reward);
            GameService.Instance.WaveService.RemoveBloon(this);
        }

        public BloonType GetBloonType() => bloonScriptableObject.Type;
    }

    public enum BloonState
    {
        ACTIVE,
        POPPED
    }
}