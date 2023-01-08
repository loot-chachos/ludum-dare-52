using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour, IEatable
{
    [SerializeField] private GameObject _seedVisual = null;
    private bool _isSeeded = false;

    private Action _isEaten = null;

    public bool IsSeeded { get => _isSeeded; }

    public event Action IsEaten
    {
        add
        {
            _isEaten -= value;
            _isEaten += value;
        }
        remove
        {
            _isEaten -= value;
        }
    }

    public void Eat()
    {
        _isEaten?.Invoke();
        _seedVisual.SetActive(false);
        _isSeeded = false;
    }

    public void PlaceSeed()
    {
        _seedVisual.SetActive(true);
        _isSeeded = true;
    }
}
