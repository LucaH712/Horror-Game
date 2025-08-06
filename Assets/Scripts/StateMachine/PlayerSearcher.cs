using UnityEngine;
using UnityEngine.AI;
using Horror.Utilities;
namespace Horror.StateMachine
{
    [System.Serializable]
    public class PlayerSearcher
    {   
        [SerializeField] float sightLineMaxRadius = 5;
        [SerializeField]float sightLineMinRadius = 0.1f;
        [SerializeField] float rayCastLength = 10;
        [SerializeField]private Vector3 startPoint;
        [SerializeField] int pointsPerRadial = 20;
        [SerializeField] int sightline_rays;
        [SerializeField] float sightline_radials;
        NavMeshPath _workingPath;
        NavMeshPath workingPath
        {
            get
            {
                if (_workingPath == null)
                {
                    _workingPath = new NavMeshPath();
                }
                return _workingPath;
            }
        }
        RaycastHit[] _raycastHits;
        RaycastHit[] raycastHits
        {
            get
            {
                if (_raycastHits == null || _raycastHits.Length != sightline_rays)
                    _raycastHits = new RaycastHit[sightline_rays];

                return _raycastHits;
            }
        }
        [SerializeField] private float maxDist = 20.0f;
        private enum SearchTypes
        {
            Random,
            Closest,
            SightLine
        }

        [SerializeField] private SearchTypes searchType;
        public bool Search(GhostPayload payload, out GameObject player)
        {
            player = null;
            bool success = false;
            switch (searchType)
            {
                case SearchTypes.Random:
                    success = RandomSearch(payload, out player);
                    break;
                case SearchTypes.Closest:
                    success = ClosestSearch(payload, out player);
                    break;
                case SearchTypes.SightLine:
                    success = SightLineSearch(payload, out player);
                    break;
            }
            return success;
        }
        bool RandomSearch(GhostPayload payload, out GameObject player)
        {
            player = PlayerManager.Instance.RandomPlayer();
            if (player !=null && !IsTargetValid(payload.Agent, player.transform)) player = null;
            return player != null;
        }
        bool ClosestSearch(GhostPayload payload, out GameObject player)
        {
            GameObject[] players = PlayerManager.Instance.SortClosestPlayers(payload.Agent);
            player = GetFirstValidPlayer(payload.Agent, players);
            return player != null;
        }
        bool SightLineSearch(GhostPayload payload, out GameObject player)
        {
            player = PlayerManager.Instance.SightlineRaycasts(payload.Transform, raycastHits, sightline_radials, sightLineMinRadius, sightLineMaxRadius, maxDist);
            return player!= null;
        }
        public bool IsTargetValid(NavMeshAgent agent, Transform target)
        {
            if (searchType == SearchTypes.SightLine) return true;
            bool pathFound = agent.CalculatePath(target.position, workingPath);
            if (!pathFound || workingPath.status != NavMeshPathStatus.PathComplete)
            {
                return false;
            }
            float dist = workingPath.CalculateDistance();
            if (dist > maxDist)
                return false;
            return true;
        }
        GameObject GetFirstValidPlayer(NavMeshAgent agent, GameObject[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (IsTargetValid(agent, players[i].transform))
                {
                    return players[i];
                }
            }
            return null;
        }
    }
}
