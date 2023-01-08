using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : CropTool
{
    protected override void UseTool() { }

    public override void UseTool(CropCell crop)
    {
        if (crop != null)
        {
            crop.Watered();
        }
        ReturnToStartPos();
    }
}