using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public enum ToolType
    {
        Seed,
        Water,
        Fertilzer,
        Shovel,
        Others
    }

    [SerializeField] private float _finalAngle = 0.0F;
    [SerializeField] private float _rotationSpeed = 0.0F;
    [SerializeField] protected ParticleSystem _particle = null;
    private bool _isGrab = false;
    private Vector2 _startPosition = Vector2.zero;

    public bool IsGrab { get => _isGrab; }
    public float FinalAngle { get => _finalAngle; }
    public float RotationSpeed { get => _rotationSpeed; }
    public virtual ToolType Type => ToolType.Others;
    protected void Start()
    {
        _startPosition = transform.position;
    }

    public void ClickTool()
    {
        if (_isGrab)
        {
            UseTool();
        }
        else
        {
            GameManager.Instance.Hand.GrabTool(this);
            _isGrab = true;
        }
    }

    public void Grap()
    {
        _isGrab = true;
    }

    public void Release()
    {
        _isGrab = false;
        transform.position = _startPosition;
    }

    protected virtual void UseTool()
    {
        //ReturnToStartPos();
    }

    protected void ReturnToStartPos()
    {
        GameManager.Instance.Hand.GrabTool(null);
        _isGrab = false;
        ResetRotation();
        transform.position = _startPosition;
    }

    protected void ResetRotation()
    {
        if (_particle != null)
        {
            _particle?.Stop();
        }
        transform.rotation = Quaternion.identity;
    }
}
