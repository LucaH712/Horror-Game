using Horror.Physics;
using UnityEngine;

namespace Horror.Inputs
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] PhysicsBody physicsBody;
        [SerializeField] private float jumpForce = 5f;
    

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
