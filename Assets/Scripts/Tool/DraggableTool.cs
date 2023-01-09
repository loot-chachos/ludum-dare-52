using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableTool<T> : Tool
{
    [SerializeField] private bool _isDurationDriven = false;
    [SerializeField] private float _maxHoldDuration = 1.0f;

    private bool _isActive = false;
    private float _timer = 0.0f;
    private float _currentZEulerAngles;

    public bool IsActive { get { return _isActive; } }

    private void Update()
    {
        if (IsGrab)
        {
            RotateTool();

            if (_isDurationDriven)
            {
                _timer += Time.deltaTime;
                if (_timer > _maxHoldDuration)
                {
                    ReturnToStartPos();
                    _timer = 0.0f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                ReturnToStartPos();
            }
            else if (Input.GetMouseButton(0))
            {
                Activated();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isActive = false;
                // Reset rotation
                transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y, 0.0F);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActive && collision.TryGetComponent(out T crop))
        {
            Action(crop);
        }
    }

    protected virtual void Action(T seed){ }

    private void RotateTool()
    {
        if (transform.eulerAngles.z != FinalAngle)
        {
            _currentZEulerAngles += Time.deltaTime * RotationSpeed;
            transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y, _currentZEulerAngles);
        }
        
    }

    protected void Activated()
    {
        _isActive = true;

        Hand hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
        Collider2D[] colliders = hand.FindCollision();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out T cell))
            {
                Action(cell);
            }
        }

    }
}
