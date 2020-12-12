using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MouseDirection : MonoBehaviour
{
    private float mouseSens = 4f; 
    private float xRotation = 0f;
    public Transform playerBody, pistol;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        float mouseX = Input.GetAxis("Mouse X") * mouseSens; // rotate left and right, around Y axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens; // rotate up and down, around X axis
        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamps the direction of rotation so you don't over rotate
        // assigns the rotation transformation calculated // calculate the xRotation degrees around the x axis (look up and down)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // transformer, representer, calculator
        playerBody.Rotate(Vector3.up * mouseX); // up means rotate around Y axis
        pistol.Rotate(Vector3.up * mouseX);
    }
}
