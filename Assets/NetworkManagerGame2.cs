using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Game2
{
    [AddComponentMenu("")]
    public class NetworkManagerGame2 : NetworkManager
    {
        public Transform leftShipSpawn;
        public Transform rightShipSpawn;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            // add player at correct spawn position
            Transform start = numPlayers == 0 ? leftShipSpawn : rightShipSpawn;
            GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);


            if (numPlayers == 2)
            {
                //if player number is 2 do something
            }
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}