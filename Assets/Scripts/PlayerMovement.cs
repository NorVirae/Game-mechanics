using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 700f;
    private CharacterController characterController;
    public float jumpHeight = 10f;
    public float jumpGracePeriod = 3f;

    float ySpeed = 0f;

    private float lastGroundTime;

    private float lastButtonPressedTime;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if(characterController.isGrounded)
        {
            lastGroundTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            lastButtonPressedTime = Time.time;
        }

            if (Time.time - lastGroundTime <= jumpGracePeriod)
        {
            ySpeed = -0.5f;
            if (Time.time - lastButtonPressedTime <= jumpGracePeriod)
            {
                Debug.Log("Jump was Pressed " + Physics.gravity.y);
                ySpeed = jumpHeight;
                lastGroundTime = 0f;
            }
        }
        Vector3 moveMagnitude = movementDirection * Mathf.Clamp01(movementDirection.magnitude) * speed;
        moveMagnitude.y = ySpeed;
        characterController.Move(moveMagnitude * Time.deltaTime);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
