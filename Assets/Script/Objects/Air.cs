using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : EnvironmentalObject/*, IEnemy*/
{
    private BallManager _ballManager;

    private void Awake()
    {
        _ballManager = FindObjectOfType<BallManager>();
    }
    public override void Collide()
    {
        //_ballManager.CollideWithEnemy();
        Debug.Log("¡TOCÓ LA ZONA DE MUERTE!");
        if (_ballManager != null)
        {
            _ballManager.CollideWithEnemy();
        }
    }

    //public void DealDamage()
    //{
    //}
}
