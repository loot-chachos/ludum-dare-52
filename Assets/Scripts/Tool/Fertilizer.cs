using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilizer : DraggableTool<CropCell>
{
    public Type type { get { return typeof(CropCell); } }

    protected override void Action(CropCell crop)
    {
        if (GameManager.Instance.HasStarted)
        {
            crop.Fertilize();
        }
    }
}
