    using System;
    using UnityEngine;

    [RequireComponent(typeof(BallInput), typeof(BallPhysics), typeof(BallData))]
    public class BallManager : MonoBehaviour
    {
        [SerializeField] private BallState _ballState;

        private BallPhysics _ballPhysics;
        private BallData _ballData;
        private TrajectoryLine _TrajectoryLine;
        public static BallManager instance;

        public event Action BallStopped;
        public event Action BallCollideWithEnemy;
        public event Action BallCollide;
        public event Action BallExploded;
        public event Action Clicked;
        public event Action Clicking;
        public event Action RaisedClick;
        public BallState CurrentBallState { get => _ballState; }


        public enum BallState
        {
            IDLE,
            MOVING,
            DEATH,
        }

        private void Awake()
        {
            _ballPhysics = GetComponent<BallPhysics>();
            _ballData = GetComponent<BallData>();
            _TrajectoryLine = GetComponent<TrajectoryLine>();
            instance = this;
        }

        private void Update()
        {
            ChangeCurrentBallState();
        }

        private BallState ChangeCurrentBallState()
        {
            if (_ballState == BallState.DEATH) return BallState.DEATH;

            if (_ballPhysics.IsGrounded() && !_ballPhysics.IsMoving())
            {
                if (_ballState != BallState.IDLE)
                    StopBall();
                return _ballState = BallState.IDLE;
            }

            return _ballState = BallState.MOVING;
        }


        public bool CheckCurrentBallState(BallState state)
        {
            if (CurrentBallState == state)
            {
                return true;
            }
            return false;
        }
        public void SetBallState(BallState newState)
        {
            _ballState = newState;
        }

        public bool IsSpaceValid()
        {
            return _ballPhysics.HasEnoughSpace();
        }

        public Vector2 ObtainInitialPosition()
        {
            return _ballData.InitialPosition;
        }

        public Rigidbody2D ObtainRigidbody()
        {
            return _ballData.BallRigibody2d;
        }

        // public GameObject ObtainGameObject()
        // {
        //     return _ballData.BallGameObject;
        // }

        public SpriteRenderer ObtainSpriteRenderer()
        {
            return _ballData.BallSpriteRenderer;
        }
        public TrajectoryLine.LineStatus GetLineStatus()
        {
            return _TrajectoryLine.CurrenLineStatus;
        }

        public void Click()
        {
            Clicked?.Invoke();
        }

        public void HoldClick()
        {
            Clicking?.Invoke();
        }

        public void ReleaseClick()
        {
            RaisedClick?.Invoke();
        }
        private void StopBall()
        {
            BallStopped?.Invoke();
        }

        public void CollideWithEnemy()
        {
            BallCollideWithEnemy?.Invoke();
        }

        public void Collide()
        {
            BallCollide?.Invoke();
        }
        public void ExplodeBall()
        {
            SetBallState(BallState.DEATH);
            BallExploded?.Invoke();
            SetBallState(BallState.IDLE);

        }
    }