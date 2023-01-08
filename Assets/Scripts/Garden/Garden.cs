using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NewGarden", order = 1)]
public class Garden : ScriptableObject
{
    [SerializeField] private GameObject _tilePrefab = null;
    [SerializeField] private Vector2 _gridSize = new Vector2(5, 5);
    [SerializeField] private Vector2 _cropPadding = new Vector2(1, 1);
    [SerializeField] private Vector2 _startingPos = new Vector2(0, 0);
    [SerializeField] private GenerationDirection _generatonDirection = GenerationDirection.NorthEast;

    [Header("Growing")]
    [SerializeField] private float _updateFrequencyInSeconds = 1f;
    [Header("Plants")]
    [SerializeField] private List<PlantParameters> _availablePlants = new List<PlantParameters>();

    public GenerationDirection GeneratonDirection => _generatonDirection;
    public GameObject TilePrefab => _tilePrefab;
    public Vector2 StartingPos => _startingPos;
    public Vector2 Size => _gridSize;
    public Vector2 CropPadding => _cropPadding;
    public float UpdateFrequencyInSeconds => _updateFrequencyInSeconds;

    public Plant PickRandomPlant()
    {
        int randomIndex = UnityEngine.Random.Range(0, _availablePlants.Count);
        return new Plant(_availablePlants[randomIndex]);
    }
}
