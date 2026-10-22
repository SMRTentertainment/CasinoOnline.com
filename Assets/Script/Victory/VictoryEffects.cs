using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _clockAudioClip;
    [SerializeField] private AudioClip _victoryBellAudioClip;

    private VictoryCollider _victoryCollider;

    private AudioSource _victoryAudioSource;

    private void Awake()
    {
        _victoryCollider = GetComponent<VictoryCollider>();
        _victoryAudioSource = GetComponent<AudioSource>();

        _victoryCollider.CountdownEnd += OnCountdownEnd;
        _victoryCollider.TriggerEntered += OnPlayerTrigger;
    }

    private void OnPlayerTrigger()
    {
        SetAudioSourceClip(_victoryAudioSource, _clockAudioClip);
        PlayAudioSourceClip(_victoryAudioSource);
    }

    private void OnCountdownEnd()
    {
        StopAudioSourceClip(_victoryAudioSource);
        SetAudioSourceClip(_victoryAudioSource, null);

        PlayAudioClip(_victoryAudioSource, _victoryBellAudioClip);
    }
    private void PlayAudioClip(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null) return;

        if (audioClip == null) return;

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

}