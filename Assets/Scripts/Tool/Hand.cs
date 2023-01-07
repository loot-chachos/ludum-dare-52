using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private LayerMask _toolLayer;
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
    }

    private void OnMouseDown()
    {
        if (_grabbedTool == null)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, _toolLayer);
            if (collider != null && collider.TryGetComponent(out Tool tool))
            {
                tool.ClickTool();
                return;
            }
        }
    }

    public void GrabTool(Tool tool)
    {
        _grabbedTool = tool;
    }
}
