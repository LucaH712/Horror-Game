using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using Horror.Utilities;
namespace Horror
{
    public class PlayerManager : SingletonBehaviour<PlayerManager>
    {
        public List<GameObject> Players { get; private set; } = new List<GameObject>();
        [SerializeField] private float checkTimer = 5.0f;
        private float currentCheckTimer = 0.0f;

        // Update is called once per frame
        void Update()
        {
            if (currentCheckTimer >= checkTimer) PollForPlayers();
            currentCheckTimer += Time.deltaTime;
        }

        void PollForPlayers()
        {
            currentCheckTimer = 0.0f;
            var players = GameObject.FindGameObjectsWithTag("Player").Where(p => !Players.Contains(p));
            Players.AddRange(players);
        }
        public GameObject RandomPlayer()
        {
            int count = Players.Count;
            if (count == 0) return null;
            int randIndex = Random.Range(0, count);
            return Players[randIndex];
        }
        public GameObject ClosestPlayer(NavMeshAgent navMeshAgent)
        {
            GameObject closestPlayer = null;
            float closestDist = Mathf.Infinity;
            NavMeshPath workingPath = new NavMeshPath();
            foreach (GameObject player in Players)
            {
                bool pathFound = navMeshAgent.CalculatePath(player.transform.position, workingPath);
                if (!pathFound || workingPath.status != NavMeshPathStatus.PathComplete)
                {
                    continue;
                }
                float dist = workingPath.CalculateDistance();
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestPlayer = player;
                }
            }
            return closestPlayer;
        }
       
    }
}
