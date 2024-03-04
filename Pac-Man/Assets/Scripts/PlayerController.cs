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
    float cameraRotationSpeed;

    [SerializeField]
    float modelRotationSpeed;

    private CharacterController characterController;
    private Transform modelTransform;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        modelTransform = transform.Find("Pac-Model");
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

        forward.y = 0f; //Vector is horizontal
        right.y = 0f; //Vector is horizontal

        // Relative movement
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // Only rotate the player if moving left or right
        if (horizontal != 0f) //left or right
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, cameraRotationSpeed * Time.deltaTime);
        }

        //Pac-Man faces direction of travel
        Quaternion targetRotation;

        if (moveDirection != Vector3.zero) // Moving Forwards
        {
            targetRotation = Quaternion.LookRotation(moveDirection);
        }
        else if (vertical < 0f) // Moving backward
        {
            targetRotation = Quaternion.LookRotation(-forward);
        }
        else // No movement
        {
            targetRotation = modelTransform.rotation;
        }

        modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, modelRotationSpeed * Time.deltaTime);

        // Move the player
        characterController.Move(moveDirection * Time.deltaTime * speed);
    }
}
