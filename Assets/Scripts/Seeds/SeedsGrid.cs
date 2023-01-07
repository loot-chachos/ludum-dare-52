using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsGrid : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab = null;
    [SerializeField] private Vector2 _gridSize = new Vector2(5, 5);
    [SerializeField] private Vector2 _cropPadding = new Vector2(1, 1);
    [SerializeField] private Vector2 _startingPos = new Vector2(0, 0);
    [SerializeField] private GenerationDirection _generatonDirection = GenerationDirection.NorthEast;

    public void SpawnGrid()
    {
        int arraySize = (int)(_gridSize.x * _gridSize.y);
        CropCell[] crops = new CropCell[arraySize];

        Vector2 direction = GridGeneration.GetPonderationDirection(_generatonDirection);

        float paddingY = _cropPadding.y * direction.y;
        float paddingX = _cropPadding.x * direction.x;

        // Generate the grid
        for (int j = 0; j < _gridSize.x; j++)
        {
            for (int i = 0; i < _gridSize.y; i++)
            {
                GameObject currentCrop = GameObject.Instantiate(_tilePrefab, new Vector3(_startingPos.x + j * paddingX, _startingPos.y + i * paddingY), Quaternion.identity, transform);
                int index = (j % (int)_gridSize.y) + i;
                crops[index] = currentCrop.GetComponent<CropCell>();
            }
        }
    }
}
