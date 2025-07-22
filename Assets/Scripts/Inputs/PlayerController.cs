using UnityEngine;
namespace Horror.Inputs{
    public class PlayerController : MonoBehaviour
    { 
        private Vector3 inputBuffer;
        [SerializeField] private float speed = 5f;
        [SerializeField] private CharacterController controller;
        // Update is called once per frame
        void FixedUpdate()
        {
            
            controller.Move(inputBuffer*Time.fixedDeltaTime*speed);
        }
        void Update(){
           inputBuffer=GetInput();
        }
          Vector3 GetInput (){
            Vector2 rawInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 input = Vector2.ClampMagnitude(rawInput, 1f);
            return new Vector3(input.x, 0.0f, input.y); 
        }
    }
}
