using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NewPlant", order = 1)]
public class PlantParameters : ScriptableObject
{
    #region Settings
    [SerializeField] private string _name = string.Empty;
    [SerializeField] private List<PlantEvolution> _evolutions = new List<PlantEvolution>();
    [SerializeField] private float _growSpeed = 1.0f;

    public string Name => _name;
    public List<PlantEvolution> Evolutions => _evolutions;
    public float GrowSpeed => _growSpeed;
    #endregion Settings
}
