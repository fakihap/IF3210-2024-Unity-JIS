using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    private float originalSpeed;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake() {
        floorMask = LayerMask.GetMask("Environment");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        originalSpeed = speed; // menyimpan nilai awal kecepatan
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v) {
        movement = transform.right * h + transform.forward * v;
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v) {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
    public void OrbIncreaseSpeed(float duration, float multiplier)
        {
            StartCoroutine(IncreaseSpeedForDuration(duration, multiplier));
        }

    IEnumerator IncreaseSpeedForDuration(float duration, float multiplier)
    {
        speed *= multiplier; // meningkatkan kecepatan sesuai multiplier
        yield return new WaitForSeconds(duration);
        speed = originalSpeed; // mengembalikan kecepatan ke nilai semula setelah durasi selesai
    }
}
