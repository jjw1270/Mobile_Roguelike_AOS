using Mirage;
using UnityEngine;

namespace GettingStarted
{
    public class StartServer : MonoBehaviour
    {
        [SerializeField] private NetworkManager networkManager;

        private void Start() 
        {
            if (!networkManager) { return; }
            
            networkManager.Server.StartServer(networkManager.Client);
        }
    }
}