using Horror.Utilities;
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
            public float AggroRadius = 5f;
            public float PatrolStopBuffer = 0.15f;
        }

        [SerializeField] Settings settings;
        public override StateMachine<GhostPayload> InstantiateStateMachine() => new StateMachine<GhostPayload>(new PatrolState(settings));

       

        class JumpState : GhostState<Settings>
        {
            public JumpState(Settings settings) : base(settings) { }
            public override IState<GhostPayload> Update(GhostPayload payload)
            {
                payload.InputValues.JumpHeld = true;

                if (payload.Target == null)
                    return new PatrolState(settings);

                if (Vector3.Distance(payload.Target.position, payload.Transform.position) >= settings.AggroRadius)
                    return new IdleState(settings);


                return this;
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
                    return new JumpState(settings);

                return this;
            }
        }

        class PatrolState : GhostState<Settings>
        {
            public PatrolState(Settings settings) : base(settings) { }
            public override IState<GhostPayload> Update(GhostPayload payload)
            {
                Vector3[] patrolPoints=PatrolPointManager.Instance.Points;
                float stopBuffer = settings.PatrolStopBuffer;
                int index = payload.PatrolDestinationIndex;
                payload.Brain.MoveTowards(patrolPoints[index]);
                if (payload.Agent.remainingDistance <= stopBuffer) payload.PatrolDestinationIndex = (index + 1) % patrolPoints.Length;
                return this;
            }
            public override void EnterState(GhostPayload payload){
                if(payload.PatrolDestinationIndex<0)  payload.PatrolDestinationIndex=0;
            }
        }
    }
}
