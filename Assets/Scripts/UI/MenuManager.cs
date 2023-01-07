using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel = null;
    [SerializeField] private GameObject _winPanel = null;
    [SerializeField] private GameObject _gameOverPanel = null;

    private void Start()
    {
        _startPanel.SetActive(true);
        _winPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
    }

    // Called by UI
    public void StartGame()
    {
        _startPanel.SetActive(false);
    }

    // Called by UI
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called by GameManager
    public void DisplayGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    // Called by GameManager
    public void DisplayWinPanel()
    {
        _winPanel.SetActive(true);
    }
}
