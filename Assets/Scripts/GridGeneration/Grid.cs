using System;
using UnityEngine;

public class Grid
{
    // Static settings
    private Garden _garden = null;

    // Runtime settings
    private bool _isGridActive = false;
    private float _timeActive = 0.0f;
    private CropCell[] _crops = null;
    private float _currentTimer = 0.0f;

    #region Events
    private Action _isCropWatered = null;
    public event Action IsCropWatered
    {
        add
        {
            _isCropWatered -= value;
            _isCropWatered += value;
        }
        remove
        {
            _isCropWatered -= value;
        }
    }

    private Action _isCropCut = null;
    public event Action IsCut
    {
        add
        {
            _isCropCut -= value;
            _isCropCut += value;
        }
        remove
        {
            _isCropCut -= value;
        }
    }

    private Action<PlantState, PlantEvolution> _plantEvolved = null;
    public event Action<PlantState, PlantEvolution> PlantEvolved
    {
        add
        {
            _plantEvolved -= value;
            _plantEvolved += value;
        }
        remove
        {
            _plantEvolved -= value;
        }
    }

    private Action _isCropBury = null;
    public event Action IsCropBury
    {
        add
        {
            _isCropBury -= value;
            _isCropBury += value;
        }
        remove
        {
            _isCropBury -= value;
        }
    }

    private Action _isCropFertilize = null;
    public event Action IsCropFertilize
    {
        add
        {
            _isCropFertilize -= value;
            _isCropFertilize += value;
        }
        remove
        {
            _isCropFertilize -= value;
        }
    }

    private Action _isCropMoved = null;
    public event Action IsCropMoved
    {
        add
        {
            _isCropMoved -= value;
            _isCropMoved += value;
        }
        remove
        {
            _isCropMoved -= value;
        }
    }
    #endregion Events

    public Grid(Garden garden)
    {
        _garden = garden;
    }

    public void InitializeGrid(Transform parent)
    {
        _isGridActive = false;
        _timeActive = 0.0f;
        _currentTimer = 0.0f;

        _crops = GridGeneration.InitializeGrid(
            _garden.GeneratonDirection,
            _garden.TilePrefab,
            _garden.Size,
            _garden.StartingPos,
            _garden.CropPadding,
            parent);
    }

    public void SeedPlant(int cellIndex, Plant plant = null)
    {
        _isGridActive = true;
        if (plant == null)
        {
            // Choose randomly among the garden
            plant = _garden.PickRandomPlant();
        }

        _crops[cellIndex].Bury(plant);
    }    

    void Update()
    {
        _currentTimer += Time.deltaTime;
        if (_isGridActive)
        {
            if (_currentTimer >= _garden.UpdateFrequencyInSeconds)
            {
                for (int i = 0; i < _crops.Length; i++)
                {
                    _crops[i].UpdateCrop(_currentTimer);
                }

                _currentTimer = 0.0f;
            }
        }
    }
}
