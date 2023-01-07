using System;
using UnityEngine;

/// <summary>
/// Data for scriptable object
/// </summary>
[Serializable]
public class PlantEvolution
{
    [SerializeField] private Sprite _visual = null;
    [SerializeField] private float _cachetGiven = 1.0f;
    [SerializeField] private float _scoreGivenWhenHarvest = 1.0f;
    [SerializeField] private float _timeReachBeforeEvolve = 10.0f;
    [SerializeField] private int _wateredNeedToEvolve = 0;
    [SerializeField] private int _stolenCountToEvolve = 0;

    public Sprite Visual => _visual;
    public float CachetGiven => _cachetGiven;
    public float ScoreGivenWhenHarvest => _scoreGivenWhenHarvest;
    public float TimeReachBeforeEvolve => _timeReachBeforeEvolve;
    public int WateredNeedToEvolve => _wateredNeedToEvolve;
    public int StolenCountToEvolve => _stolenCountToEvolve;
}
