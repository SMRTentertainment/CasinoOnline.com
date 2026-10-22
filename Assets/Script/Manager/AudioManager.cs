using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _levelMusic;
    [SerializeField] private AudioSource _musicAudioSource;

    private void Awake()
    {
        SetAudioClip(_musicAudioSource, _levelMusic);
        PlayAudioSource(_musicAudioSource);
    }

    private void PlayAudioClip(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioClip == null || audioSource == null) return;

        audioSource.PlayOneShot(audioClip);
    }

    private void PlayAudioSource(AudioSource audioSource)
    {
        if (audioSource == null) return;

        audioSource.Play();
    }

    private void SetAudioClip(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioClip == null || audioSource == null) return;

        audioSource.clip = audioClip;
           
    }
}