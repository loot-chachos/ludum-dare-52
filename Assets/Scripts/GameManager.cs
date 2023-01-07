using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private MenuManager _menuManager = null;
    [SerializeField] private Garden _gardenSettings = null;
    [SerializeField] private ScoreManager _scoreManager = null;
    [SerializeField] private WorldEvolutionManager _worldEvolutionManager = null;

    private bool _isPlaying = false;
    private Grid _grid = null;
    private GameObject _gridRoot = null;

    public WorldEvolutionManager WorldEvolutionManager { get => _worldEvolutionManager; }
    public ScoreManager ScoreManager { get => _scoreManager; }
    public MenuManager MenuManager { get => _menuManager; }
    public bool HasStarted { get => _isPlaying; }

    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
#endif // UNITY_EDITOR
    }

    public void StartGame()
    {
        _isPlaying = true;
        
        if (_gridRoot == null)
        {
            _gridRoot = new GameObject();
            _gridRoot.name = "GardenGrid";
        }

        _grid = new Grid(_gardenSettings);
        _grid.InitializeGrid(_gridRoot.transform);
        _grid.SeedPlant(5);
    }

    public void EndGame(bool isWin)
    {
        _isPlaying = false;
        if (isWin)
        {
            _menuManager.DisplayWinPanel();
        }
        else
        {
            _menuManager.DisplayGameOverPanel();
        }
    }
}
