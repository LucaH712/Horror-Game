using Horror.Physics;
using UnityEngine;
using Horror.Inputs;
namespace Horror.Controllers
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] PhysicsBody physicsBody;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private InputBrain inputBrain;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (inputBrain.JumpHeld)
            {
                AttemptJump();
                
            }
        }
        void AttemptJump()
        {
            if (!physicsBody.isGrounded)
            {
                Debug.Log("Not Grounded");
                return;
            }
            Debug.Log("Should Jump");
            physicsBody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        }
    }
}
