using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : EnvironmentalObject, ISoundable
{
    [SerializeField] private AudioClip _deathAudioClip;
    
    public void PlaySound()
    {
        if (_deathAudioClip == null) { Debug.Log("Empty " + _deathAudioClip); return; }

        AudioManager.instance.PlaySFX(_deathAudioClip);
    }
    public override void Collide()
    {
        Debug.Log("¡TOCÓ LA ZONA DE MUERTE!");
        if (BallManager.instance != null)
        {
            BallManager.instance.CollideWithEnemy();
            PlaySound();
        }
    }
}
