using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace Horror
{
    public class PlayerManager : MonoBehaviour
    {
        public List<GameObject> Players { get; private set; } = new List<GameObject>();
        [SerializeField]private float checkTimer = 90.0f;
        private float currentCheckTimer = 0.0f;
        
        // Update is called once per frame
        void Update()
        {
            if (currentCheckTimer >= checkTimer) PollForPlayers();
            currentCheckTimer+=Time.deltaTime;
        }

        void PollForPlayers()
        {
            currentCheckTimer = 0.0f;
            var players = GameObject.FindGameObjectsWithTag("Player").Where(p => !Players.Contains(p));
            Players.AddRange(players);
        }
    }
}
