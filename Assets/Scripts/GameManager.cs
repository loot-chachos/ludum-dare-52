using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")]
    [SerializeField] private MenuManager _menuManager = null;
    [SerializeField] private ScoreManager _scoreManager = null;
    [SerializeField] private WorldEvolutionManager _worldEvolutionManager = null;
    [SerializeField] private AnimalsSpawner _animalSpawner = null;
    [SerializeField] private DialogManager _dialogManager = null;
    [SerializeField] private AudioManager _audioManager = null;

    [Header("Others")]
    [SerializeField] private Vector3 _gameViewPosition = Vector3.zero;
    [SerializeField] private Garden _menuGardenSettings = null;
    [SerializeField] private Garden _gardenSettings = null;
    [SerializeField] private SeedsGrid _seedsGrid = null;
    [SerializeField] private Hand _hand = null;

    private TransitionManager _transitionManager = null;
    private bool _isPlaying = false;
    private bool _isPaused = false;
    private Grid _grid = null;
    private GameObject _menuGridRoot = null;
    private GameObject _gameplayGridRoot = null;

    public WorldEvolutionManager WorldEvolutionManager { get => _worldEvolutionManager; }
    public ScoreManager ScoreManager { get => _scoreManager; }
    public AudioManager AudioManager { get => _audioManager; }
    public MenuManager MenuManager { get => _menuManager; }
    public bool HasStarted { get => _isPlaying; }
    public bool IsGamePaused { get => _isPaused; }
    public Grid Grid { get => _grid;}
    public SeedsGrid SeedsGrid { get => _seedsGrid; }
    public Hand Hand { get => _hand; }
    public Vector3 GameViewCenter
    {
        get
        {
            return HasStarted ? new Vector3(_gameViewPosition.x, _gameViewPosition.y, 0) : Vector3.zero;
        }
    }

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

        _audioManager.PlayMenuMusic();
        _transitionManager = new TransitionManager();
    }

    private void Start()
    {
        // Destroy gameplay grid if any
        if (_gameplayGridRoot != null)
        {
            Destroy(_gameplayGridRoot);
        }

        // Init real gameplay grid.
        _menuGridRoot = new GameObject();
        _menuGridRoot.name = "MenuGrid";

        // Spawn menu grid to allow animal spawn
        _grid = new Grid(_menuGardenSettings);
        _grid.InitializeGrid(_menuGridRoot.transform);
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartGame();
        }
#endif // UNITY_EDITOR

        if (_isPlaying)
        {
            _grid?.Update();
        }
    }

    public void StartGame()
    {
        // Init real gameplay grid.
        _gameplayGridRoot = new GameObject();
        _gameplayGridRoot.name = "GameplayGrid";

        // Plant grid
        _grid = new Grid(_gardenSettings);
        _grid.InitializeGrid(_gameplayGridRoot.transform);
        _grid.ActivateGrid();
        _grid.SeedFirstPlant(27);

        // Food for animals grid
        _seedsGrid.SpawnGrid();

        _transitionManager.IsTransitionFinished += OnIntroTransitionFinished;
        _transitionManager.StartIntroTransition(_gameViewPosition);
    }

    private void OnIntroTransitionFinished()
    {
        _transitionManager.IsTransitionFinished -= OnIntroTransitionFinished;

        _animalSpawner.Clean();
        // Destroy menu grid
        if (_menuGridRoot != null)
        {
            Destroy(_menuGridRoot);
        }

        // HUD
        _scoreManager.Show();

        _dialogManager.DialogFinished += OnDialogFinished;
        _audioManager.PlayDialogMusic();
        _dialogManager.Show();
    }

    public void OnDialogFinished()
    {
        // Others
        _animalSpawner.OnStartGame();

        _audioManager.PlayInGameMusic();
        _isPlaying = true;
    }

    public void EndGame(bool isWin)
    {
        _scoreManager.Hide();
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
