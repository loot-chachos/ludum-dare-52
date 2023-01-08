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
            OnMouseClick();
        }
    }

    private void OnMouseClick()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.1f, _clickableLayer);
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].TryGetComponent(out Tool tool))
            {
                if (_grabbedTool == null)
                {
                    tool.ClickTool();
                    return;
                }
            }
            else if (collider[i].TryGetComponent(out CropCell cell))
            {
                cell.Harvest();
            }
            else if (collider[i].TryGetComponent(out Animal animal))
            {
                animal.Kill();
            }
        }
        
        if (_grabbedTool)
        {
            _grabbedTool.ClickTool();
        }
    }

    public void GrabTool(Tool tool)
    {
        _grabbedTool = tool;
    }
}
