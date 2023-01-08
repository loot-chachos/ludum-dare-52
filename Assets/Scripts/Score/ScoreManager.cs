using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 0 = Healthy world, 100 = Sick world
    [SerializeField] private float _currentCachet = 0.0f;
    [SerializeField] private float _currentMoney = 0.0f;
    [SerializeField] private float _maxCachetPerCrop = 30.0f;

    [SerializeField] private CanvasGroup _canvasGroup = null;
    [SerializeField] private TextMeshProUGUI _cachetText = null;
    [SerializeField] private TextMeshProUGUI _moneyText = null;

    [SerializeField] private int _updateIterations = 20;
    [SerializeField] private float _moneyScoreUpdateSpeedNumberPerSeconds = 25.0f;
    [SerializeField] private float _cachetScoreUpdateSpeedNumberPerSeconds = 25.0f;

    public float CurrentCachet { get => _currentCachet; }
    public float CurrentMoney { get => _currentMoney;}

    private Coroutine _moneyScoreCoroutine = null;
    private Coroutine _cachetScoreCoroutine = null;

    public void Show()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0.0f;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    public void OnMoneyAdded(float money)
    {
        if (_moneyScoreCoroutine != null)
        {
            StopCoroutine(_moneyScoreCoroutine);
        }
        _moneyScoreCoroutine = StartCoroutine(MoneyScoreUpdater(_currentMoney, _currentMoney + money));
        _currentMoney += money;
    }

    public void OnCachetUpdated(float totalCachet)
    {
        if (_cachetScoreCoroutine != null)
        {
            StopCoroutine(_cachetScoreCoroutine);
        }
        _cachetScoreCoroutine = StartCoroutine(CachetScoreUpdater(_currentCachet, totalCachet));
        _currentCachet = totalCachet;
        CheckWorldViability();
    }

    #region Debug
    [ContextMenu("OnMoneyAdded")]
    public void DebugOnMoneyAdded()
    {
        OnMoneyAdded(10);
    }

    [ContextMenu("OnCachetUpdated")]
    public void DebugOnCachetUpdated()
    {
        OnCachetUpdated(_currentCachet + 100);
    }

    private void CheckWorldViability()
    {
        if (_currentCachet >= _maxCachetPerCrop * GameManager.Instance.Grid.GetCropCount())
        {
            GameManager.Instance.EndGame(true);
        }
    }

    private IEnumerator MoneyScoreUpdater(float startValue, float endValue)
    {
        for (float i = startValue; i < endValue; i+= _moneyScoreUpdateSpeedNumberPerSeconds / _updateIterations)
        {
            _moneyText.text = $"Money : {i:0000}";
            yield return new WaitForSeconds(1.0f/ _updateIterations);
        }
        _moneyText.text = $"Money : {endValue:0000}";
    }

    private IEnumerator CachetScoreUpdater(float startValue, float endValue)
    {
        for (float i = startValue; i < endValue; i += _cachetScoreUpdateSpeedNumberPerSeconds / _updateIterations)
        {
            _cachetText.text = $"Cachet : {i:0000}";
            yield return new WaitForSeconds(1.0f/ _updateIterations);
        }
        _cachetText.text = $"Cachet : {endValue:0000}";
    }
    #endregion Debug
}
