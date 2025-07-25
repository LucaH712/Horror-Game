using PlasticGui.WorkspaceWindow;
using UnityEngine;
using Unity.Netcode;
namespace Horror.Inputs
{
    public abstract class InputBrain : NetworkBehaviour
    {
        public bool AllowInput = true;
        public bool InputAllowed => AllowInput && IsOwner && IsSpawned;
        protected abstract InputValues InternalInput { get; }
        public InputValues Input => InputAllowed ? InternalInput : InputValues.Empty;
        
            
        
    }
    public struct InputValues
    {
        public Vector3 Movement;
        public bool JumpHeld;
        public Vector2 Look;
        public static readonly InputValues Empty = default(InputValues);
    }
    
     
}
