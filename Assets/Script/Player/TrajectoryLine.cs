using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    [Tooltip("Max length of the LineRenderer")]
    [Range(1, 20)] [SerializeField] private float _radius = 5f;

    private BallManager _ballManager;
    private LineRenderer _lineRenderer;
    private LineStatus _lineStatus;
    private Tween _colorTween;

    public LineStatus CurrenLineStatus { get => _lineStatus; }

    public enum LineStatus
    {
        WHITE,
        GREEN,
        YELLOW,
        RED,
    }

    private void Awake()
    {
        _ballManager = GetComponent<BallManager>();
        _lineRenderer = GetComponent<LineRenderer>();

        _ballManager.Clicked += OnClicked;
        _ballManager.Clicking += OnClicking;
        _ballManager.RaisedClick += OnRaisedClick;

        _lineRenderer.enabled = false;
    }

    private void OnClicked()
    {
        _lineRenderer.enabled = true;
        RenderInitialPosition();
    }

    private void OnClicking()
    {
        RenderEndPosition();
        SwitchLineRendererColors();
    }

    private void OnRaisedClick()
    {
        _lineStatus = LineStatus.WHITE;
        _lineRenderer.enabled = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        // Asignamos a Z la distancia exacta de la cámara al plano 0 del juego (normalmente Z = 10)
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void RenderInitialPosition()
    {
        Vector3 startPoint = GetMouseWorldPosition();

        _lineRenderer.SetPosition(0, startPoint);
        _lineRenderer.SetPosition(1, startPoint);
    }

    private void RenderEndPosition()
    {
        Vector3 endPoint = GetMouseWorldPosition();
        endPoint = CheckDistance(_lineRenderer.GetPosition(0), endPoint);

        _lineRenderer.SetPosition(1, endPoint);
    }

    public Vector3 ObtainLineRendererPosition()
    {
        // CORREGIDO: Resta explícita entre el punto inicial y el punto final de la línea
        Vector3 lineRendererPosition = _lineRenderer.GetPosition(0) - _lineRenderer.GetPosition(1);
        return lineRendererPosition;
    }

    private Vector3 CheckDistance(Vector3 centerPosition, Vector3 newPosition)
    {
        float distance = Vector3.Distance(newPosition, centerPosition);

        if (distance > _radius)
        {
            // CORREGIDO: Resta de vectores para obtener la dirección normalizada
            Vector3 fromOriginToObject = newPosition - centerPosition;
            fromOriginToObject *= _radius / distance;

            Vector3 newVector = centerPosition + fromOriginToObject;
            return newVector;
        }

        return newPosition;
    }

    private void SetLineRendererStartColor(LineRenderer lineRenderer, Color2 startColor, Color2 endColor)
    {
        if (_colorTween != null) _colorTween.Kill();

        _colorTween = lineRenderer.DOColor(startColor, endColor, 0.48f);
    }

    private void SwitchLineRendererColors()
    {
        float distance = Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
        
        switch (distance)
        {
            case >= 7:
                SetLineRendererStartColor(_lineRenderer, new Color2(_lineRenderer.startColor, _lineRenderer.endColor), new Color2(Color.red, Color.red));
                _lineStatus = LineStatus.RED;
                break;
            case >= 3:
                SetLineRendererStartColor(_lineRenderer, new Color2(_lineRenderer.startColor, _lineRenderer.endColor), new Color2(Color.yellow, Color.yellow));
                _lineStatus = LineStatus.YELLOW;
                break;
            case >= 0:
                SetLineRendererStartColor(_lineRenderer, new Color2(_lineRenderer.startColor, _lineRenderer.endColor), new Color2(Color.green, Color.green));
                _lineStatus = LineStatus.GREEN;
                break;
        }
    }
}