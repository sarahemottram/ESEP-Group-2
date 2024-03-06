using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class PlayerLocomotion : MonoBehaviour
    {
        public void GetMovement(Quaternion modelRotation, float cameraRotationSpeed, float horizontal, float vertical, Vector3 forward, Vector3 right, out Vector3 moveDirection, out Quaternion targetRotation)
        {
            // Relative movement
            moveDirection = (forward * vertical + right * horizontal).normalized;

            // Only rotate the player if moving left or right
            if (horizontal != 0f) //left or right
            {
                Quaternion newRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, cameraRotationSpeed * Time.deltaTime);
            }

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
                targetRotation = modelRotation;
            }
        }
    }
}