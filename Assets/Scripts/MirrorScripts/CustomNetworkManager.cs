using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;

    public override void OnStartServer()
    {
        base.OnStartServer();
        SceneManager.sceneLoaded += OnServerSceneChanged;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        SceneManager.sceneLoaded -= OnServerSceneChanged;
    }

    // Scene Change
    void OnServerSceneChanged(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game_2")
        {
            foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
            {
                SpawnCharacter(conn);
            }
        }
    }

    // players prefabs spawn in correct places
    void SpawnCharacter(NetworkConnectionToClient conn)
    {
        GameObject playerPrefab = conn.connectionId == 0 ? playerPrefab1 : playerPrefab2; // first player server second client
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name == "Game_2")
        {
            SpawnCharacter(conn);
        }
        else
        {
            base.OnServerAddPlayer(conn);
        }
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        //if client in main menu
        //no need to do anything,
        //player will be spawned when scene changes to game
    }
}