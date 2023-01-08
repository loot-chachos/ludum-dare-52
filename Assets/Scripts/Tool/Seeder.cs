using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeder : DraggableTool<Seed>
{
    protected override void Action(Seed seed)
    {
        seed.PlaceSeed();
    }
}
