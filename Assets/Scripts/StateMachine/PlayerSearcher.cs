using UnityEngine;
using UnityEngine.AI;
using Horror.Utilities;
namespace Horror.StateMachine
{
    [System.Serializable]
    public class PlayerSearcher
    {
         NavMeshPath workingPath = new NavMeshPath();
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
            if(!IsTargetValid(payload.Agent,payload.Target)) player = null;
            
                
            
            return player != null;
        }
        bool ClosestSearch(GhostPayload payload, out GameObject player)
        {
            player = PlayerManager.Instance.ClosestPlayer(payload.Agent);
            return player != null;
        }
        bool SightLineSearch(GhostPayload payload, out GameObject player)
        {
            throw new System.NotImplementedException("Sightline Search isn't implemented yet");
        }
        public bool IsTargetValid(NavMeshAgent agent, Transform target)
        {
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
    }
}
