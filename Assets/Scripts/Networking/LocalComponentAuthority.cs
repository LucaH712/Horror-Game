using UnityEngine;
using Unity.Netcode;

namespace Horror
{
    public class LocalComponentAuthority : NetworkBehaviour
    {
        [SerializeField] MonoBehaviour[] components;

        void Awake()
        {
            foreach (MonoBehaviour component in components)
            {
                component.enabled = enabled;
            }
        }
        void Start()
        {
            bool enabled = IsSpawned && IsOwner;
            foreach (MonoBehaviour component in components)
            {
                component.enabled = enabled;
            }
        }
    }
}
