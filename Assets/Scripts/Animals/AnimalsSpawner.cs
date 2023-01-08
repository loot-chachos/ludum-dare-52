using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsSpawner : MonoBehaviour
{
    [SerializeField] private List<Animal> _animalPrefabs = null;
    [SerializeField] private AnimalsSpawnParameters _menuAnimalSpawnParameters = null;
    [SerializeField] private AnimalsSpawnParameters _animalSpawnParameters = null;
    
    private float _timer = 0.0f;
    private List<GameObject> _animalSpawned = null;
    private AnimalsSpawnParameters _currentActiveSpawner = null;

    private void Start()
    {
        _animalSpawned = new List<GameObject>();
        OnBackMainMenu();
    }

    public void OnStartGame()
    {
        _currentActiveSpawner = _animalSpawnParameters;
    }

    public void OnBackMainMenu()
    {
        _currentActiveSpawner = _menuAnimalSpawnParameters;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused || GameManager.Instance.HasStarted == false)
        {
            return;
        }
        
        _timer += Time.deltaTime;

        float spawnRate = _currentActiveSpawner.SpawnRateByWorldEvolutionPercent.Evaluate(GameManager.Instance.WorldEvolutionManager.CurrentWorldEvolutionPercent);
        spawnRate /= 1.0f + GameManager.Instance.SeedsGrid.SeedsCount() * _currentActiveSpawner.SeedSpawnMultiplier;

        if (_timer > spawnRate)
        {
            _timer = 0.0f;
            SpawnAnAnimal();
        }
    }

    private void SpawnAnAnimal()
    {
        bool isVertical = Random.value > 0.5f;
        Vector2 spawnPoint = Vector2.zero;
        if (isVertical)
        {
            spawnPoint.y = Random.Range(_currentActiveSpawner.MinSpawnPoint.y, _currentActiveSpawner.MaxSpawnPoint.y);
            spawnPoint.x = Random.value > 0.5f ? _currentActiveSpawner.MinSpawnPoint.x : _currentActiveSpawner.MaxSpawnPoint.x;
        }
        else
        {
            spawnPoint.x = Random.Range(_currentActiveSpawner.MinSpawnPoint.x, _currentActiveSpawner.MaxSpawnPoint.x);
            spawnPoint.y = Random.value > 0.5f ? _currentActiveSpawner.MinSpawnPoint.y : _currentActiveSpawner.MaxSpawnPoint.y;
        }
        int randomIndex = Random.Range(0, _animalPrefabs.Count);
        Animal animal = Instantiate(_animalPrefabs[randomIndex], spawnPoint, Quaternion.identity, transform);
        _animalSpawned.Add(animal.gameObject);

        Seed seed = GameManager.Instance.SeedsGrid.FindRandomSeed();

        CropCell crop = GameManager.Instance.Grid.FindRandomCropBetweenState(PlantState.Maturity, PlantState.Great);

        if (seed == null && crop == null)
        {
            crop = GameManager.Instance.Grid.FindRandomCrop();
            animal.StartMovement(crop);
        }
        else if (seed == null)
        {
            animal.StartMovement(crop);
        }
        else if (crop == null)
        {
            animal.StartMovement(seed);
        }
        else
        {
            if (Random.value > 0.75f)
            {
                animal.StartMovement(seed);
            }
            else
            {
                animal.StartMovement(crop);
            }
        }
    }

    public void Clean()
    {
#if UNITY_EDITOR
        Debug.Log("Clean Animal Spawner");
#endif 
        foreach (var animal in _animalSpawned)
        {
            Destroy(animal);
        }
    }
}
