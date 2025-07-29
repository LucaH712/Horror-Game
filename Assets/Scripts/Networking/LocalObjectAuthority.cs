using UnityEngine;
using Unity.Netcode;
using Horror.Utilities;
namespace Horror
{
    public class LocalObjectAuthority : NetworkBehaviour
    {
    [SerializeField] private GameObject[] targets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn()
    {
      for (int i = 0; i < targets.Length; i++)
      {
        targets[i].SetActive(this.CanControl());
      }
    }

   
    }
}
