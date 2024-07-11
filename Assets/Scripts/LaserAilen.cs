using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAilen : MonoBehaviour
{
    public float lifeTime = 5.0f; // Time in seconds before the laser is destroyed

    void Start()
    {
        // Destroy the laser after 'lifeTime' seconds
        Destroy(gameObject, lifeTime);
    }

    void OnBecameInvisible()
    {
        // Destroy the laser when it goes off-screen
        Destroy(gameObject);
    }
}
