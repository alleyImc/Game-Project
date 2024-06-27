using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteors;
    public float spawnRate = 2;
    private float timer = 0;
    private float heightOffset = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Spawn();
            timer = 0;
        }
        
    }

    void Spawn()
    {
        float lowestPoint = transform.position.x - heightOffset;
        float heighstPoint = transform.position.x + heightOffset;

        Instantiate(meteors, new Vector3(Random.Range(lowestPoint,heighstPoint), transform.position.y, 0), transform.rotation);
    }
}
