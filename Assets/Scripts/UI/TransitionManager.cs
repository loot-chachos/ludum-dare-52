using System;
using System.Collections;
using UnityEngine;

public class TransitionManager
{
    private float _transitionSpeed = 0.01f;
    private Vector3 _gameView = Vector3.zero;

    private Action _isTransitionFinished = null;
    public event Action IsTransitionFinished
    {
        add
        {
            _isTransitionFinished -= value;
            _isTransitionFinished += value;
        }
        remove
        {
            _isTransitionFinished -= value;
        }
    }

    public void StartIntroTransition(Vector3 gameView)
    {
        _gameView = gameView;
        GameManager.Instance.StartCoroutine(StartCameraTransition());
    }

    private IEnumerator StartCameraTransition()
    {
        Camera camera = Camera.main;
        float currentTime = 0.0f;
        while ((camera.transform.position - _gameView).sqrMagnitude > 0.001f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, _gameView, currentTime);
            currentTime += Time.deltaTime * _transitionSpeed;
            yield return new WaitForEndOfFrame();
        }

        if (_isTransitionFinished != null)
        {
            _isTransitionFinished();
        }
    }
}
