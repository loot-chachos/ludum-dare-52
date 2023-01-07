using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalMovementParameters", menuName = "ScriptableObjects/AnimalMovementParameters")]
public class AnimalMovementParameters : ScriptableObject
{
    [SerializeField] public float Speed = 1.0f;
}
