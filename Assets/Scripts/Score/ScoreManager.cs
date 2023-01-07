using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 0 = Healthy world, 100 = Sick world
    [SerializeField] private float _currentCachet = 0.0f;
    [SerializeField] private float _currentMoney = 0.0f;

    [SerializeField] private TextMeshProUGUI _cachetText = null;
    [SerializeField] private TextMeshProUGUI _moneyText = null;

    public float CurrentCachet { get => _currentCachet; }
    public float CurrentMoney { get => _currentMoney;}

    public void OnMoneyAdded(float money)
    {
        _currentMoney += money;
        UpdateMoneyText(_currentMoney);
    }

    public void OnCachetUpdated(float totalCachet)
    {
        _currentCachet = totalCachet;
        UpdateCachetText(_currentCachet);
    }
    public void UpdateCachetText(float cachet)
    {
        _cachetText.text = "Cachet : " + cachet.ToString("0000");
    }

    public void UpdateMoneyText(float cachet)
    {
        _moneyText.text = "Money : " + cachet.ToString("0000");
    }

    #region Debug
    [ContextMenu("OnMoneyAdded")]
    public void DebugOnMoneyAdded()
    {
        _currentMoney += 10;
        UpdateMoneyText(_currentMoney);
    }

    [ContextMenu("OnCachetUpdated")]
    public void DebugOnCachetUpdated()
    {
        _currentCachet += 10;
        UpdateCachetText(_currentCachet);
    }
    #endregion Debug
}
