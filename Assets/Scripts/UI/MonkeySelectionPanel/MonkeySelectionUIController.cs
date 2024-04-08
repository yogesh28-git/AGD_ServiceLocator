using ServiceLocator.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.UI
{
    public class MonkeySelectionUIController
    {
        private Transform cellContainer;
        private List<MonkeyCellController> monkeyCellControllers;
        private MonkeyCellView monkeyCellPrefab;
        private List<MonkeyCellScriptableObject> monkeyCellScriptableObjects;

        public MonkeySelectionUIController(Transform cellContainer, MonkeyCellView monkeyCellPrefab, List<MonkeyCellScriptableObject> monkeyCellScriptableObjects)
        {
            this.cellContainer = cellContainer;
            monkeyCellControllers = new List<MonkeyCellController>(); 
            this.monkeyCellPrefab = monkeyCellPrefab;
            this.monkeyCellScriptableObjects = monkeyCellScriptableObjects;
        }

        public void Init( PlayerService playerService)
        {
            foreach ( MonkeyCellScriptableObject monkeySO in monkeyCellScriptableObjects )
            {
                MonkeyCellController monkeyCell = new MonkeyCellController( cellContainer, monkeyCellPrefab, monkeySO, playerService );
                monkeyCellControllers.Add( monkeyCell );
            }
        }

        public void SetActive(bool setActive) => cellContainer.gameObject.SetActive(setActive);

    }
}