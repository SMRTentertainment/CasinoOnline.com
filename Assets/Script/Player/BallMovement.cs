using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private BallManager _ballManager;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _ballManager = GetComponent<BallManager>();

        _ballManager.BallStopped += OnStopBall;
        _ballManager.BallExploded += OnBallExplode;
    }
    private void SetBallPosition(Vector3 newPos)
    {
        _rigidbody2D.position = newPos;
    }
    private void OnStopBall()
    {
        //if (_ballManager.IsSpaceValid())
            SetBallPosition(_rigidbody2D.position + Vector2.up);
    }

    private void OnBallExplode()
    {
        SetBallPosition(_ballManager.ObtainInitialPosition());
    }
}