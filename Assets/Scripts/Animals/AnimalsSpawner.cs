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
        if (GameManager.Instance.HasStarted)
        {
            _timer += Time.deltaTime;

            float spawnRate = _animalSpawnParameters.SpawnRateByWorldEvolutionPercent.Evaluate(GameManager.Instance.WorldEvolutionManager.CurrentWorldEvolutionPercent);
            if (_timer > spawnRate)
            {
                _timer = 0.0f;
                SpawnAnAnimal();
            }
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
        CropCell crop = GameManager.Instance.Grid.FindRandomCropAtLeastState(PlantState.Underground);
        animal.StartMovement(crop.transform.position);
    }
}
