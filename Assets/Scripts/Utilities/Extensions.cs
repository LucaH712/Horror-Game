using UnityEngine;
using Unity.Netcode;
namespace Horror.Utilities
{
    public static class Extensions
    {
        public static void DrawGroundContactGizmos(this CharacterController controller)
        {
            //* Draws a sphere at the ground contact point to visualize if the character is grounded.
            float radius = controller.radius - controller.skinWidth;
            Vector3 position = controller.transform.position + Vector3.down * (controller.height * .5f - controller.radius + controller.skinWidth * 2f);
            Gizmos.color = controller.isGrounded ? Color.red : Color.green;
            Gizmos.DrawWireSphere(position, radius);
        }
        public static bool CanControl(this NetworkBehaviour networkBehaviour)
        {
            return networkBehaviour.gameObject.activeInHierarchy && networkBehaviour.IsSpawned && networkBehaviour.IsOwner;
        }
    }
}
