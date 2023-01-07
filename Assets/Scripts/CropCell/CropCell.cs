using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCell : MonoBehaviour
{
    [SerializeField] private Plant _hostedPlant = null;
    [SerializeField] private CropState _state = CropState.blank;

    #region Lifecycle
    public CropCell()
    {
        _hostedPlant = null;
        _state = CropState.blank;
    }

    public void Bury(Plant plant)
    {
        _state = CropState.fertile;
        _hostedPlant = plant;
    }

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
    }

    public void Move()
    {
        // TODO
    }
    #endregion Peaceful gameplay


    #region Speed run gameplay
    public void Harvest()
    {
        _hostedPlant = null;
        // TODO
    }

    public void Fertilize()
    {
        // TODO
    }

    #endregion Speed run gameplay
}
