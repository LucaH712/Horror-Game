using UnityEngine;
using Horror.Physics;
using Horror.Inputs;
namespace Horror.Controllers
{
    public class MovementController : InputControllerBase
    {
       
        [SerializeField] private float speed = 5f;
        [SerializeField] private PhysicsBody physics;
       
      
        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 right = transform.right * inputBrain.Input.Movement.x;
            Vector3 forward = transform.forward * inputBrain.Input.Movement.z;
            Vector3 up = transform.up * inputBrain.Input.Movement.y;
            Vector3 movement = right + up + forward;
            physics.Move(movement * Time.fixedDeltaTime * speed);
        }
        
       
    }
    
}
