using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 8f;
    private float gravity = -6.81f;
    private float jumpHeight = 0.4f;
    private Vector3 velocity;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        Vector3 move = transform.right * x + transform.forward * z + transform.up * velocity.y;
        controller.Move(move * speed * Time.deltaTime);
    }
}
