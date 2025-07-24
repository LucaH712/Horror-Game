using UnityEngine;
using Horror.Physics;
using Horror.Inputs;
namespace Horror.Controllers
{
    public class MovementController : MonoBehaviour
    {
       
        [SerializeField] private float speed = 5f;
        [SerializeField] private PhysicsBody physics;
        [SerializeField] private InputBrain inputBrain;
      
        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 right = transform.right * inputBrain.Movement.x;
            Vector3 forward = transform.forward * inputBrain.Movement.z;
            Vector3 up = transform.up * inputBrain.Movement.y;
            Vector3 movement = right + up + forward;
            physics.Move(movement * Time.fixedDeltaTime * speed);
        }
        
       
    }
    
}
