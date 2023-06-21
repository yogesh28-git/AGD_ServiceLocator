using System.Collections;
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
        [SerializeField] private Button nextWaveButton;

        [SerializeField] private GameObject LevelSelectionPanel;
        [SerializeField] private Button Map1Button;

        private void Start()
        {
            GameplayPanel.SetActive(false);
            LevelSelectionPanel.SetActive(true);
            nextWaveButton.onClick.AddListener(OnNextWaveButton);
        }

        public void OnMapSelected()
        {
            LevelSelectionPanel.SetActive(false);
            GameplayPanel.SetActive(true);
        }

        private void OnNextWaveButton()
        {
            GameService.Instance.WaveService.StarNextWave();
            SetNextWaveButton(false);
        }

        public void SetNextWaveButton(bool setInteractable) => nextWaveButton.interactable = setInteractable;

        public void UpdateHealthUI(int healthToDisplay) => healthText.SetText(healthToDisplay.ToString());

        public void UpdateMoneyUI(int moneyToDisplay) => moneyText.SetText(moneyToDisplay.ToString());

    }
}