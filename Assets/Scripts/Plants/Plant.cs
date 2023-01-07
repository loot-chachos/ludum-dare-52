using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NewPlant", order = 1)]
public class Plant : ScriptableObject
{
    #region Settings
    [SerializeField] private string _name = string.Empty;
    [SerializeField] private List<PlantEvolution> _stateSprite = new List<PlantEvolution>();
    [SerializeField] private float _growSpeed = 1.0f;

    public string Name => _name;
    public List<PlantEvolution> StateSprite => _stateSprite;
    public float GrowSpeed => _growSpeed;
    #endregion Settings

    #region Runtime variables
    private PlantState _state = PlantState.Underground;
    private int _wateredCount = 0;
    private int _harvestCount = 0;
    private int _stoleByAnimalsCount = 0;

    public int WateredCount { get; set; }
    #endregion Runtime variables




    public Plant()
    {
        _state = PlantState.Underground;
        _wateredCount = 0;
        _harvestCount = 0;
        _stoleByAnimalsCount = 0;
    }
}
