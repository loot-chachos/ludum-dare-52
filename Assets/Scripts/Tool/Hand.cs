using System;
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

    [SerializeField] private GameObject _hand = null;
    [SerializeField] private GameObject _hoe = null;
    [SerializeField] private GameObject _aim = null;
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

    private void FixedUpdate()
    {
        if (_grabbedTool != null)
        {
            _hand.SetActive(false);
            _hoe.SetActive(false);
            _aim.SetActive(false);
        }
        else
        {
            bool hand = false;
            bool hoe = false;
            bool aim = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, _clickableLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out CropCell cell) && cell.HostedPlant != null && cell.HostedPlant.State >= PlantState.Maturity)
                {
                    hoe = true;
                    break;
                }
                if (colliders[i].TryGetComponent(out Animal animal))
                {
                    aim = true;
                    break;
                }
            }
            if (hoe == false && aim == false)
            {
                hand = true;
            }
            _hand.SetActive(hand);
            _hoe.SetActive(hoe);
            _aim.SetActive(aim);
        }
    }
    

    public Collider2D[] FindCollision()
    {
        return Physics2D.OverlapCircleAll(transform.position, 0.5f, _clickableLayer);
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
                    GameManager.Instance.AudioManager.PlayHarvest();
                    cell.Harvest();
                }
                else if (colliders[i].TryGetComponent(out Animal animal))
                {
                    GameManager.Instance.AudioManager.PlayShoot();
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
        // Release previous tool
        _grabbedTool?.Release();
        // Assign new one
        _grabbedTool = tool;
        _grabbedTool?.Grap();
    }
}
