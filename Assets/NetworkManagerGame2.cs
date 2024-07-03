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
                CmdChangeScene(); // Bu bir command olduðu için, serverda çalýþtýðý zaman clientta da çalýþacak.
                // Commandlar bir kere çalýþtý mý, herkeste çalýþýr.
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
                
                
                Alttakileri boþver
                //GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
                //NetworkServer.Spawn(playerSpawnSystemInstance);

                //GameObject roundSystemInstance = Instantiate(roundSystem);
                //NetworkServer.Spawn(roundSystemInstance);
            }
        }*/



    }
}