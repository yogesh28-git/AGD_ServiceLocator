using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private GameObject GameplayPanel;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI moneyText;

        [SerializeField] private TextMeshProUGUI waveProgressText;
        [SerializeField] private Button nextWaveButton;

        [SerializeField] private GameObject LevelSelectionPanel;
        [SerializeField] private Button Map1Button;

        // Monkey Selection UI:
        private MonkeySelectionUIController monkeySelectionController;
        [SerializeField] private GameObject MonkeySelectionPanel;
        [SerializeField] private Transform cellContainer;
        [SerializeField] private MonkeyCellView monkeyCellPrefab;
        [SerializeField] private List<MonkeyCellScriptableObject> monkeyCellScriptableObjects;


        private void Start()
        {
            monkeySelectionController = new MonkeySelectionUIController(cellContainer, monkeyCellPrefab, monkeyCellScriptableObjects);
            MonkeySelectionPanel.SetActive(false);
            monkeySelectionController.SetActive(false);

            GameplayPanel.SetActive(false);
            LevelSelectionPanel.SetActive(true);
            nextWaveButton.onClick.AddListener(OnNextWaveButton);
        }

        public void OnMapSelected()
        {
            LevelSelectionPanel.SetActive(false);
            GameplayPanel.SetActive(true);
            MonkeySelectionPanel.SetActive(true);
            monkeySelectionController.SetActive(true);
        }

        private void OnNextWaveButton()
        {
            GameService.Instance.WaveService.StarNextWave();
            SetNextWaveButton(false);
        }

        public void SetNextWaveButton(bool setInteractable) => nextWaveButton.interactable = setInteractable;

        public void UpdateHealthUI(int healthToDisplay) => healthText.SetText(healthToDisplay.ToString());

        public void UpdateMoneyUI(int moneyToDisplay) => moneyText.SetText(moneyToDisplay.ToString());

        public void UpdateWaveProgressUI(int waveCompleted, int totalWaves) => waveProgressText.SetText(waveCompleted.ToString() + "/" + totalWaves.ToString());

    }
}