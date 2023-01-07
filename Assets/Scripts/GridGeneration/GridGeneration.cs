using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    private enum GenerationDirection
    {
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest,
    }

    [SerializeField] private GameObject _tilePrefab = null;
    [SerializeField] private Vector2 _gridSize = new Vector2(5, 5);
    [SerializeField] private Vector2 _cropPadding = new Vector2(1, 1);
    [SerializeField] private GenerationDirection _generatonDirection = GenerationDirection.NorthEast;

    private CropCell[] _crops = null;

    void Start()
    {
        Vector2 startingPos = transform.position;
        _crops = InitializeGrid(
            _generatonDirection,
            _tilePrefab,
            _gridSize,
            startingPos, 
            _cropPadding,
            transform);
    }

    private static CropCell[] InitializeGrid(GenerationDirection generatonDirection, GameObject prefab, Vector2 gridSize, Vector2 start, Vector2 padding, Transform parent)
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
                GameObject currentCrop = Instantiate(prefab, new Vector3(start.x + j * paddingX, start.y + i * paddingY), Quaternion.identity, parent);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
