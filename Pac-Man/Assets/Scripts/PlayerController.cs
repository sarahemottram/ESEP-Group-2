using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PacMan
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerLocomotion locomotion;
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
            locomotion = GetComponent<PlayerLocomotion>();
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

            locomotion.GetMovement(modelTransform.rotation, cameraRotationSpeed, horizontal, vertical, forward, right, out Vector3 moveDirection, out Quaternion targetRotation);

            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, modelRotationSpeed * Time.deltaTime);

            // Move the player
            characterController.Move(speed * Time.deltaTime * moveDirection);
        }
    }
}