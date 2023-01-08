using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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

    private Action _plantEvolved = null;
    public event Action PlantEvolved
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

    internal void ActivateGrid()
    {
        for (int i = 0; i < _crops.Length; i++)
        {
            _crops[i].IsCut += OnCropCut;
            _crops[i].IsBury += _isCropBury;
            _crops[i].IsFertilize += _isCropFertilize;
            _crops[i].IsMoved += _isCropMoved;
            _crops[i].IsWatered += _isCropWatered;
            _crops[i].PlantEvolved += _plantEvolved;

            _crops[i].Initiliaze(_garden.WateredDuration);

            _crops[i].PlantEvolved += UpdateCachet;
            _crops[i].IsMoved += UpdateCachet;
            _crops[i].IsBury += UpdateCachet;
            _crops[i].IsCut += UpdateCachet;
        }
    }
    #endregion Events

    public Grid(Garden garden)
    {
        _garden = garden;
    }

    public int GetCropCount()
    {
        return _crops.Length;
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

    public void Update()
    {
        if (GameManager.Instance.HasStarted == false)
        {
            return;
        }

        _currentTimer += Time.deltaTime;
        if (_isGridActive)
        {
            if (_currentTimer >= _garden.UpdateFrequencyInSeconds)
            {
                for (int i = 0; i < _crops.Length; i++)
                {
                    _crops[i]?.UpdateCrop(_currentTimer);
                }

                _currentTimer = 0.0f;
            }
        }
    }

    public CropCell FindRandomCropOfState(PlantState plantState)
    {
        List<CropCell> possibleCells = new List<CropCell>();
        foreach (CropCell crop in _crops)
        {
            if (crop?.HostedPlant != null && crop.HostedPlant.State == plantState)
            {
                possibleCells.Add(crop);
            }
        }

        return possibleCells[UnityEngine.Random.Range(0, possibleCells.Count)];
    }

    public CropCell FindRandomCrop()
    {
        return _crops[UnityEngine.Random.Range(0, _crops.Length)];
    }

    public CropCell FindRandomCropBetweenState(PlantState minState, PlantState maxState)
    {
        List<CropCell> possibleCells = new List<CropCell>();
        foreach (CropCell crop in _crops)
        {
            if (crop?.HostedPlant != null && crop.HostedPlant.State >= minState && crop.HostedPlant.State <= maxState)
            {
                possibleCells.Add(crop);
            }
        }

        if (possibleCells.Count == 0)
        {
            return null;
        }

        return possibleCells[UnityEngine.Random.Range(0, possibleCells.Count)];
    }

    private void OnCropCut(int cellIndex)
    {
        // Spread it to the 8 adjacent cells randomly
        int cellX = (int)(cellIndex / _garden.Size.y);
        int cellY = (int)(cellIndex % _garden.Size.y);
#if UNITY_EDITOR
        Debug.Log($"cell index: {cellIndex}, X: {cellX}, Y: {cellY}");
#endif

        // Clamp future values
        int previousXCell = Mathf.Clamp(cellX-1, 0, (int)_garden.Size.x);
        int nextCellX = Mathf.Clamp(cellX+1, 0, (int)_garden.Size.x);
        int previousYCell = Mathf.Clamp(cellY-1, 0, (int)_garden.Size.y);
        int nextCellY = Mathf.Clamp(cellY+1, 0, (int)_garden.Size.y);

        for (int i = previousXCell; i <= nextCellX; i++)
        {
            for (int j = previousYCell; j <= nextCellY; j++)
            {
#if UNITY_EDITOR
                Debug.Log($"crop is X:{i}, Y:{j}");
#endif
                int cropIndex = i * (int)_garden.Size.y + j;
                if (_crops[cropIndex].State != CropState.fertile || _crops[cropIndex].HostedPlant != null)
                {
                    continue;
                }

                if ((i != cellX || j != cellY)  && UnityEngine.Random.value > 0.5f)
                {
#if UNITY_EDITOR
                    //Debug.Log($"Next crop updated will be X:{i}, Y:{j}");
#endif
                    // Seed it
                    Plant plant = _garden.PickRandomPlant();
                    _crops[cropIndex].Bury(plant);
                }
            }
        }

        if (_isCropCut != null)
        {
            _isCropCut();
        }
    }

    private void UpdateCachet(int cellIndex)
    {
        UpdateCachet();
    }

    private void UpdateCachet()
    {
        float cachet = 0.0f;
        foreach(CropCell crop in _crops)
        {
            if (crop.HostedPlant != null)
            {
                cachet += crop.HostedPlant.CurrentEvolution.CachetGiven;
            }
        }
        GameManager.Instance.ScoreManager.OnCachetUpdated(cachet);
    }
}
