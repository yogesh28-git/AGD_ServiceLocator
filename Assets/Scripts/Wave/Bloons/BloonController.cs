using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Bloon
{
    public class BloonController
    {
        private BloonView bloonView;
        private BloonScriptableObject bloonScriptableObject;

        private List<Vector3> waypoints;
        private int currentHealth;

        public void Init(BloonView bloonPrefab, BloonScriptableObject bloonScriptableObject)
        {
            ResetBloon();
            bloonView = Object.Instantiate(bloonPrefab);
            bloonView.SetController(this);
            this.bloonScriptableObject = bloonScriptableObject;
            currentHealth = bloonScriptableObject.Health;
        }

        private void ResetBloon()
        {
            if (bloonView != null)
                Object.Destroy(bloonView.gameObject);
            waypoints.Clear();
        }

        public void SetWayPoints(Vector3 spawnPosition, List<Vector3> waypointsToFollow)
        {
            bloonView.transform.position = spawnPosition;
            this.waypoints = waypointsToFollow;
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth = currentHealth - damageToTake <= 0 ? 0 : currentHealth = damageToTake;
            if(currentHealth <= 0)
            {
                PopBloon();
            }
        }

        private void FollowWayPoints()
        {

        }

        private bool IfClearedMap()
        {
            throw new System.NotImplementedException();
            // Inform WaveService of this event.
            // Reduce Player's Health
        }

        private void PopBloon()
        {
            bloonView.PopBloon();
            // Return this back to the Pool.
            // Give Reward to player.
            // Inform WaveService of this event.
        }
    }
}