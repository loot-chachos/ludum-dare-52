using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group = null;
    [SerializeField] private TextMeshProUGUI _textObj = null;
    [SerializeField] private List<string> _speech = null;

    private Action _dialogFinished = null;
    public event Action DialogFinished
    {
        add
        {
            _dialogFinished -= value;
            _dialogFinished += value;
        }
        remove
        {
            _dialogFinished -= value;
        }
    }

    private int _currentIndex = 0;

    // Start is called before the first frame update
    public void Show()
    {
        _group.alpha = 1.0f;
        _group.interactable = true;
        _group.blocksRaycasts = true;

        StartDialog();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
        }
    }

    // Update is called once per frame
    private void Hide()
    {
        _group.alpha = 0.0f;
        _group.interactable = false;
        _group.blocksRaycasts = false;
    }

    public void StartDialog()
    {
        _currentIndex = 0;
        _textObj.text = _speech[_currentIndex];
    }

    public void NextDialog()
    {
        _currentIndex++;
        if (_currentIndex >= _speech.Count)
        {
            if (_dialogFinished != null)
            {
                _dialogFinished();
            }

            Hide();
            return;
        }

        _textObj.text = _speech[_currentIndex];
    }
}
