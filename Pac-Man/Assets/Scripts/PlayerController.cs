using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PacMan
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerLocomotion locomotion;
        private PlayerInput input;
        private PlayerLife life;
        [SerializeField]
        float speed;

        [SerializeField]
        float cameraRotationSpeed;

        [SerializeField]
        float modelRotationSpeed;

        private CharacterController characterController;
        private Transform modelTransform;
        private new Camera camera;

        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            locomotion = GetComponent<PlayerLocomotion>();
            input = GetComponent<PlayerInput>();
            modelTransform = transform.Find("Pac-Model");
            GetComponent<PlayerReset>().SetStartLocation(transform.position);
            life = GetComponent<PlayerLife>();
            camera = Camera.main;
        }

        private void Update()
        {
            if (!characterController.enabled)
                return;
            CameraRelativeMovement();
        }

        private void CameraRelativeMovement()
        {
            // Inputs
            float horizontal = input.Horizontal;
            float vertical = input.Vertical;

            // Camera direction
            Vector3 forward = camera.transform.forward;
            Vector3 right = camera.transform.right;

            forward.y = 0f; //Vector is horizontal
            right.y = 0f; //Vector is horizontal

            locomotion.GetMovement(modelTransform.rotation, cameraRotationSpeed, horizontal, vertical, forward, right, out Vector3 moveDirection, out Quaternion targetRotation);

            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, modelRotationSpeed * Time.deltaTime);

            // Move the player
            characterController.Move(speed * Time.deltaTime * moveDirection);
        }
    }
}