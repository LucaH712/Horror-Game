using UnityEngine;
using Horror.Inputs;
namespace Horror.Controllers
{
  
    public class LookController : MonoBehaviour
    {
        [SerializeField] private float maxVerticalAngle = 85;
        [SerializeField] private InputBrain inputBrain;
        [SerializeField] private Transform head;
        private float headRotation;
   
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame

        void Update()
        {
            if (inputBrain.Look.x != 0) transform.Rotate(Vector3.up, inputBrain.Look.x);

            if (inputBrain.Look.y != 0)
            {
                headRotation = Mathf.Clamp(headRotation - inputBrain.Look.y, -maxVerticalAngle, maxVerticalAngle);
                head.localEulerAngles = new Vector3(headRotation, head.localEulerAngles.y, head.localEulerAngles.z);
            }
            
                 
                
        }
    }
}
