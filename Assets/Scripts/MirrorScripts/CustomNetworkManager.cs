using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    public GameObject playerPrefabGame1Host;
    public GameObject playerPrefabGame1Client;
    public GameObject playerPrefabGame2Host;
    public GameObject playerPrefabGame2Client;
    public GameObject playerPrefabGame3Host;
    public GameObject playerPrefabGame3Client;

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

    // Handles scene change for all games
    void OnServerSceneChanged(Scene scene, LoadSceneMode mode)
    {
        foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
        {
            if (scene.name == "Game_1")
            {
                SpawnCharacter(conn, playerPrefabGame1Host, playerPrefabGame1Client);
            }
            else if (scene.name == "Game_2")
            {
                SpawnCharacter(conn, playerPrefabGame2Host, playerPrefabGame2Client);
            }
            else if (scene.name == "Game_3")
            {
                SpawnCharacter(conn, playerPrefabGame3Host, playerPrefabGame3Client);
            }
        }
    }

    // Spawns the correct character prefab based on the game and connection ID
    void SpawnCharacter(NetworkConnectionToClient conn, GameObject hostPrefab, GameObject clientPrefab)
    {
        GameObject playerPrefab = conn.connectionId == 0 ? hostPrefab : clientPrefab; // first player server, second client
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "Game_1")
        {
            SpawnCharacter(conn, playerPrefabGame1Host, playerPrefabGame1Client);
        }
        else if (activeSceneName == "Game_2")
        {
            SpawnCharacter(conn, playerPrefabGame2Host, playerPrefabGame2Client);
        }
        else if (activeSceneName == "Game_3")
        {
            SpawnCharacter(conn, playerPrefabGame3Host, playerPrefabGame3Client);
        }
        else
        {
            base.OnServerAddPlayer(conn);
        }
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        // If client is in the main menu
        // No need to do anything, player will be spawned when scene changes to the game
    }
}
