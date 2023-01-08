using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    private bool _isGrab = false;

    private Vector2 _startPosition = Vector2.zero;

    public bool IsGrab { get => _isGrab; }

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
        ReturnToStartPos();
    }

    protected void ReturnToStartPos()
    {
        GameManager.Instance.Hand.GrabTool(null);
        _isGrab = false;
        transform.position = _startPosition;
    }
}
