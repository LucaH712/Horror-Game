
using UnityEngine;

namespace Horror.Inputs
{
    public class SimpleChaseBrain : InputBrain
    {
        private InputValues input;
        protected override InputValues InternalInput => input;
        [SerializeField] private Vector3 movementMultipliers = new Vector3(1.0f, 0.0f, 1.0f);
        [SerializeField] private Vector3 lookMultipliers = new Vector3(1.0f, 0.50f, 1.0f);
        [SerializeField]private float lookMaxSpeed = 100.0f;
        [SerializeField] private bool canJump = true;
        [SerializeField] private float jumpHeightThreshold = 0.6f;

        // Update is called once per frame
        void Update()
        {
            /* try to find the target(player)
                if a target is found,move towards it
                if not, provide no input */
            Transform target = FindTarget();
            if (!target) { input = InputValues.Empty; return; }
            MoveTowards(target);
            AttemptJump(target);      
              }
        Transform FindTarget()
        {
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            float closestPlayerDistance = Mathf.Infinity;
            Transform closestPlayer = null;
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestPlayerDistance)
                {
                    closestPlayerDistance = distance;
                    closestPlayer = player.transform;
                }
            }
            return closestPlayer;
        }
        void MoveTowards(Transform target)
        {
            //!-----------------------------------------------------------------
            Vector3 dir = target.position - transform.position;
            Vector3 localDir = transform.InverseTransformDirection(dir);
            Vector3 moveDir = localDir;
            moveDir = Vector3.Scale(moveDir, movementMultipliers);
            moveDir = Vector3.ClampMagnitude(moveDir, 1.0f);
            //!-----------------------------------------------------------------
            Vector3 lookDir = localDir;
            lookDir = Vector3.Scale(lookDir, lookMultipliers);
            lookDir = Vector3.ClampMagnitude(lookDir, lookMaxSpeed);
            //!-----------------------------------------------------------------
            input.Movement = moveDir;
            input.Look = lookDir;
            //!-----------------------------------------------------------------
        
        }
        void AttemptJump(Transform target)
        {
            input.JumpHeld = canJump && target.position.y - transform.position.y > jumpHeightThreshold;
        }
    }
}
