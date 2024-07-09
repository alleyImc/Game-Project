using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MeteorSpawner : NetworkBehaviour
{
    public GameObject meteorPrefab;
    public float spawnRate = 2f;
    private float timer = 0f;
    private float heightOffset = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer) //only server can use this script
        {
            InvokeRepeating("SpawnMeteor", spawnRate, spawnRate);
        }
    }

    void SpawnMeteor()
    {
        float lowestPoint = transform.position.x - heightOffset;
        float highestPoint = transform.position.x + heightOffset;
        Vector3 spawnPosition = new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y, 0);

        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, transform.rotation);
        NetworkServer.Spawn(meteor); // send meteors to clients
    }
}