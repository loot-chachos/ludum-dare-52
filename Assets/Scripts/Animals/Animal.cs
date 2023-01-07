using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private AnimalMovementParameters _animalMovementParameters;
    private Vector2 _targetFlower = Vector2.zero;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void StartMovement(Vector2 targetPoint)
    {
        _targetFlower = targetPoint;
        Vector2 direction = (_targetFlower - (Vector2)transform.position).normalized;
        _rigidbody2D.velocity = direction * _animalMovementParameters.Speed;
        _spriteRenderer.transform.right = direction;
    }
}
