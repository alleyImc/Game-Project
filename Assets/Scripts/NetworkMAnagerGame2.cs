/*using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player;

        if (numPlayers == 0)
        {
            player = Instantiate(playerPrefab1, startPos.position, startPos.rotation);
        }
        else
        {
            player = Instantiate(playerPrefab2, startPos.position, startPos.rotation);
        }

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        base.OnServerChangeScene(newSceneName);

        // Oyuncu gemilerini sahne deðiþikliðinden sonra yeniden oluþturma
        if (newSceneName == "Game1" || newSceneName == "Game2")
        {
            foreach (NetworkConnection conn in NetworkServer.connections.Values)
            {
                Transform startPos = GetStartPosition();
                GameObject player;

                if (conn.identity == null) continue;

                if (conn.identity.gameObject.name.Contains("Ship1"))
                {
                    player = Instantiate(playerPrefab1, startPos.position, startPos.rotation);
                }
                else
                {
                    player = Instantiate(playerPrefab2, startPos.position, startPos.rotation);
                }

                NetworkServer.ReplacePlayerForConnection(conn, player, true);
            }
        }
    }
}
*/