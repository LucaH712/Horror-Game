using UnityEngine;
using Unity.Netcode;
namespace Horror
{
    public class LocalObjectAuthority : NetworkBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
          if(!IsSpawned || !IsOwner) gameObject.SetActive(false);
        }

   
    }
}
