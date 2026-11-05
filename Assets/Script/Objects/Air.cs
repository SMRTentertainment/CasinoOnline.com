using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : EnvironmentalObject, ISoundable
{
    private BallManager _ballManager;
    [SerializeField] private AudioClip _deathAudioClip;
    private AudioSource _deathAudioSource;
    

    private void Awake()
    {
        _deathAudioSource = GetComponent<AudioSource>();
        
        _ballManager = FindObjectOfType<BallManager>();
    }
    
    public void PlaySound()
    {
        if (_deathAudioSource == null) { Debug.Log("Empty " + _deathAudioSource); return; }
        if (_deathAudioClip == null) { Debug.Log("Empty " + _deathAudioClip); return; }

        _deathAudioSource.PlayOneShot(_deathAudioClip);
    }
    public override void Collide()
    {
        Debug.Log("¡TOCÓ LA ZONA DE MUERTE!");
        if (_ballManager != null)
        {
            _ballManager.CollideWithEnemy();
            PlaySound();
        }
    }
}
