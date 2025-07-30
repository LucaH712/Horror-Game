using UnityEngine;

namespace Horror.StateMachine
{
    public interface IState<T>
    {
        void EnterState(T payload);
        void ExitState(T payload);
        IState<T> Update(T payload);
        
            
     }
       
 }

