using Horror.Physics;
using UnityEngine;
using Horror.Inputs;
namespace Horror.Controllers
{
    public class JumpController : InputControllerBase
    {
        [SerializeField] PhysicsBody physicsBody;
        [SerializeField] private float jumpForce = 5f;
        

        // Update is called once per frame
        void FixedUpdate()
        {
            if (inputBrain.Input.JumpHeld)
            {
                AttemptJump();
                
            }
        }
        void AttemptJump()
        {
            if (!physicsBody.isGrounded)
            {
                return;
            }
            physicsBody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        }
    }
}
