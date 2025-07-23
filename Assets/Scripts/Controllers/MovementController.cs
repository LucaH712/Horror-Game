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

            physics.Move(inputBrain.Movement * Time.fixedDeltaTime * speed);
        }
        
       
    }
    
}
