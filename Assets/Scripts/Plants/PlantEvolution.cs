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

    public Sprite Visual => _visual;
    public float CachetGiven => _cachetGiven;
    public float ScoreGivenWhenHarvest => _scoreGivenWhenHarvest;
}
