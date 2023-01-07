using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableTool : Tool
{
    [SerializeField] private float _maxHoldDuration = 1.0f;

    private float _timer = 0.0f;

    private void Update()
    {
        if (IsGrab)
        {
            _timer += Time.deltaTime;
            if (_timer > _maxHoldDuration)
            {
                ReturnToStartPos();
                _timer = 0.0f;
            }
        }
    }
}
