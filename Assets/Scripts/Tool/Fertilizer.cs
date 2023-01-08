using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilizer : DraggableTool
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CropCell crop))
        {
            if (GameManager.Instance.HasStarted)
            {
                crop.Fertilize();
            }
        }
    }
}
