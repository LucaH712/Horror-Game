using UnityEngine;

namespace Horror.Inputs
{
    public class PlayerBrain : InputBrain
    {
        private InputValues input=new InputValues();
        protected override InputValues InternalInput => input;
        [SerializeField] private float lookSensitvity=1.0f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created

        // Update is called once per frame
        void OnEnable(){
            Cursor.lockState = CursorLockMode.Locked;
        }
        void OnDisable(){
            Cursor.lockState = CursorLockMode.None;
        }
      
        void Update()
        {
            input.Movement = GetInput();
            input.JumpHeld = UnityEngine.Input.GetButton("Jump");
            input.Look = GetLook();
        }
        Vector3 GetInput()
        {
    Vector2 rawInput = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
            Vector2 input = Vector2.ClampMagnitude(rawInput, 1f);
            return new Vector3(input.x, 0.0f, input.y);
        }
        private Vector2 GetLook()
        {
            return new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"))*lookSensitvity;
        }
    }
}
