using Horror.Inputs;
using Horror.StateMachine;
using UnityEngine;

namespace Horror.StateMachine
{
    public class BasicGhostStateMachine : InputBrain
    {
        [SerializeField] private float radius = 5.0f;
        public InputValues inputValues= new InputValues();
        void Start()
        {
            stateMachine.Restart(GetPayload());
        }
        void Update()
        {
            stateMachine.Update(GetPayload());
        }
        Payload GetPayload()
        {
            Payload payload = new Payload();
            payload.Self = this.transform;
            payload.Target = FindTarget();
            payload.Radius = radius;
            payload.input = inputValues;
            return payload;
        }
        Transform FindTarget()
        {
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            float closestPlayerDistance = Mathf.Infinity;
            Transform closestPlayer = null;
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestPlayerDistance)
                {
                    closestPlayerDistance = distance;
                    closestPlayer = player.transform;
                }
            }
            return closestPlayer;
        }
        private StateMachine<Payload> stateMachine = new StateMachine<Payload>(new Idle());

        protected override InputValues InternalInput => inputValues;
    }


 #region States
    public class Jump : IState<Payload>
    {

        public void EnterState(Payload payload) { }

        public void ExitState(Payload payload) { }
     

        public IState<Payload> Update(Payload payload)
        {
              payload.input.JumpHeld = true;
            if (payload.Target == null) return new Idle();
            if (Vector3.Distance(payload.Target.position, payload.Self.position) >= payload.Radius)
            {
                return new Idle();
            }
          
            return this;
        }
    }
    public class Idle : IState<Payload>
    {
        public void EnterState(Payload payload) { }
        public void ExitState(Payload payload) { }

        public IState<Payload> Update(Payload payload)
        {
             payload.input.JumpHeld = false;
            if (payload.Target == null) return this;
            if (Vector3.Distance(payload.Target.position, payload.Self.position) < payload.Radius)
            {
                return new Jump();
            }
          
            return this;
        }
    }
    #endregion
    public struct Payload
    {
        public Transform Self, Target;
        public float Radius;
        public InputValues input;
    }
    
}
