using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsSpawnParameters", menuName = "ScriptableObjects/AnimalsSpawnParameters")]
public class AnimalsSpawnParameters : ScriptableObject
{
    [Header("In Menu")]
    [SerializeField] public float SpawnRateInMenu = 0.0f;

    [Header("In Game")]
    [SerializeField] public AnimationCurve SpawnRateByWorldEvolutionPercent = null;
    [SerializeField] public Vector2 MinSpawnPoint = Vector2.zero;
    [SerializeField] public Vector2 MaxSpawnPoint = Vector2.zero;
    [SerializeField] public float SeedSpawnMultiplier = 0.0f;
}
