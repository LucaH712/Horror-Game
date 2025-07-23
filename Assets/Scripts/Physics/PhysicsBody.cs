
using UnityEngine;
using Horror.Utilities;

namespace Horror.Physics
{
    public class PhysicsBody : MonoBehaviour
    {
        /*
            Gravity,
            Velocity,
            *DOESNT HANDLE INPUT,JUST PHSYCIS*
        */
        [SerializeField] private float gravity = 1.0f;
        [SerializeField] private float drag = 0.1f;
        private Vector3 Velocity;
        [SerializeField] private CharacterController controller;
        private Vector3 moveBuffer;
        private Vector3 externalForces;
        public bool isGrounded => controller.isGrounded;
        void FixedUpdate()
        {

            Velocity = ApplyGravity(Velocity);
            Velocity = ApplyForce(Velocity);
            Velocity = ApplyCollision(Velocity);
            moveBuffer = SetSpeed(moveBuffer);

            controller.Move(moveBuffer);
            moveBuffer = Vector3.zero;
        }
        Vector3 ApplyGravity(Vector3 velocity)
        {
            velocity += UnityEngine.Physics.gravity * gravity * Time.fixedDeltaTime;
            return velocity;
        }
        Vector3 SetSpeed(Vector3 moveStep)
        {
            moveStep += Velocity * Time.fixedDeltaTime;
            Velocity *= 1f - drag * Time.fixedDeltaTime;
            return moveStep;
        }
        Vector3 ApplyCollision(Vector3 velocity)
        {
            if (controller.isGrounded)
            {
                velocity.y = Mathf.Max(-controller.skinWidth,velocity.y);

            }
            return velocity;
        }
        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            if (forceMode == ForceMode.Force || forceMode == ForceMode.Acceleration)
            {
                force *= Time.fixedDeltaTime;
            }

            externalForces += force;

        }
        private Vector3 ApplyForce(Vector3 velocity)
        {
            velocity += externalForces;
            externalForces = Vector3.zero;
            return velocity;
        }
        public void Move(Vector3 moveStep)
        {
            moveBuffer += moveStep;
            
        }
        void OnDrawGizmosSelected()
        {
            controller.DrawGroundContactGizmos();
        }
    }
}
