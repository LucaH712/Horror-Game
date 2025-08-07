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
        public GameObject SightLineSearch(float Radius, Transform head, RaycastHit[] raycasts, float pointsPerRadial, float minRadius, float maxRadius, float maxDistance, Vector3 startPoint)
        {
            return null;
            // SpawnRaycasts(sightLineMaxRadius);           
        }
        public GameObject SightlineRaycasts(Transform head, RaycastHit[] raycasts, float radials, float minRadius, float maxRadius, float maxDistance)
        {
            //* Goal: Find a player in one of these rays we shoot out
            //* Parameters:
            //* head: our head we're casting from
            //* raycasts: an allocated array to place our casts in
            //* radials: how many full circles we make in our spiral casting
            //* minRadius: the start radius of our cast
            //* maxRadius: the end radius of our cast (cone)
            //* maxDistance: how far does this cone shoot out

            GameObject player = null;
            float fullCircles_radians = Mathf.PI * 2f * radials;
            int casts = raycasts.Length;
            for (int i = 0; i < casts; i++)
            {
                float percent = (float)i / casts;
                float rad = percent * fullCircles_radians; //* PI * 2 is one full circle, radials provides how many circles we want to do total
                Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * percent;

                Vector3 startPoint = pos * minRadius;
                Vector3 endPoint = pos * maxRadius + Vector3.forward * maxDistance;

                ///* Transform local start and end point to our worldstart and direction (relative from the head)
                Vector3 worldStart = head.TransformPoint(startPoint);
                Vector3 dir = head.TransformDirection(endPoint - startPoint);

                //* hit returns true if the raycast found something
                bool hit = UnityEngine.Physics.Raycast(worldStart, dir, out raycasts[i], maxDistance);
                bool valid = false; //* We set the hit as valid if we find a player

                if (hit && raycasts[i].collider.tag == "Player")
                {
                    //* Player found! get the gameobject
                    valid = true;
                    player = raycasts[i].collider.gameObject;
                    //? This could be optimized by just returning the player the first time it's found, but that would halt the DrawRays portion
                }
                Debug.DrawRay(worldStart, dir * maxDistance, valid ? Color.red : Color.green, .2f);
            }

            return player;
        }
    public GameObject SpawnRaycasts(float startRadius, float Decrement,float minRadius,float castDistance,int pointsPerRadial, Transform head)
        {
            GameObject player = null;
            for (float i = startRadius; i > minRadius; i -= Decrement)
            {
                player = DrawCircleInRays(i, castDistance, pointsPerRadial, head);
                if (player != null)
                {
                    return player;
                }
            }
            return player;
        }
        public GameObject[] SortClosestPlayers(NavMeshAgent agent)
        {
            NavMeshPath workingPath = new NavMeshPath();

            return Players.OrderBy(player =>
            {
                bool pathFound = agent.CalculatePath(player.transform.position, workingPath);
                if (!pathFound || workingPath.status != NavMeshPathStatus.PathComplete)
                {
                    return Mathf.Infinity;
                }
                return workingPath.CalculateDistance();
            }).ToArray();
        }

    
    GameObject DrawCircleInRays(float radius, float castDistance, int pointsPerRadial, Transform head) {
            float AngleIncrement = 360 / pointsPerRadial;
            GameObject player = null;
            for (float i = 0; i < 360; i += AngleIncrement)
            {
                float angle = i;
                float xLength = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float yLength = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                Vector3 circle = new Vector3(xLength, yLength, castDistance);
                Vector3 direction = head.TransformDirection(circle);
                bool hit = UnityEngine.Physics.Raycast(head.position, direction, out RaycastHit raycast, castDistance);
                if (hit && raycast.collider.tag == "Player")
                {
                    //* Player found! get the gameobject

                    player = raycast.collider.gameObject;
                    //? This could be optimized by just returning the player the first time it's found, but that would halt the DrawRays portion
                    // return player;

                }
                Debug.DrawRay(head.position, direction * castDistance, Color.red, .5f);
                
            }
            return player;
        }
    }
}
