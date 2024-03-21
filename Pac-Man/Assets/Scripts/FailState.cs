using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class FailState : MonoBehaviour
    {

        public void Die(GameObject player)
        {
            var playerInput = player.GetComponent<PlayerInput>();
            playerInput.enabled = false;//remove player control
            var virtualCamera = GameObject.Find("Virtual Camera");
            virtualCamera.SetActive(false); //turns off virtual camera (to fix a bug)
            player.transform.GetChild(0).transform.rotation = Quaternion.Euler(-90, 0, 0); //flips pac-man on his back
            var playerLife = player.GetComponent<PlayerLife>();
            playerLife.LoseLife();
            StartCoroutine(Die(playerInput, virtualCamera, playerLife, player.GetComponent<PlayerReset>(), 3));
        }

        private IEnumerator Die(PlayerInput playerInput, GameObject virtualCamera, PlayerLife playerLife, PlayerReset playerReset, float time)
        {
            yield return new WaitForSeconds(time);
            if (playerLife.Lives <= 0)
            {
                GameOver();
                yield break;
            }
            SoftReset(playerInput, playerReset, virtualCamera);
        }

        public void SoftReset(PlayerInput playerInput, PlayerReset playerReset, GameObject virtualCamera)
        {
            //todo play sound effect
            playerReset.ResetPosition(); //resets player to beginning position
            playerInput.enabled = true; //gives player controll back
            virtualCamera.SetActive(true); //turns back on virtual camera
        }

        private void GameOver()
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
