using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldEvolutionParameters", menuName = "ScriptableObjects/WorldEvolutionParameters")]
public class WorldEvolutionParameters : ScriptableObject
{
    [SerializeField] public float TintedMaxSpeedPercentPerSeconds = 1;
    [SerializeField] public SpriteRenderer TintedFilter = null;
    [SerializeField] public Gradient ColorGradient = null;
    [SerializeField] public float FertilizerWorldIncreasePerSeconds = 1;
}
