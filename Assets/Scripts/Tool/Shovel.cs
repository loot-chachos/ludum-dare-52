using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : CropTool
{
    private CropCell _storedCell = null;
    private Plant _storedPlant = null;

    protected override void UseTool() { }
    public override ToolType Type => ToolType.Shovel;

    public override void UseTool(CropCell crop)
    {
        if (_storedCell != null)
        {
            if (crop != null)
            {
                _storedCell.Bury(crop.HostedPlant);
                crop.Bury(_storedPlant);
            }
            else
            {
                _storedCell.Bury(_storedPlant);
            }
            _storedCell = null;
            //ReturnToStartPos();
        }
        else
        {
            if (crop != null && crop.HostedPlant != null)
            {
                _storedPlant = crop.HostedPlant;
                _storedCell = crop;
                _storedCell.Move();
            }
            else
            {
                ReturnToStartPos();
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ReturnToStartPos();
        }
    }
}
