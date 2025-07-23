using UnityEngine;

namespace Horror.Inputs
{
    public class PlayerBrain : InputBrain
    {
        private Vector3 movement;
        public override Vector3 Movement => movement;
        private bool jumpHeld;
        public override bool JumpHeld => jumpHeld;
        private Vector2 look;
        public override Vector2 Look => look;
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
            movement = GetInput();
            jumpHeld = Input.GetButton("Jump");
            look = GetLook();
        }
        Vector3 GetInput()
        {
            Vector2 rawInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 input = Vector2.ClampMagnitude(rawInput, 1f);
            return new Vector3(input.x, 0.0f, input.y);
        }
        private Vector2 GetLook()
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*lookSensitvity;
        }
    }
}
