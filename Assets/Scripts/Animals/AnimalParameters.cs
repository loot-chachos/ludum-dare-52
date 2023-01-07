using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalParameters", menuName = "ScriptableObjects/AnimalParameters")]
public class AnimalParameters : ScriptableObject
{
    [SerializeField] public float Speed = 1.0f;
    [SerializeField] public int MoneyReward = 15;
}
