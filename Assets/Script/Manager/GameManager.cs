using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float _sceneLoadingTime;

    private BallManager _ballManager;
    private VictoryCollider _victoryCollider;

    private void Awake()
    {
        _ballManager = FindObjectOfType<BallManager>();
        _victoryCollider = FindObjectOfType<VictoryCollider>();

        if (_victoryCollider != null)
            _victoryCollider.CountdownEnd += OnCountdownEnd;
        
        if (_ballManager != null)
            _ballManager.BallCollideWithEnemy += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        if (_victoryCollider != null)
            _victoryCollider.CountdownEnd -= OnCountdownEnd;

        if (_ballManager != null)
            _ballManager.BallCollideWithEnemy -= OnPlayerDeath;
    }
    
    public BallManager.BallState GetCurrentBallState()
    {
        return _ballManager.CurrentBallState;
    }

    private void OnCountdownEnd()
    {
        StartCoroutine(WaitForLoading(_sceneLoadingTime));
    }
    private void LoadTheNextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
    private IEnumerator WaitForLoading(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        LoadTheNextLevel();
    }
    
    private void OnPlayerDeath()
    {
        StartCoroutine(RestartCurrentLevel(1.5f));
    }

    private IEnumerator RestartCurrentLevel(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}