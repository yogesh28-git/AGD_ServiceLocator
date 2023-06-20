using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIService : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI moneyText;

    public void UpdateHealthUI(int healthToDisplay) => healthText.SetText(healthToDisplay.ToString());

    public void UpdateMoneyUI(int moneyToDisplay) => moneyText.SetText(moneyToDisplay.ToString());
}