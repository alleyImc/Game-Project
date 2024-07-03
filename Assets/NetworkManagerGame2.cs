using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        /*[Server]
        public void StartGame()
        {
         if(Event buttonx clicked)
            {
                CmdChangeScene(); // Bu bir command oldu�u i�in, serverda �al��t��� zaman clientta da �al��acak.
                // Commandlar bir kere �al��t� m�, herkeste �al���r.
            }
        }

        [Command]
        public void CmdChangeScene1()
        {
            SceneManager.LoadScene("1.Sahne");
        }


        [Command]
        public void CmdChangeScene2()
        {
            SceneManager.LoadScene("2.Sahne");
        }*/

        // Bu olmazsa


        /*public override void OnServerSceneChanged(string sceneName)
        {
            if (sceneName.StartsWith("MainMenu") && Event buttonx clicked)
            {
                
                
                Alttakileri bo�ver
                //GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
                //NetworkServer.Spawn(playerSpawnSystemInstance);

                //GameObject roundSystemInstance = Instantiate(roundSystem);
                //NetworkServer.Spawn(roundSystemInstance);
            }
        }*/



    }
}