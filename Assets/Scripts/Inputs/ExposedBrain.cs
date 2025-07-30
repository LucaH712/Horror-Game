using UnityEngine;

namespace Horror.Inputs
{
    public class ExposedBrain : InputBrain
    {
        public InputValues input=new InputValues();
        protected override InputValues InternalInput => input;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        
    }
}
