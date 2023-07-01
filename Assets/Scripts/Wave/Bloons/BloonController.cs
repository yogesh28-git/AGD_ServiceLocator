using ServiceLocator.Player;
using ServiceLocator.Sound;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Wave.Bloon
{
    public class BloonController
    {
        private PlayerService playerService;
        private WaveService waveService;
        private SoundService soundService;

        private BloonView bloonView;
        private BloonScriptableObject bloonScriptableObject;

        private const float waypointThreshold = 0.1f;
        private List<Vector3> waypoints;
        private int currentHealth;
        private int currentWaypointIndex;
        private BloonState currentState;

        public Vector3 Position => bloonView.transform.position;

        public BloonController(PlayerService playerService, WaveService waveService, SoundService soundService, BloonView bloonPrefab, Transform bloonContainer)
        {
            this.playerService = playerService;
            this.waveService = waveService;
            this.soundService = soundService;
            bloonView = Object.Instantiate(bloonPrefab, bloonContainer);
            bloonView.Controller = this;
        }

        public void Init(BloonScriptableObject bloonScriptableObject)
        {
            this.bloonScriptableObject = bloonScriptableObject;
            bloonView.SetRenderer(bloonScriptableObject.Sprite);
            currentHealth = bloonScriptableObject.Health;
            
            currentState = BloonState.ACTIVE;
            waypoints = new List<Vector3>();
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
                soundService.PlaySoundEffects(Sound.SoundType.BloonPop);
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
            waveService.RemoveBloon(this);
            playerService.TakeDamage(bloonScriptableObject.Damage);
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
                waveService.SpawnBloons(bloonScriptableObject.LayeredBloons, bloonView.transform.position, currentWaypointIndex);

            playerService.GetReward(bloonScriptableObject.Reward);
            waveService.RemoveBloon(this);
        }

        public BloonType GetBloonType() => bloonScriptableObject.Type;
    }

    public enum BloonState
    {
        ACTIVE,
        POPPED
    }
}