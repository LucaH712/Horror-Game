using UnityEngine;
using Horror.Inputs;
namespace Horror.Controllers
{
  
    public class LookController : InputControllerBase
    {
        [SerializeField] private float maxVerticalAngle = 85;
       
        [SerializeField] private Transform head;
        private float headRotation;
   
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame

        void Update()
        {
            if (inputBrain.Input.Look.x != 0) transform.Rotate(Vector3.up, inputBrain.Input.Look.x);

            if (inputBrain.Input.Look.y != 0)
            {
                headRotation = Mathf.Clamp(headRotation - inputBrain.Input.Look.y, -maxVerticalAngle, maxVerticalAngle);
                head.localEulerAngles = new Vector3(headRotation, head.localEulerAngles.y, head.localEulerAngles.z);
            }
            
                 
                
        }
    }
}
