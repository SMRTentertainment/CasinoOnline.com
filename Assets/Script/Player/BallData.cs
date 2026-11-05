using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class BallData : MonoBehaviour
{
    [SerializeField] private Vector2 _initialPosition;

    private Rigidbody2D _ballRigibody2d;
    // private GameObject _ballGameObject;
    private SpriteRenderer _ballSpriteRenderer;
    private void Awake()
    {
        _ballRigibody2d = GetComponent<Rigidbody2D>();
        _ballSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // _ballGameObject = this.gameObject;
    }
   
    public Vector2 InitialPosition { get => _initialPosition; }
    public Rigidbody2D BallRigibody2d { get => _ballRigibody2d; }
    // public GameObject BallGameObject { get => _ballGameObject; set => _ballGameObject = value; }
    public SpriteRenderer BallSpriteRenderer { get => _ballSpriteRenderer; set => _ballSpriteRenderer = value; }
}