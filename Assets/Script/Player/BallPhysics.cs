using UnityEngine;

[RequireComponent(typeof(BallInput), typeof(TrajectoryLine))]
public class BallPhysics : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float _forceMultiplier;
    [Range(0.05f, 10f)]
    [SerializeField] private float _extraGroundHeight;
    [Range(0.05f, 10f)]
    [SerializeField] private float _extraSkyHeight;
    [SerializeField] private LayerMask _GroundLayer;

    private TrajectoryLine _trajectoryLine;
    private Rigidbody2D _rigidbody2d;
    private CircleCollider2D _CircleCollider;
    private BallManager _ballManager;

    private void Awake()
    {
        _ballManager = GetComponent<BallManager>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _CircleCollider = GetComponent<CircleCollider2D>();
        _trajectoryLine = GetComponent<TrajectoryLine>();


        _ballManager.RaisedClick += OnRaisedClick;
        _ballManager.BallStopped += OnStopBall;
        _ballManager.BallCollideWithEnemy += OnCollideWithEnemy;

    }

    private void Start()
    {
        OnStopBall();
    }

    private void OnRaisedClick()
    {
        SetBallGravityScale(1);
        ApplyForceToBall();
    }
    private void OnStopBall()
    {
        SetBallGravityScale(0);
        SetBallVelocity(Vector2.zero);
        SetBallAngularVelocity(0);
        SetBallRotation(0);
    }
    private void OnCollideWithEnemy()
    {
        SetBallGravityScale(0);
        SetBallVelocity(Vector2.zero);
        SetBallAngularVelocity(0);
        SetBallRotation(0);
    }

    private void ApplyForceToBall()
    {
        Vector3 inputForce = _trajectoryLine.ObtainLineRendererPosition();

        _rigidbody2d.AddForce(inputForce * _forceMultiplier, ForceMode2D.Impulse);
    }

    public bool IsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.Raycast(_CircleCollider.bounds.center, Vector2.down, _CircleCollider.bounds.extents.y + _extraGroundHeight, _GroundLayer);

        Color rayColor;

        if (rayCastHit.collider != null)
        {
            rayColor = Color.green;
            Debug.DrawRay(_CircleCollider.bounds.center, Vector2.down * (_CircleCollider.bounds.extents.y + _extraGroundHeight), rayColor);
            return true;
        }
        else
        {
            rayColor = Color.red;
            Debug.DrawRay(_CircleCollider.bounds.center, Vector2.down * (_CircleCollider.bounds.extents.y + _extraGroundHeight), rayColor);
            return false;
        }


    }

    public bool IsMoving()
    {
        Vector2 minVelocity = new Vector2(0.1f, 0);
        Vector2 ballVelocity = new Vector2(Mathf.Abs(_rigidbody2d.linearVelocity.x), Mathf.Abs(_rigidbody2d.linearVelocity.y));

        if (ballVelocity.x > minVelocity.x || ballVelocity.y > minVelocity.y)
        {
            return true;
        }
        return false;
    }

    public bool HasEnoughSpace()
    {
        RaycastHit2D rayCastHit = Physics2D.Raycast(_CircleCollider.bounds.center, Vector2.up, _CircleCollider.bounds.extents.y + _extraSkyHeight, _GroundLayer);
        Color rayColor;

        if (rayCastHit.collider != null)
        {
            rayColor = Color.red;
            Debug.DrawRay(_CircleCollider.bounds.center, Vector2.up * (_CircleCollider.bounds.extents.y + _extraSkyHeight), rayColor);
            return false;
        }
        else
        {
            rayColor = Color.green;
            Debug.DrawRay(_CircleCollider.bounds.center, Vector2.up * (_CircleCollider.bounds.extents.y + _extraSkyHeight), rayColor);
            return true;
        }
    }

    private void SetBallGravityScale(float newGravity)
    {
        _rigidbody2d.gravityScale = newGravity;
    }
    private void SetBallVelocity(Vector2 newVelocity)
    {
        _rigidbody2d.linearVelocity = newVelocity;
    }

    private void SetBallAngularVelocity(float newAngularVelocity)
    {
        _rigidbody2d.angularVelocity = newAngularVelocity;
    }

    private void SetBallRotation(float newRotation)
    {
        _rigidbody2d.rotation = newRotation;
    }
}