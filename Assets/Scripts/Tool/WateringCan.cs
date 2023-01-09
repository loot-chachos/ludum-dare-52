using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : DraggableTool<CropCell>
{
    protected override void UseTool() {}

    protected override void Action(CropCell crop)
    {
        base.Action(crop);
        // Play watering animation
        crop.Watered();
    }
}