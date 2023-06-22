using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player;
using ServiceLocator.Main;

namespace ServiceLocator.Player.Input
{
    public class InputService
    {
        private SpawnInput currentSpawnInput;

        public InputService() => currentSpawnInput = SpawnInput.DISABLED;

        public void UpdateInputs()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && currentSpawnInput == SpawnInput.ENABLED)
            {
                TrySpawningMonkey();
            }
        }

        private void TrySpawningMonkey()
        {
            if(GameService.Instance.MapService.TryGetSpawnPosition(UnityEngine.Input.mousePosition, out Vector3 spawnPosition))
            {
                GameService.Instance.PlayerService.SpawnMonkey(MonkeyType.Monkey1, spawnPosition);
            }
        }

        public void SetInputState(SpawnInput spawnInput) => currentSpawnInput = spawnInput;
    }

    public enum SpawnInput
    {
        DISABLED,
        ENABLED
    }
}