using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCell : MonoBehaviour
{
    #region Fields
    [SerializeField] private Plant _hostedPlant = null;
    [SerializeField] private CropState _state = CropState.blank;
    #endregion Fields

    public float TimeAlive => _hostedPlant.TimeSpentAlive;
    
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

    private Action _isCut = null;
    public event Action IsCut
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
    public CropCell()
    {
        _hostedPlant = null;
        _state = CropState.blank;
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
        _state = CropState.fertile;
        _hostedPlant = plant;

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
        _hostedPlant = null;
        _state = CropState.dead;
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
        // TODO

        if (_isMoved != null)
        {
            _isMoved();
        }
    }
    #endregion Peaceful gameplay

    #region Speed run gameplay
    public void Harvest()
    {
        _hostedPlant = null;
        _hostedPlant.TimeSpentAlive = 0.0F;
        _hostedPlant.HarvestCount++;
        // TODO

        if (_isCut != null)
        {
            _isCut();
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
}
