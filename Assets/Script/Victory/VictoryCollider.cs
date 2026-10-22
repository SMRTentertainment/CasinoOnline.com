using System;
using System.Collections;
using UnityEngine;

public class VictoryCollider : MonoBehaviour
{
    [SerializeField] private float _secondsToWait;

    private IEnumerator _trackPlayer;

    private GameManager _GameManager;

    public event Action TriggerEntered;
    public event Action CountdownEnd;

    private void Awake()
    {
        _GameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallManager player = collision.GetComponent<BallManager>();

        if (player != null)
        {
            StartTrackPlayerCoroutine();
            PlayerTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BallManager player = collision.GetComponent<BallManager>();

        if (player != null)
        {
            StopTrackPlayerCoroutine();
        }
    }

    private void StopTrackPlayerCoroutine()
    {
        if (_trackPlayer != null)
            StopCoroutine(_trackPlayer);
    }

    private void StartTrackPlayerCoroutine()
    {
        _trackPlayer = TrackPlayer();

        StartCoroutine(_trackPlayer);
    }

    private IEnumerator TrackPlayer()
    {
        while (_GameManager.GetCurrentBallState() == BallManager.BallState.MOVING)
        {
            yield return new WaitForSeconds(_secondsToWait);
        }
        EndCountdown();
    }

    private void PlayerTrigger()
    {
        TriggerEntered?.Invoke();
    }

    private void EndCountdown()
    {
        CountdownEnd?.Invoke();
    }

}