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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, _clickableLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Tool tool))
            {
                if (_grabbedTool == tool && _grabbedTool is CropTool cropTool)
                {
                    UnityEngine.Debug.Log(_grabbedTool);
                    foreach (Collider2D collider in colliders)
                    {
                        UnityEngine.Debug.Log(collider);
                        if (collider.TryGetComponent(out CropCell cell))
                        {
                            UnityEngine.Debug.Log(cell);
                            cropTool.UseTool(cell);
                            return;
                        }
                    }
                    cropTool.UseTool(null);
                    return;
                }

                tool.ClickTool();
            }
            else if (colliders[i].TryGetComponent(out CropCell cell))
            {
                if (_grabbedTool == null)
                {
                    cell.Harvest();
                }
            }
            else if (colliders[i].TryGetComponent(out Animal animal))
            {
                animal.Kill();
            }
        }
    }

    public void GrabTool(Tool tool)
    {
        _grabbedTool = tool;
    }
}
