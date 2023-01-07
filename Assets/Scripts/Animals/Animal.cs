using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Animal : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private AnimalParameters _animalParameters;
    [SerializeField] private float _destroyDistanceFromCenter = 10.0f;
    private Vector2 _targetFlower = Vector2.zero;

    [SerializeField] private UnityEvent _onKilled;
    [SerializeField] private GameObject _particlePrefab = null;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void StartMovement(Vector2 targetPoint)
    {
        _targetFlower = targetPoint;
        Vector2 direction = (_targetFlower - (Vector2)transform.position).normalized;
        _rigidbody2D.velocity = direction * _animalParameters.Speed;
        _spriteRenderer.transform.right = direction;
    }

    private void Update()
    {
        if (transform.position.sqrMagnitude > _destroyDistanceFromCenter * _destroyDistanceFromCenter)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        GameManager.Instance.ScoreManager.OnMoneyAdded(_animalParameters.MoneyReward);
        GameManager.Instance.WorldEvolutionManager.OnKillAnimals();
        _onKilled.Invoke();
        Instantiate(_particlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}