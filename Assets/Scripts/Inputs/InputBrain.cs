using PlasticGui.WorkspaceWindow;
using UnityEngine;

namespace Horror.Inputs
{
    public abstract  class  InputBrain : MonoBehaviour
    {
        public abstract Vector3 Movement { get;}
        public abstract bool JumpHeld { get; }
        public abstract Vector2 Look {get; }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    }
}
