using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : CropTool
{
    private CropCell _storedCell = null;

    protected override void UseTool() { }

    public override void UseTool(CropCell crop)
    {
        if (_storedCell != null)
        {
            if (crop != null)
            {
                crop.Bury(_storedCell.HostedPlant);
            }
            else
            {
                _storedCell.Bury(_storedCell.HostedPlant);
            }
            _storedCell = null;
            ReturnToStartPos();
        }
        else
        {
            if (crop != null && crop.HostedPlant != null)
            {
                _storedCell = crop;
                _storedCell.Move();
            }
            else
            {
                ReturnToStartPos();
            }
        }
    }
}
