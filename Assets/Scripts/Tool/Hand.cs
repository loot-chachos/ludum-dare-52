using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private LayerMask _clickableLayer;
    private Tool _grabbedTool = null;

    public Tool GrabbedTool { get => _grabbedTool; }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        transform.position = mousePosition;
        if (_grabbedTool != null)
        {
            _grabbedTool.transform.position = mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnMoouseClick();
        }
    }

    private void OnMoouseClick()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, _clickableLayer);
        if (collider != null)
        {
            if (collider.TryGetComponent(out Tool tool))
            {
                if (_grabbedTool == null)
                {
                    tool.ClickTool();
                }
                return;
            }
            else if (collider.TryGetComponent(out CropCell cell))
            {
                cell.Harvest();
                return;
            }
        }
    }

    public void GrabTool(Tool tool)
    {
        _grabbedTool = tool;
    }
}
