using Unity.Netcode;
using UnityEngine;

namespace Horror
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject source;
        void Awake()
        {
            Init();
        }
        void OnValidate()
        {
            Init();
        }
        void Init()
        {
            if (source.scene != null)
            {
                 source.SetActive(false);
            }
           
        }
        public void Spawn()
        {
            NetworkObject clone = NetworkObject.InstantiateAndSpawn(source, NetworkManager.Singleton, position: transform.position, rotation: transform.rotation);
            clone.gameObject.SetActive(true);
        }
    }
}
