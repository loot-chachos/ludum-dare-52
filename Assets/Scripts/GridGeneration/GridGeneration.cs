using UnityEngine;

public static class GridGeneration
{
    public static CropCell[] InitializeGrid(GenerationDirection generatonDirection, GameObject prefab, Vector2 gridSize, Vector2 start, Vector2 padding, Transform parent)
    {
        int arraySize = (int)(gridSize.x * gridSize.y);
        CropCell[] crops = new CropCell[arraySize];

        Vector2 direction = GetPonderationDirection(generatonDirection);

        float paddingY = padding.y * direction.y;
        float paddingX = padding.x * direction.x;

        // Generate the grid
        for (int j = 0; j < gridSize.y; j++)
        {
            for (int i = 0; i < gridSize.x; i++)
            {
                GameObject currentCrop = GameObject.Instantiate(prefab, new Vector3(start.x + j * paddingX, start.y + i * paddingY), Quaternion.identity, parent);
                int index = (j % (int)gridSize.x) + i;
                crops[index] = currentCrop.GetComponent<CropCell>();
            }
        }

        return crops;
    }

    private static Vector2 GetPonderationDirection(GenerationDirection direction)
    {
        switch (direction)
        {
            case GenerationDirection.NorthEast: return new Vector2(1, 1);
            case GenerationDirection.NorthWest: return new Vector2(-1, 1);
            case GenerationDirection.SouthEast: return new Vector2(1, -1);
            case GenerationDirection.SouthWest: return new Vector2(-1, -1);
            default: return new Vector2(1, 1);
        }
    }
}
