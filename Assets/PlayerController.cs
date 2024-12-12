using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2) // Left side of screen for movement
            {
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");

                Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

                rb.velocity = movementDirection * moveSpeed;
            }
            else // Right side of screen for rotation
            {
                float horizontalRotationInput = Input.GetAxisRaw("Horizontal");

                transform.eulerAngles += new Vector3(0f, horizontalRotationInput * rotationSpeed, 0f);
            }
        }
    }
}
