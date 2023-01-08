using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsGrid : MonoBehaviour
{
    [SerializeField] private Seed _tilePrefab = null;
    [SerializeField] private Vector2 _gridSize = new Vector2(5, 5);
    [SerializeField] private Vector2 _cropPadding = new Vector2(1, 1);
    [SerializeField] private Vector2 _startingPos = new Vector2(0, 0);
    [SerializeField] private GenerationDirection _generatonDirection = GenerationDirection.NorthEast;
    private Seed[] _seeds = new Seed[1];

    public void SpawnGrid()
    {
        int arraySize = (int)(_gridSize.x * _gridSize.y);
        _seeds = new Seed[arraySize];
        Vector2 direction = GridGeneration.GetPonderationDirection(_generatonDirection);

        float paddingY = _cropPadding.y * direction.y;
        float paddingX = _cropPadding.x * direction.x;

        // Generate the grid
        for (int j = 0; j < _gridSize.x; j++)
        {
            for (int i = 0; i < _gridSize.y; i++)
            {
                Seed seed = Instantiate(_tilePrefab, new Vector3(_startingPos.x + j * paddingX, _startingPos.y + i * paddingY), Quaternion.identity, transform);
                int index = (j * (int)_gridSize.y) + i;
                _seeds[index] = seed;
            }
        }
    }

    public int SeedsCount()
    {
        int seedCount = 0;
        for (int i = 0; i < _seeds.Length; i++)
        {
            if (_seeds[i].IsSeeded)
            {
                seedCount++;
            }
        }
        return seedCount;
    }

    public Seed FindRandomSeed()
    {
        List<Seed> possibleSeeds = new List<Seed>();
        foreach (Seed seed in _seeds)
        {
            if (seed != null && seed.IsSeeded)
            {
                possibleSeeds.Add(seed);
            }
        }
        if (possibleSeeds.Count == 0)
        {
            return null;
        }
        return possibleSeeds[UnityEngine.Random.Range(0, possibleSeeds.Count)];
    }
}
