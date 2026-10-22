using System;
using UnityEngine;

public class BallInput : MonoBehaviour
{
    private BallManager _ballManager;

    private bool _wasClicked;
    private void Awake()
    {
        _ballManager = GetComponent<BallManager>();
    }
    private void Update()
    {
        if (_ballManager == null) { Debug.LogWarning("Empty " + _ballManager); return; }

        if (_ballManager.CheckCurrentBallState(BallManager.BallState.IDLE))
            CheckInput(Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.GetMouseButtonUp(0));
    }
    private void CheckInput(bool input1, bool input2, bool input3)
    {
        if (_ballManager == null) return;

        if (input1)
        {
            _ballManager.Click();
            _wasClicked = true;
        }

        if (input2 && _wasClicked)
        {
            _ballManager.HoldClick();
        }

        if (input3 && _wasClicked)
        {
            _ballManager.ReleaseClick();
            _wasClicked = false;
        }
    }
}