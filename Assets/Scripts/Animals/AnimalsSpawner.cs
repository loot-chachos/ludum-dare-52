using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsSpawner : MonoBehaviour
{
    [SerializeField] private List<Animal> _animalPrefabs = null;
    [SerializeField] private AnimalsSpawnParameters _animalSpawnParameters = null;
    private float _timer = 0.0f;
    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            return;
        }
        
        _timer += Time.deltaTime;

        float spawnRate = 0.0f;
        if (GameManager.Instance.HasStarted)
        {
            spawnRate = _animalSpawnParameters.SpawnRateByWorldEvolutionPercent.Evaluate(GameManager.Instance.WorldEvolutionManager.CurrentWorldEvolutionPercent);
            spawnRate /= 1.0f + GameManager.Instance.SeedsGrid.SeedsCount() * _animalSpawnParameters.SeedSpawnMultiplier;
        }
        else
        {
            // Have some spawn during main menu
            spawnRate = _animalSpawnParameters.SpawnRateInMenu;
        }

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
            spawnPoint.y = Random.Range(_animalSpawnParameters.MinSpawnPoint.y, _animalSpawnParameters.MaxSpawnPoint.y);
            spawnPoint.x= Random.value > 0.5f ? _animalSpawnParameters.MinSpawnPoint.x : _animalSpawnParameters.MaxSpawnPoint.x;
        }
        else
        {
            spawnPoint.x = Random.Range(_animalSpawnParameters.MinSpawnPoint.x, _animalSpawnParameters.MaxSpawnPoint.x);
            spawnPoint.y = Random.value > 0.5f ? _animalSpawnParameters.MinSpawnPoint.y : _animalSpawnParameters.MaxSpawnPoint.y;
        }
        int randomIndex = Random.Range(0, _animalPrefabs.Count);
        Animal animal = Instantiate(_animalPrefabs[randomIndex], spawnPoint, Quaternion.identity, transform);

        CropCell crop = GameManager.Instance.Grid.FindRandomCropAtLeastState(PlantState.Maturity);
        if (crop == null)
        { 
            crop = GameManager.Instance.Grid.FindRandomCropAtLeastState(PlantState.Underground);
        }
        animal.StartMovement(crop);
    }
}
