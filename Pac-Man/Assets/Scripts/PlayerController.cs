using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationSpeed;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CameraRelativeMovement();
    }  

    private void CameraRelativeMovement()
    {
        // Inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Camera direction
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f; // Ensure the vector is horizontal
        right.y = 0f; // Ensure the vector is horizontal

        // Relative movement
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // Only rotate the player if moving left or right
        if (horizontal != 0f) // Moving left or right
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the player
        characterController.Move(moveDirection * Time.deltaTime * speed);
    }
}
