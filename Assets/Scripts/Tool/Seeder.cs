using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeder : DraggableTool<Seed>
{
    protected override void UseTool() { }

    protected override void Action(Seed seed)
    {
        base.Action(seed);
        seed.PlaceSeed();
    }
}
