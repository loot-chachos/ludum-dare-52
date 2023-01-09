using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilizer : DraggableTool<CropCell>
{
    public override ToolType Type => ToolType.Fertilzer;
    public Type type { get { return typeof(CropCell); } }

    protected override void UseTool() { }

    protected override void Action(CropCell crop)
    {
        base.Action(crop);
        if (GameManager.Instance.HasStarted)
        {
            crop.Fertilize();
        }
    }
}
