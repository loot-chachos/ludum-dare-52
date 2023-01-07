using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private MenuManager _menuManager = null;
    [SerializeField] private ScoreManager _scoreManager = null;
    [SerializeField] private WorldEvolutionManager _worldEvolutionManager = null;

    public WorldEvolutionManager WorldEvolutionManager { get => _worldEvolutionManager; }
    public ScoreManager ScoreManager { get => _scoreManager; }
    public MenuManager MenuManager { get => _menuManager; }
    void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
