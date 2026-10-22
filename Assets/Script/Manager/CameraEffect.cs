using UnityEngine;
using DG.Tweening;

public class CameraEffect : MonoBehaviour
{
    private Camera _currentCamera;
    private BallManager _ballManager;
    private Tween zoomTween;

    [SerializeField] private float _normalOrthoSize;
    [Space]
    [SerializeField] private float _greenOrthoSize;
    [SerializeField] private float _yellowOrthoSize;
    [SerializeField] private float _redOrthoSize;
    [Space]

    [Header("Shake Settings")]
    [Range(0, 10)]
    [SerializeField] private float _shakeDuration;
    [Range(0, 10)]
    [SerializeField] private float _shakeStrength;
    [Range(0, 20)]
    [SerializeField] private int _shakeVibrato;
    [Range(0, 180)]
    [SerializeField] private float _shakeRandomness;


    private void Awake()
    {
        _currentCamera = GetComponent<Camera>();
        _ballManager = FindObjectOfType<BallManager>();

        _ballManager.Clicking += OnClicking;
        _ballManager.RaisedClick += OnRaisedClick;
    }
    private void OnClicking()
    {
        SwitchCameraZoom();
    }
    private void OnRaisedClick()
    {
        BounceCameraOrthoSize();
    }
    private void SwitchCameraZoom()
    {
        if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.WHITE) { return; }

        if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.GREEN)
        {
            if (zoomTween != null)
                zoomTween.Kill();

            zoomTween = _currentCamera.DOOrthoSize(_greenOrthoSize, 1.0f);
        }
        else if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.YELLOW)
        {
            if (zoomTween != null)
                zoomTween.Kill();

            zoomTween = _currentCamera.DOOrthoSize(_yellowOrthoSize, 1.0f);
        }
        else if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.RED)
        {
            if (zoomTween != null)
                zoomTween.Kill();

            zoomTween = _currentCamera.DOOrthoSize(_redOrthoSize, 1.0f);
        }
    }
    private void BounceCameraOrthoSize()
    {
        if (zoomTween != null)
            zoomTween.Kill();

        zoomTween = _currentCamera.DOOrthoSize(_normalOrthoSize, 0.24f).SetEase(Ease.InOutBounce);

        _currentCamera.DOShakePosition(_shakeDuration, SwitchShakeStrength(), _shakeVibrato, _shakeRandomness, true, ShakeRandomnessMode.Harmonic);
    }
    private float SwitchShakeStrength()
    {
        float newStrength = 0;

        switch (_ballManager.GetLineStatus())
        {
            case TrajectoryLine.LineStatus.GREEN:
                 newStrength = _shakeStrength * 0.15f;
                return newStrength;
            case TrajectoryLine.LineStatus.YELLOW:
                 newStrength = _shakeStrength * 0.35f;
                return newStrength;
            case TrajectoryLine.LineStatus.RED:
                newStrength = _shakeStrength;
                return newStrength;

        }
        return _shakeStrength;

    }
}