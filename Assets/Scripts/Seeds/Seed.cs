using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private GameObject _seedVisual = null;

    private Action _isEaten = null;
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

    private void OnMouseOver()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Animal"))
        {
            _seedVisual.SetActive(false);
            _isEaten?.Invoke();
        }
        if (collision.TryGetComponent<Hand>(out Hand hand) && hand.GrabbedTool is Seeder)
        {
            _seedVisual.SetActive(true);
        }
    }
}
