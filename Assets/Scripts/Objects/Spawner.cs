using Unity.Netcode;
using UnityEngine;

namespace Horror
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject source;
        public void Spawn()
        {
            NetworkObject clone = NetworkObject.InstantiateAndSpawn(source, NetworkManager.Singleton, position: transform.position, rotation: transform.rotation);
          
        }
    }
}
