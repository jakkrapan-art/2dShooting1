using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthHUD : MonoBehaviour
{
    [SerializeField] private GameObject _healthBarHUDObj;
    [SerializeField] private Image _bar;

    public void ShowHealthBar()
    {
        _healthBarHUDObj.SetActive(true);
    }

    private void SetBarFillAmount(float value)
    {
        _bar.fillAmount = value;
    }

    public void UpdateBarValue(int currentHealth, int maxHealth)
    {
        var targetBarFillAmount = (float)currentHealth / maxHealth;

        SetBarFillAmount(targetBarFillAmount);
    }
}
