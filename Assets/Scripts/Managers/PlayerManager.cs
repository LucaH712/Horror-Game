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
        public GameObject SightLineSearch()
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
        /*void SpawnRaycasts(float Radius) {
            float AngleIncrement = 360 / pointsPerRadial;
            float angle = 0;
            for (int i = 0; i < pointsPerRadial; i++)
            {
                
                Vector3 endPoint= Vector3.zero;
                float CenterToPointAngleY;
                float CenterToPointAngleX = 0.0f;
                float xLength = Mathf.Cos(angle) * Radius;
                CenterToPointAngleX = Mathf.Atan2(xLength, rayCastLength);
                float yLength=Mathf.Sin(angle)*Radius;
                CenterToPointAngleY = Mathf.Atan2(yLength, rayCastLength);
                Vector2 StartToEndAngle = new Vector3(CenterToPointAngleX, CenterToPointAngleY);
                float xMag = rayCastLength/Mathf.Cos(CenterToPointAngleX);
                float mag = xMag/Mathf.Cos(CenterToPointAngleY);

                
                //Calculate End Point;
                //? endPoint = new Vector3(IdkTheX, IdkTheY, rayCastLength);
                //Gets the directionVector;
                Vector3 DirectionVector = endPoint - startPoint;
                if (angle == 360)
                {
                    SpawnRaycasts(Radius - 0.1f);
                }
                //Spawn the actual raycast, then increase the angle, keep going until angle is 360
                //When angle is 360, we run the function again with a smaller radius

                //Raycasts are done, increase angle and do again;
                angle += AngleIncrement;
            }
        }*/
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
       
    }
}
