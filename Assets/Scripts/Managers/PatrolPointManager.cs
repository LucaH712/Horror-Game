using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.AI;
namespace Horror

{
    public class PatrolPointManager : SingletonBehaviour<PatrolPointManager>
    {
        public Vector3[] Points;
        // Update is called once per frame
        void Start()
        {
            Points = FindPoints();
        }

        Vector3[] FindPoints()
        {
            Vector3[] points = GameObject.FindGameObjectsWithTag("PatrolPoint").Select(obj => obj.transform.position).ToArray();
            return ValidatePoints(points);
        }
        Vector3[] ValidatePoints(Vector3[] points)
        {
            List<Vector3> validPoints= new List<Vector3>();
            foreach (Vector3 point in points)
            {
                if (NavMesh.SamplePosition(point, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
                {
                    validPoints.Add(hit.position);
                }
            }
            return validPoints.ToArray();
        }
    }
}
