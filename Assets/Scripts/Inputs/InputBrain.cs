using Horror.Utilities;
using UnityEngine;
using Unity.Netcode;
namespace Horror.Inputs
{
    public abstract class InputBrain : NetworkBehaviour
    {
        public bool AllowInput = true;
        public bool InputAllowed => AllowInput && this.CanControl();
        protected abstract InputValues InternalInput { get; }
        public InputValues Input => InputAllowed && InternalInput!=null ? InternalInput : InputValues.Empty;
        
        
            
        
    }
   [System.Serializable] public class InputValues
    {
        public Vector3 Movement;
        public bool JumpHeld;
        public Vector2 Look;
        public static readonly InputValues Empty = new InputValues();
    }
    
     
}
