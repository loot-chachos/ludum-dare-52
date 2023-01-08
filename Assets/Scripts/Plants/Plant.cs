using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Plant
{
    #region Settings
    [SerializeField] private PlantParameters _parameters = null;
    #endregion Settings

    #region Runtime variables
    public PlantState State { get; internal set; }
    public float TimeSpentAlive { get; internal set; }
    public int WateredCount { get; internal set; }
    public int HarvestCount { get; internal set; }
    public int FertilizeCount { get; internal set; }
    public int StoleByAnimalsCount { get; internal set; }
    public PlantEvolution CurrentEvolution => _parameters.Evolutions[(int)(State)];
    public SpriteRenderer PlantSpriteRenderer { get; internal set; }
    #endregion Runtime variables

    public Plant(PlantParameters plantParameters)
    {
        State = PlantState.Underground;
        TimeSpentAlive = 0.0F;
        WateredCount = 0;
        FertilizeCount = 0;
        HarvestCount = 0;
        StoleByAnimalsCount = 0;
        _parameters = plantParameters;
    }

    public bool CanEvolve()
    {
        if ((int)(State) + 1 >= _parameters.Evolutions.Count)
            return false;
        PlantEvolution nextEvolution = _parameters.Evolutions[(int)(State) + 1];
        return nextEvolution.TimeReachBeforeEvolve <= TimeSpentAlive
            && nextEvolution.WateredNeedToEvolve <= WateredCount
            && nextEvolution.StolenCountToEvolve <= StoleByAnimalsCount;
    }

    public void Evolve()
    {
        State += 1;
        // TODO: switch sprite at least.
    }

    public void UpdateSprite()
    {
        PlantSpriteRenderer.sprite = CurrentEvolution.Visual;
    }

    public void UpdateSpriteOnEvo(PlantState state = PlantState.Underground, PlantEvolution evo = null)
    {
        UpdateSprite();
    }
}
