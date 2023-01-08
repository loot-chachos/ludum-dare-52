using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCell : MonoBehaviour, IEatable
{
    #region Fields
    [SerializeField] private CropState _state = CropState.blank;
    [SerializeField] private SpriteRenderer _soilSpriteRenderer;
    [SerializeField] private SpriteRenderer _plantSpriteRenderer;

    [SerializeField] private Sprite _blankSprite = null;
    [SerializeField] private Sprite _fertileSprite = null;
    [SerializeField] private Sprite _deadSprite = null;
    #endregion Fields

    private Plant _hostedPlant = null;
    private int _cellIndex = -1;

    public float TimeAlive => _hostedPlant.TimeSpentAlive;

    public Plant HostedPlant { get => _hostedPlant;}
    public CropState State { get => _state;}

    #region Events
    private Action _isWatered = null;
    public event Action IsWatered {
        add
        {
            _isWatered -= value;
            _isWatered += value;
        }
        remove
        {
            _isWatered -= value;
        }
    }
    private Action<int> _isCut = null;
    public event Action<int> IsCut
    {
        add
        {
            _isCut -= value;
            _isCut += value;
        }
        remove
        {
            _isCut -= value;
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

    private Action _isBury = null;
    public event Action IsBury
    {
        add
        {
            _isBury -= value;
            _isBury += value;
        }
        remove
        {
            _isBury -= value;
        }
    }

    private Action _isFertilize = null;
    public event Action IsFertilize
    {
        add
        {
            _isFertilize -= value;
            _isFertilize += value;
        }
        remove
        {
            _isFertilize -= value;
        }
    }

    private Action _isMoved = null;
    public event Action IsMoved
    {
        add
        {
            _isMoved -= value;
            _isMoved += value;
        }
        remove
        {
            _isMoved -= value;
        }
    }
    #endregion Events

    #region Lifecycle
    public CropCell(int cellIndex)
    {
        _hostedPlant = null;
        SetIndex(cellIndex);
    }

    public void SetIndex(int index)
    {
        _hostedPlant = null;
        _cellIndex = index;
    }

    public void Initiliaze()
    {
        UpdateCropState(CropState.blank);
    }

    public void UpdateCrop(float additionalTime)
    {
        if (_state == CropState.blank || _state == CropState.dead)
        {
#if UNITY_EDITOR
            //UnityEngine.Debug.Log("Crop is either blank or dead. Do not update.");
#endif // UNITY_EDITOR
            return;
        }

        _hostedPlant.TimeSpentAlive += additionalTime;

        if (_hostedPlant.CanEvolve())
        {
            _hostedPlant.Evolve();
            if (_plantEvolved != null)
            {
                // Send the current state which is new
                _plantEvolved(_hostedPlant.State, _hostedPlant.CurrentEvolution);
            }
        }
    }

    /// <summary>
    /// When the crop begin to host a plant. Should be automatic
    /// </summary>
    /// <param name="plant"></param>
    public void Bury(Plant plant)
    {
        if (plant != null)
        {
            _hostedPlant = plant;
            UpdateCropState(CropState.fertile);
            RegisterPlant();
        }
        else
        {
            RemovePlant();
            UpdateCropState(CropState.blank);
        }

        if (_isBury != null)
        {
            _isBury();
        }
    }

    /// <summary>
    /// When the crop cannot host a plant anymore. It is dead. (tumbstone ?)
    /// </summary>
    public void Kill()
    {
        RemovePlant();
        UpdateCropState(CropState.dead);
    }
    #endregion Lifecycle

    #region Peaceful gameplay
    public void Watered()
    {
        _hostedPlant.WateredCount++;

        if (_isWatered != null)
        {
            _isWatered();
        }
    }

    public void Move()
    {
        RemovePlant();
        UpdateCropState(CropState.blank);

        if (_isMoved != null)
        {
            _isMoved();
        }
    }
    #endregion Peaceful gameplay

    #region Speed run gameplay
    public void Stolen()
    {
        _hostedPlant.TimeSpentAlive = 0.0F;
        _hostedPlant.StoleByAnimalsCount++;
        _hostedPlant.State = 0;
        // TODO

        if (_isCut != null)
        {
            _isCut(_cellIndex);
        }
    }

    public void Harvest()
    {
        if (_hostedPlant == null)
        {
#if UNITY_EDITOR
            Debug.Log("Can't harvest empty plant. Return.");
#endif
            return;
        }

        _hostedPlant.TimeSpentAlive = 0.0F;
        _hostedPlant.HarvestCount++;
        _hostedPlant.State = 0;
        // TODO

        if (_isCut != null)
        {
            _isCut(_cellIndex);
        }
    }

    public void Fertilize()
    {
        // TODO
        _hostedPlant.FertilizeCount++;

        if (_isFertilize != null)
        {
            _isFertilize();
        }
    }
#endregion Speed run gameplay

    public void UpdateCropState(CropState state)
    {
        _state = state;
        switch (state)
        {
            case CropState.blank:
                {
                    _plantSpriteRenderer.sprite = null;
                    _soilSpriteRenderer.sprite = _deadSprite;
                }
                break;
            case CropState.fertile:
                {
                    _soilSpriteRenderer.sprite = _fertileSprite;
                }
                break;
            case CropState.dead:
                {
                    _plantSpriteRenderer.sprite = null;
                    _soilSpriteRenderer.sprite = _deadSprite;
                }
                break;
        }
    }

    private void RegisterPlant()
    {
        _hostedPlant.PlantSpriteRenderer = _plantSpriteRenderer;
        IsBury += _hostedPlant.UpdateSprite;
        IsCut += _hostedPlant.UpdateSprite;
        PlantEvolved += _hostedPlant.UpdateSpriteOnEvo;
    }

    private void RemovePlant()
    {
        if (_hostedPlant != null)
        {
            IsBury -= _hostedPlant.UpdateSprite;
            IsCut -= _hostedPlant.UpdateSprite;
            PlantEvolved -= _hostedPlant.UpdateSpriteOnEvo;
        }
        _plantSpriteRenderer.sprite = null;
        _hostedPlant = null;
    }
}