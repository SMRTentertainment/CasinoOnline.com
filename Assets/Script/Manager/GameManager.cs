using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] string _nextLevelName;
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
        // Esperamos 1.5 segundos para dar tiempo a la animación de explosión de BallEffects
        StartCoroutine(RestartCurrentLevel(1.5f));
    }

    private IEnumerator RestartCurrentLevel(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        // Recarga la escena actual en la que está jugando
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}