using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private BallManager _ballManager;

    private void Awake()
    {
        _ballManager = GetComponent<BallManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnvironmentalObject client = collision.gameObject.GetComponent<EnvironmentalObject>();

        if (client != null)
            client.Collide();

        _ballManager.Collide();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnvironmentalObject envObj = collision.GetComponent<EnvironmentalObject>();
        if (envObj != null)
        {
            envObj.Collide();
        }
    }
}