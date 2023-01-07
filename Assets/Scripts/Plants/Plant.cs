using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NewPlant", order = 1)]
public class Plant : ScriptableObject
{
    #region Settings
    [SerializeField] private string _name = string.Empty;
    [SerializeField] private List<PlantEvolution> _evolutions = new List<PlantEvolution>();
    [SerializeField] private float _growSpeed = 1.0f;

    public string Name => _name;
    public List<PlantEvolution> Evolutions => _evolutions;
    public float GrowSpeed => _growSpeed;
    #endregion Settings

    #region Runtime variables
    public PlantState State { get; internal set; }
    public float TimeSpentAlive { get; internal set; }
    public int WateredCount { get; internal set; }
    public int HarvestCount { get; internal set; }
    public int FertilizeCount { get; internal set; }
    public int StoleByAnimalsCount { get; internal set; }
    public PlantEvolution CurrentEvolution => _evolutions[(int)(State)];
    #endregion Runtime variables

    public Plant()
    {
        State = PlantState.Underground;
        TimeSpentAlive = 0.0F;
        WateredCount = 0;
        FertilizeCount = 0;
        HarvestCount = 0;
        StoleByAnimalsCount = 0;
    }

    public bool CanEvolve()
    {
        PlantEvolution nextEvolution = _evolutions[(int)(State) + 1];
        return nextEvolution.TimeReachBeforeEvolve <= TimeSpentAlive
            && nextEvolution.WateredNeedToEvolve <= WateredCount
            && nextEvolution.StolenCountToEvolve <= StoleByAnimalsCount;
    }

    public void Evolve()
    {
        State += 1;
        // TODO: switch sprite at least.
    }
}
