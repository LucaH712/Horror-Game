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
            return new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        }
    }
}
