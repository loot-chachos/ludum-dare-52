using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Animal : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private AnimalParameters _animalParameters;
    [SerializeField] private float _destroyDistanceFromCenter = 10.0f;
    private IEatable _target = null;

    [SerializeField] private UnityEvent _onKilled;
    [SerializeField] private GameObject _particlePrefab = null;

    private Action _eat = null;

    public event Action Eat
    {
        add
        {
            _eat -= value;
            _eat += value;
        }
        remove
        {
            _eat -= value;
        }
    }
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void StartMovement(IEatable targetPoint)
    {
        _target = targetPoint;
        Vector2 direction = (((MonoBehaviour)_target).transform.position - transform.position).normalized;
        _rigidbody2D.velocity = direction * _animalParameters.Speed;
        float angle = Vector2.Angle(Vector2.right, direction) * Mathf.Sign(Vector2.Dot(Vector2.down, direction));
        _animator.SetFloat("Angle", angle);
        Eat += AnimateEat;
    }

    private void Update()
    {
        if (_target != null && _animalParameters.CanEat)
        {
            if (Vector2.Distance(((MonoBehaviour)_target).transform.position, transform.position) < 0.2f)
            {
                if (_target is Seed seed && seed.IsSeeded)
                {
                    seed.Eat();
                    _eat.Invoke();
                }

                if (_target is CropCell crop && crop.HostedPlant != null && crop.HostedPlant.State >= PlantState.Maturity && crop.HostedPlant.StoleByAnimalsCount < 2)
                {
                    crop.Stolen();
                    _eat.Invoke();
                }
            }
        }

        if ((GameManager.Instance.GameViewCenter - transform.position).sqrMagnitude > _destroyDistanceFromCenter * _destroyDistanceFromCenter)
        {
            Destroy(gameObject);
        }
    }

    public void Kill()
    {
        GameManager.Instance.ScoreManager.OnMoneyAdded(_animalParameters.MoneyReward);
        GameManager.Instance.WorldEvolutionManager.OnKillAnimals(_animalParameters.WorldPercentEvolution);
        _onKilled.Invoke();
        Instantiate(_particlePrefab, transform.position, transform.rotation);
        Eat -= AnimateEat;
        Destroy(gameObject);
    }

    public void AnimateEat()
    {
        _animator.SetTrigger("Eat");
    }
}