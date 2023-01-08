using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private MenuManager _menuManager = null;
    [SerializeField] private ScoreManager _scoreManager = null;
    [SerializeField] private WorldEvolutionManager _worldEvolutionManager = null;
    [SerializeField] private Garden _gardenSettings = null;
    [SerializeField] private SeedsGrid _seedsGrid = null;
    [SerializeField] private Hand _hand = null;

    private bool _isPlaying = false;
    private Grid _grid = null;
    private GameObject _gridRoot = null;

    public WorldEvolutionManager WorldEvolutionManager { get => _worldEvolutionManager; }
    public ScoreManager ScoreManager { get => _scoreManager; }
    public MenuManager MenuManager { get => _menuManager; }
    public bool HasStarted { get => _isPlaying; }
    public Grid Grid { get => _grid;}
    public SeedsGrid SeedsGrid { get => _seedsGrid; }
    public Hand Hand { get => _hand; }

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
        _grid?.Update();
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
        //_grid.SeedPlant(4);
        //_grid.SeedPlant(2);
        //_grid.SeedPlant(7);
        _seedsGrid.SpawnGrid();
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
