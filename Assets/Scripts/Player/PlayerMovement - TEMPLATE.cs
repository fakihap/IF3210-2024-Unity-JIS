using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class PlayerMovement : PausibleObject
    {
        public float speed = 6f;

        Vector3 movement;
        Animator anim;
        Rigidbody playerRigidBody;
        int floorMask;
        readonly float camRayLength = 100f;

        private void Awake()
        {
            floorMask = LayerMask.GetMask("Environment");
            anim = GetComponent<Animator>();
            playerRigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Move(h, v);
            Turning();
            Animating(h, v);
        }

        void Move(float h, float v)
        {
            movement = transform.right * h + transform.forward * v;

            movement = movement.normalized * speed * Time.deltaTime;

            playerRigidBody.MovePosition(transform.position + movement);
        }

        void Turning()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out RaycastHit floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidBody.MoveRotation(newRotation);
            }
        }

        void Animating(float h, float v)
        {
            bool walking = h != 0f || v != 0f;
            anim.SetBool("IsWalking", walking);
        }

    }
}