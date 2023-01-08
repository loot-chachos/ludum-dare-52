using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private LayerMask _clickableLayer;

    [SerializeField] private KeyCode _fertilizerKeyCode = KeyCode.R;
    [SerializeField] private KeyCode _seederKeyCode  = KeyCode.E;
    [SerializeField] private KeyCode _shovelKeyCode = KeyCode.W;
    [SerializeField] private KeyCode _wateringKeyCode = KeyCode.Q;
    private Tool _grabbedTool = null;

    public Tool GrabbedTool { get => _grabbedTool; }

    private void Update()
    {
        if (GameManager.Instance.HasStarted == false)
        {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        transform.position = mousePosition;
        if (_grabbedTool != null)
        {
            _grabbedTool.transform.position = mousePosition;
        }

        if (Input.GetKeyDown(_fertilizerKeyCode))
        {
            Tool tool = FindObjectOfType(typeof(Fertilizer)) as Tool;
            if (tool != null)
            {
                GrabTool(tool);
            }
        }
        else if (Input.GetKeyDown(_seederKeyCode))
        {
            Tool tool = FindObjectOfType(typeof(Seeder)) as Tool;
            if (tool != null)
            {
                GrabTool(tool);
            }
        }
        else if (Input.GetKeyDown(_shovelKeyCode))
        {
            Tool tool = FindObjectOfType(typeof(Shovel)) as Tool;
            if (tool != null)
            {
                GrabTool(tool);
            }
        }
        else if (Input.GetKeyDown(_wateringKeyCode))
        {
            Tool tool = FindObjectOfType(typeof(WateringCan)) as Tool;
            if (tool != null)
            {
                GrabTool(tool);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
    }

    private void OnMouseClick()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, _clickableLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (_grabbedTool == null)
            {
                if (colliders[i].TryGetComponent(out Tool tool))
                {
                    tool.ClickTool();
                    return;
                }
                else if (colliders[i].TryGetComponent(out CropCell cell))
                {
                    cell.Harvest();
                }
                else if (colliders[i].TryGetComponent(out Animal animal))
                {
                    animal.Kill();
                }
            }
        }

        if (_grabbedTool != null)
        {
            if (_grabbedTool is CropTool cropTool)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.TryGetComponent(out CropCell cell))
                    {
                        cropTool.UseTool(cell);
                        return;
                    }
                }
                cropTool.UseTool(null);
                return;
            }
            else
            {
                _grabbedTool.ClickTool();
            }
        }
    }

    public void GrabTool(Tool tool)
    {
        _grabbedTool = tool;
    }
}
