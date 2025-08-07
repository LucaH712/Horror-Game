using Horror.Utilities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
namespace Horror.StateMachine
{
    [CreateAssetMenu(fileName = "GhostStateMachineObject", menuName = "State Machines/GhostStateMachineObject")]
    public class GhostStateMachineObject : GhostStateMachineObjectBase

    {
       
        [System.Serializable]
        class Settings
        {
            public float PlayerValidationCheckInterval = 1.0f;
            public PlayerSearcher Searcher;
            public float PlayerSearchInterval = 3.0f;
            public float AggroRadius = 5f;
            public float PatrolStopBuffer = 0.15f;
            public bool UseNavMesh = true;
        }


        [SerializeField] Settings settings;
        public override StateMachine<GhostPayload> InstantiateStateMachine() => new StateMachine<GhostPayload>(new PatrolState(settings));



        class ChaseState : GhostState<Settings>
        {
            public ChaseState(Settings settings) : base(settings) { }
            float timeSinceLastValidCheck;
            public override IState<GhostPayload> Update(GhostPayload payload)
            {

                if (payload.Target == null)
                    return new PatrolState(settings);
                if (timeSinceLastValidCheck > settings.PlayerValidationCheckInterval)
                {
                    timeSinceLastValidCheck = 0.0f;
                    if (!settings.Searcher.IsTargetValid(payload.Agent,payload.Target)) return new PatrolState(settings);
                }
                timeSinceLastValidCheck += Time.deltaTime;
                payload.Brain.MoveTowards(payload.Target.position,settings.UseNavMesh);
                return this;
            }
            public override void EnterState(GhostPayload payload)
            {
                timeSinceLastValidCheck = 0.0f;
            }
        }

        class IdleState : GhostState<Settings>
        {
            public IdleState(Settings settings) : base(settings) { }
            public override IState<GhostPayload> Update(GhostPayload payload)
            {
                payload.InputValues.JumpHeld = false;

                if (payload.Target == null)
                    return new PatrolState(settings);

                if (Vector3.Distance(payload.Target.position, payload.Transform.position) < settings.AggroRadius)
                    return new ChaseState(settings);

                return this;
            }
        }

        class PatrolState : GhostState<Settings>
        {
            float timeSinceLastSearch;
            public PatrolState(Settings settings) : base(settings) { }
            public override IState<GhostPayload> Update(GhostPayload payload)
            {
                Vector3[] patrolPoints = PatrolPointManager.Instance.Points;
                float stopBuffer = settings.PatrolStopBuffer;
                int index = payload.PatrolDestinationIndex;
                payload.Brain.MoveTowards(patrolPoints[index],settings.UseNavMesh);
                float dist = settings.UseNavMesh ? payload.Agent.remainingDistance : Vector3.Distance(payload.Transform.position, patrolPoints[index]);
                if (dist <= stopBuffer) payload.PatrolDestinationIndex = (index + 1) % patrolPoints.Length;
                    timeSinceLastSearch += Time.deltaTime;
                if (timeSinceLastSearch >= settings.PlayerSearchInterval)
                {
                    timeSinceLastSearch = 0.0f;

                    bool foundPlayer = settings.Searcher.Search(payload, out GameObject player);
                    if (foundPlayer) payload.Target = player.transform;
                    return new ChaseState(settings);
                }
                return this;
            }
            public override void EnterState(GhostPayload payload)
            {
                payload.Target = null;
                timeSinceLastSearch = 0.0f;
                if (payload.PatrolDestinationIndex < 0) payload.PatrolDestinationIndex = 0;
            }
        }
    }
}
