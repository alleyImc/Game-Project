using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Discovery
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Network Discovery HUD")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-discovery")]
    [RequireComponent(typeof(NetworkDiscovery))]
    public class NetworkDiscoveryHUD : MonoBehaviour
    {
        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        Vector2 scrollViewPos = Vector2.zero;

        public NetworkDiscovery networkDiscovery;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GetComponent<NetworkDiscovery>();
                UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
            }
        }
#endif

        void OnGUI()
        {
            if (NetworkManager.singleton == null)
                return;

            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
                DrawGUI();

            if (NetworkServer.active || NetworkClient.active)
                StopButtons();
        }

        void DrawGUI()
        {
            float width = 700;
            float height = 1400; // Yüksekliği artırdık
            float x = (Screen.width - width) / 2;
            float y = 170; // Y ekseni değerini 200 olarak ayarladık

            // Arka plan rengi
            Color buttonColor = new Color(1.0f, 0.75f, 0.8f); // pembe renk

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 40; // Yazı boyutunu büyüttük
            buttonStyle.normal.textColor = Color.white;
            buttonStyle.normal.background = MakeTex(2, 2, buttonColor); // pembe arka plan

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 40; // Yazı boyutunu büyüttük

            GUILayout.BeginArea(new Rect(x, y, width, height));
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Sunucuları Bul", buttonStyle, GUILayout.Height(100))) // Düğme yüksekliğini arttırdık
            {
                discoveredServers.Clear();
                networkDiscovery.StartDiscovery();
            }

            // LAN Host
            if (GUILayout.Button("Host Başlat", buttonStyle, GUILayout.Height(100))) // Düğme yüksekliğini arttırdık
            {
                discoveredServers.Clear();
                NetworkManager.singleton.StartHost();
                networkDiscovery.AdvertiseServer();
            }

            // Dedicated server
            if (GUILayout.Button("Sunucu Başlat", buttonStyle, GUILayout.Height(100))) // Düğme yüksekliğini arttırdık
            {
                discoveredServers.Clear();
                NetworkManager.singleton.StartServer();
                networkDiscovery.AdvertiseServer();
            }

            GUILayout.EndHorizontal();

            GUILayout.Label($"Keşfedilen Sunucular [{discoveredServers.Count}]:", labelStyle);

            scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);

            foreach (ServerResponse info in discoveredServers.Values)
                if (GUILayout.Button(info.EndPoint.Address.ToString(), buttonStyle, GUILayout.Height(100))) // Düğme yüksekliğini arttırdık
                    Connect(info);

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        void StopButtons()
        {
            float width = 400;
            float height = 200; // Durdur butonunun yüksekliğini 200 olarak ayarladık
            float x = (Screen.width - width) / 2;
            float y = 40; // Y ekseni değerini 40 olarak ayarladık (yukarı taşıdık)

            // Arka plan rengi
            Color buttonColor = new Color(1.0f, 0.75f, 0.8f); // pembe renk

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 40; // Yazı boyutunu büyüttük
            buttonStyle.normal.textColor = Color.white;
            buttonStyle.normal.background = MakeTex(2, 2, buttonColor); // pembe arka plan

            GUILayout.BeginArea(new Rect(x, y, width, height)); // Düğme alanını genişlettik ve yüksekliğini arttırdık

            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                if (GUILayout.Button("Host Durdur", buttonStyle, GUILayout.Height(100))) // Düğme yüksekliğini arttırdık
                {
                    NetworkManager.singleton.StopHost();
                    networkDiscovery.StopDiscovery();
                }
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Client Durdur", buttonStyle, GUILayout.Height(100))) // Düğme yüksekliğini arttırdık
                {
                    NetworkManager.singleton.StopClient();
                    networkDiscovery.StopDiscovery();
                }
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Sunucu Durdur", buttonStyle, GUILayout.Height(100))) // Düğme yüksekliğini arttırdık
                {
                    NetworkManager.singleton.StopServer();
                    networkDiscovery.StopDiscovery();
                }
            }

            GUILayout.EndArea();
        }

        void Connect(ServerResponse info)
        {
            networkDiscovery.StopDiscovery();
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }

        // Buton arka planı için doku oluşturma metodu
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}
