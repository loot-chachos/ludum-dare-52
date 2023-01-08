using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private enum PanelAvailable : int
    {
        MainMenu,
        Win,
        GameOver,
        Options,
        Credits
    }

    [Header("Should follow the order define in the PanelAvailable Enum")]
    [SerializeField] private List<GameObject> _panels = new List<GameObject>();

    private GameObject _currentPanel = null;

    private void Start()
    {
        _currentPanel = _panels[0];
        for (int i = 0; i < _panels.Count; i++)
        {
            _panels[i].SetActive(false);
        }

        _currentPanel.SetActive(true);
    }

    public void DisplayGameOverPanel()
    {
        _panels[(int)PanelAvailable.GameOver].SetActive(true);
    }

    public void DisplayWinPanel()
    {
        _panels[(int)PanelAvailable.Win].SetActive(true);
    }

    #region Called by the UI
    public void StartGame()
    {
        _currentPanel.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisplayOptions()
    {
        _currentPanel.SetActive(false);
        _panels[(int)PanelAvailable.Options].SetActive(true);
    }

    public void DisplayCredits()
    {
        _currentPanel.SetActive(false);
        _panels[(int)PanelAvailable.Credits].SetActive(true);
    }

    public void BackButton()
    {
        _currentPanel.SetActive(false);
        _panels[(int)PanelAvailable.MainMenu].SetActive(true);
    }
    #endregion Called by the UI
}
