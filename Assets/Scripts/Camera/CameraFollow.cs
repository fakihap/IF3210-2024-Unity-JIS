using System;
using UnityEngine;

namespace Nightmare
{
    public class CameraFollow : MonoBehaviour
    {
        // for following target
        public Transform target;            // The position that the camera will be following.
        public float distance = 3f;         // The distance between the camera and the target.
        public float height = 1.5f;           // The height offset of the camera from the target.
        public float shoulderOffset = 2f;   // The right shoulder offset of the camera from the target.
        public float damping = 10f;          // The smoothness of camera movement.
        public float rotationDamping = 10f; // The smoothness of camera rotation.

        // for moving the camera around 
        public float mouseSensitivity = 120f;
        public float topClamp = -60f;
        public float bottomClamp = 15f;
                public float smoothing = 5f;        // The speed with which the camera will be following.
        Vector3 offset;                     // The initial offset from the target.


        float xRotation = 0f;
        float yRotation = 0f;


        private void Start()
        {
            // Cursor.lockState = CursorLockMode.Locked;
            offset = transform.position - target.position;
        }

        private void FixedUpdate()
        {
            
            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);            
            // // Calculate the desired position and rotation of the camera.
            // Vector3 desiredPosition = target.position + target.up * height - target.forward * distance + target.right * shoulderOffset;
            // transform.position = Vector3.Lerp(transform.position, desiredPosition, damping * Time.deltaTime);

            // // Calculate the direction from the camera to the mouse cursor.
            // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            // float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // // Rotation around the x axis (Look up and down)
            // xRotation -= mouseY;

            // xRotation = Math.Clamp(xRotation, topClamp, bottomClamp);
            // yRotation += mouseX;

            // transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}