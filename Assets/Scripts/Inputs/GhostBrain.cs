using Horror.StateMachine;
using UnityEngine;
using UnityEngine.AI;
namespace Horror.Inputs
{
    public class GhostBrain : InputBrain
    {
        private InputValues input = new InputValues();
        protected override InputValues InternalInput => input;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] GhostStateMachineObjectBase stateMachineObject;
        GhostPayload payload = new GhostPayload();
        [SerializeField] private Vector3 movementMultipliers = new Vector3(1.0f, 0.0f, 1.0f);
        [SerializeField] private Vector3 lookMultipliers = new Vector3(1.0f, 0.50f, 1.0f);
        [SerializeField] private float lookMaxSpeed = 100.0f;
        StateMachine<GhostPayload> stateMachine;

        GhostStateMachineObjectBase cached_stateMachineObject;
        void OnValidate()
        {
            if (Application.isPlaying && stateMachineObject != cached_stateMachineObject)
                GenerateStateMachine();
            if (!agent) agent = this.GetComponent<NavMeshAgent>();
        }

        void GenerateStateMachine()
        {
            if (!InputAllowed)
                return;

            FreshPayload();

            cached_stateMachineObject = stateMachineObject;
            stateMachine = stateMachineObject.InstantiateStateMachine();

            stateMachine.Restart(payload);
        }

        void FreshPayload()
        {
            payload = new GhostPayload();

            payload.Transform = transform;
            payload.InputValues = input;
            payload.Target = null;
            payload.Agent = agent;
            payload.Brain = this;
        }

        public override void OnNetworkSpawn()
        {
            GenerateStateMachine();
        }

        void Update()
        {
            if (InputAllowed && stateMachine != null)
                stateMachine.Update(payload);
        }
        public void MoveTowardsAgentDestination()
        {
            //!-----------------------------------------------------------------
            Vector3 dir = agent.desiredVelocity;//target.position - transform.position;
            Vector3 localDir = transform.InverseTransformDirection(dir);
            Vector3 moveDir = localDir;
            moveDir = Vector3.Scale(moveDir, movementMultipliers);
            moveDir = Vector3.ClampMagnitude(moveDir, 1.0f);
            //!-----------------------------------------------------------------
            Vector3 lookDir = localDir;
            lookDir = Vector3.Scale(lookDir, lookMultipliers);
            lookDir = Vector3.ClampMagnitude(lookDir, lookMaxSpeed);
            //!-----------------------------------------------------------------
            input.Movement = moveDir;
            input.Look = lookDir;
            //!-----------------------------------------------------------------

        }
        public void MoveTowards(Vector3 pos)
        {
            agent.destination = pos;
            MoveTowardsAgentDestination();
        }
    }
}
