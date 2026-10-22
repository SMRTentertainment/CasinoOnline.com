using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _ballDeathAudioClip;
    [SerializeField] private AudioClip _ballKickAudioClip;
    [SerializeField] private AudioClip _ballThrowAudioClip;
    [Space]
    [Header("Pitch Effect Settings")]
    [Range(0.5f, 2.0f)]
    [SerializeField] float _greenPitch;
    [Range(0.5f, 2.0f)]
    [SerializeField] float _yellowPitch;
    [Range(0.5f, 2.0f)]
    [SerializeField] float _redPitch;

    private AudioSource _ballAudioSource;
    private BallManager _ballManager; //.8 pitch, 1.5
    private Tween _audioPitchTween;
    private void Awake()
    {
        _ballManager = GetComponent<BallManager>();
        _ballAudioSource = GetComponent<AudioSource>();

        _ballManager.BallCollideWithEnemy += OnExplode;
        _ballManager.BallCollide += OnBallCollide;
        _ballManager.RaisedClick += OnRaisedClick;
        _ballManager.Clicked += OnClicked;
        _ballManager.Clicking += OnClicking;
    }
    private void OnExplode()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        Debug.Log("Inicio corrutina");
        yield return new WaitForSeconds(2);
        PlayFxWithSound(_ballAudioSource, _ballDeathAudioClip);
        _ballManager.ExplodeBall();
    }

    private void OnClicked()
    {
        _ballAudioSource.loop = true;
        SetAudioSourceClip(_ballAudioSource, _ballThrowAudioClip);
        PlayAudioSourceClip(_ballAudioSource);
    }
    private void OnClicking()
    {
        SwitchAudioSourcePitch(_ballAudioSource);
    }

    private void OnRaisedClick()
    {
        _ballAudioSource.loop = false;
        StopAudioSourceClip(_ballAudioSource);
        SetAudioSourceClip(_ballAudioSource, null);
        KillTween(_audioPitchTween);
        ResetPitch(_ballAudioSource);

        PlayFxWithSound(_ballAudioSource, _ballKickAudioClip);
    }
    private void OnBallCollide()
    {
        //DeformTheBall();
    }
    private void DeformTheBall()
    {
        float xVelocity = Mathf.Abs(_ballManager.ObtainRigidbody().linearVelocity.x);
        float yVelocity = Mathf.Abs(_ballManager.ObtainRigidbody().linearVelocity.y);

        if (xVelocity > yVelocity)
        {
            if (xVelocity > 10.0f)
            {
                _ballManager.ObtainSpriteRenderer().transform.DOScaleY(1.25f, 0.12f).OnComplete(() =>
                {
                    _ballManager.ObtainSpriteRenderer().transform.DOScaleY(1, 0.12f);
                });
            }
            else if (xVelocity > 5.0f && xVelocity < 10.0f)
            {
                _ballManager.ObtainSpriteRenderer().transform.DOScaleY(1.15f, 0.12f).OnComplete(() =>
                {
                    _ballManager.ObtainSpriteRenderer().transform.DOScaleY(1, 0.12f);
                });
            }
        }
        else
        {
            if (yVelocity > 7.5f)
            {
                _ballManager.ObtainSpriteRenderer().transform.DOScaleY(1.25f, 0.12f).OnComplete(() =>
                {
                    _ballManager.ObtainSpriteRenderer().transform.DOScaleY(1, 0.12f);
                });
            }
            else if (yVelocity > 2.5f && yVelocity < 7.5f)
            {
                _ballManager.ObtainSpriteRenderer().transform.DOScaleX(1.15f, 0.12f).OnComplete(() =>
                {
                    _ballManager.ObtainSpriteRenderer().transform.DOScaleX(1, 0.12f);
                });
            }
        }

    }


    private void SwitchAudioSourcePitch(AudioSource audioSource)
    {
        if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.WHITE) return;

        if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.GREEN)
        {
            KillTween(_audioPitchTween);

            _audioPitchTween = audioSource.DOPitch(_greenPitch, 0.24f);
        }

        else if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.YELLOW)
        {
            KillTween(_audioPitchTween);

            _audioPitchTween = audioSource.DOPitch(_yellowPitch, 0.24f);
        }

        else if (_ballManager.GetLineStatus() == TrajectoryLine.LineStatus.RED)
        {
            KillTween(_audioPitchTween);

            _audioPitchTween = audioSource.DOPitch(_redPitch, 0.24f);
        }

    }

    private void PlayFxWithSound(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null) return;

        audioSource.PlayOneShot(audioClip);
    }

    private void SetAudioSourceClip(AudioSource audioSource, AudioClip newAudioClip)
    {
        if (audioSource != null)
            audioSource.clip = newAudioClip;
    }

    private void PlayAudioSourceClip(AudioSource audioSource)
    {
        if (audioSource == null) return;

        if (audioSource.clip == null) return;

        audioSource.Play();

    }
    private void StopAudioSourceClip(AudioSource audioSource)
    {
        if (audioSource == null) return;

        if (audioSource.clip == null) return;

        audioSource.Stop();
    }
    private void ResetPitch(AudioSource audioSource)
    {
        audioSource.pitch = 1.0f;
    }
    private void KillTween(Tween currentTween)
    {
        if (currentTween != null)
            currentTween.Kill();
    }
}