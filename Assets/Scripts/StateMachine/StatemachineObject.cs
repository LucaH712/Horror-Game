using UnityEngine;

namespace Horror.StateMachine
{

    public abstract class StatemachineObject<T> : ScriptableObject
    {
        public abstract StateMachine<T> InstantiateStateMachine();
        // public void RestartStateMachine(T payload)
        // {
        //     stateMachine.Restart(payload);
        // }
        // public void UpdateStateMachine(T payload)
        // {
        //     stateMachine.Update(payload);
        // }
    }
    
}
