using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : EnvironmentalObject, ISoundable
{
    [SerializeField] private AudioClip _collisionAudioClip;

    private AudioSource _collisionAudioSource;
    private void Awake()
    {
        _collisionAudioSource = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        if (_collisionAudioSource == null) { Debug.Log("Empty " + _collisionAudioSource); return; }
        if (_collisionAudioClip == null) { Debug.Log("Empty " + _collisionAudioClip); return; }

        _collisionAudioSource.PlayOneShot(_collisionAudioClip);
    }

    public override void Collide()
    {
        PlaySound();
    }
}
