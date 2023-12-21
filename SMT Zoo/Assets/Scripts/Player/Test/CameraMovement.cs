using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

// Old crappy movement script for testing
public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1, moveSpeed = 0.5f;

    float xAxisClamp;
    int forward, right, up;

    // Start is called before the first frame update
    void Start()
    {
        //removes cursos from screen when in-game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            forward = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            right = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            right = -1;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            up = 1;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            up = -1;
        }

        RotateCamera();
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 forwardDir = transform.rotation * Vector3.forward;
        forwardDir.y = 0;
        forwardDir = forwardDir.normalized;
        Vector3 rightDir = transform.rotation * Vector3.right;

        Vector3 velocity = (forwardDir * forward + rightDir * right + Vector3.up * up) * moveSpeed * Time.deltaTime;

        transform.position += velocity;

        //Resets input int's after movement
        forward = 0;
        right = 0;
        up = 0;
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector2 rotAmount = new Vector2(mouseX, mouseY) * mouseSensitivity ;

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y += rotAmount.x;
        rot.x -= rotAmount.y;

        //Saves the x axis rotation in seperate float
        xAxisClamp -= rotAmount.y;

        //Clamps x axis rotation
        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            rot.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rot.x = 270;
        }

        //Applies new rotation
        transform.rotation = Quaternion.Euler(rot);
    }
}
