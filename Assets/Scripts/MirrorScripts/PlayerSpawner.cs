using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Server]
    public void SpawnPlayer()
    {
        //GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        GameObject playerInstance = Instantiate(playerPrefab, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(playerInstance);
    }

}
