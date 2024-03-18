using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class FailState : MonoBehaviour
    {
        GameObject player;
        GameObject virtualCamera;
        PlayerController playerController;
        private bool dead;
        private float deadTimer = 0;
        private float pauseTime = 3;

        public void Start()
        {
            player = GameObject.FindGameObjectWithTag("Pac-Man");
            virtualCamera = GameObject.Find("Virtual Camera");
            playerController = player.GetComponent<PlayerController>();
        }

        public void Update() 
        {
            if (dead && player.GetComponent<PlayerLife>().lives > 0)
            {
                deadTimer += Time.deltaTime;
                if (deadTimer >= pauseTime)
                {
                    dead = false;
                    SoftReset();
                }
            }
            else if (dead && player.GetComponent<PlayerLife>().lives <= 0)
            {
                deadTimer += Time.deltaTime;
                if (deadTimer >= pauseTime)
                    GameOver();
            }
        }
        public void Die()
        {
            var modelTransform = player.transform.Find("Pac-Model");
            var x = -90;

            playerController.enabled = false; //remove player control
            virtualCamera.SetActive(false); //turns off virtual camera (to fix a bug)
            modelTransform.rotation = Quaternion.Euler(x, 0, 0); //flips pac-man on his back

            dead = true; //this triggers the update loop to pause and reset
        }

        private void SoftReset()
        {
            //todo play sound effect
            playerController.ResetPosition(); //resets player to beginning position
            player.GetComponent<PlayerLife>().lives--; //removes life
            playerController.enabled = true; //gives player controll back
            deadTimer = 0;
            virtualCamera.gameObject.SetActive(true); //turns back on virtual camera
        }

            private void GameOver()
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
