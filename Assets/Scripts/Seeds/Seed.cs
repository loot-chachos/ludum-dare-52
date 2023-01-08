using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
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
}
