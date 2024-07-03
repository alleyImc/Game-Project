using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerGame1 : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Vector3 startPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        GameObject player = Instantiate(playerPrefab, startPosition, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
