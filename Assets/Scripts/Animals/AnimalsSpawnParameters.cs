using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsSpawnParameters", menuName = "ScriptableObjects/AnimalsSpawnParameters")]
public class AnimalsSpawnParameters : ScriptableObject
{
    [SerializeField] public AnimationCurve SpawnRateByWorldEvolutionPercent = null;
    [SerializeField] public Vector2 MinSpawnPoint = Vector2.zero;
    [SerializeField] public Vector2 MaxSpawnPoint = Vector2.zero;
}
