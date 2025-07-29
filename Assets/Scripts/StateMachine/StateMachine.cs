using UnityEngine;

namespace Horror.StateMachine
{
    public class StateMachine
    {
        private State defaultState;
        public State CurrentState { get; protected set; }
        public StateMachine(State defaultState) {
            this.defaultState = defaultState;
        }
        public void Restart()
        {
            CurrentState =defaultState;
            CurrentState.EnterState();
        }
        public void Update()
        {
            State nextState = CurrentState.Update();
            if(nextState!=CurrentState) {
                CurrentState.ExitState();
                CurrentState = nextState;
                CurrentState.EnterState();
            }
        }
    }
    public class State
    {
        internal void EnterState()
        {
            
        }
        internal State Update()
        {
            return this;
        }
        internal void ExitState()
        {
            
        }
    }
}
