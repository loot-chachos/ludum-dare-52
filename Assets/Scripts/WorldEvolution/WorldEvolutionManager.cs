using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEvolutionManager : MonoBehaviour
{
    // 0 = Healthy world, 100 = Sick world
    [SerializeField, Range(0, 100)] private float _currentWorldEvolutionPercent = 0.0f;
    private float _currentGradientValue = 0.0f;

    private SpriteRenderer _filterObject = null;

    [SerializeField] private WorldEvolutionParameters _parameters = null;

    private void Start()
    {
        _filterObject = Instantiate(_parameters.TintedFilter, transform);
        _currentGradientValue = _currentWorldEvolutionPercent;
    }

    private void Update()
    {
        _currentGradientValue += Mathf.Clamp(_currentWorldEvolutionPercent - _currentGradientValue, 0.0f, _parameters.TintedMaxSpeedPercentPerSeconds * Time.deltaTime);
        _filterObject.color = _parameters.ColorGradient.Evaluate(_currentGradientValue / 100.0f);
    }

    [ContextMenu("OnKillAnimals")]
    public void OnKillAnimals()
    {
        _currentWorldEvolutionPercent += _parameters.KillDegradationPercent;
        CheckWorldViability();
    }

    [ContextMenu("OnUseFertilizer")]
    public void OnUseFertilizer()
    {
        _currentWorldEvolutionPercent += _parameters.FertilizerDegradationPercent;
        CheckWorldViability();
    }

    private void CheckWorldViability()
    {
        if (_currentWorldEvolutionPercent >= 100.0f)
        {
            GameManager.Instance.EndGame(false);
        }
    }
}
