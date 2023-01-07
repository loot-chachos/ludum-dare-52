using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldEvolutionParameters")]
public class WorldEvolutionParameters : ScriptableObject
{
    [SerializeField] public float FertilizerDegradationPercent = 1;
    [SerializeField] public float KillDegradationPercent = 1;
    [SerializeField] public float TintedMaxSpeedPercentPerSeconds = 1;
    [SerializeField] public SpriteRenderer TintedFilter = null;
    [SerializeField] public Gradient ColorGradient = null;
}
