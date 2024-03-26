using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class FailState : MonoBehaviour
    {
        SoundManager soundManager;

        private void Start()
        {
            soundManager = GetComponent<SoundManager>();
        }

        public void Die(GameObject player)
        {
            var playerInput = player.GetComponent<PlayerInput>();
            playerInput.enabled = false;//remove player control
            var virtualCamera = GameObject.Find("Virtual Camera");
            virtualCamera.SetActive(false); //turns off virtual camer
            player.transform.GetChild(0).transform.rotation = Quaternion.Euler(-90, 0, 0); //flips pac-man on his back
            var playerLife = player.GetComponent<PlayerLife>();
            playerLife.LoseLife();
            StartCoroutine(Die(playerInput, virtualCamera, playerLife, player.GetComponent<PlayerReset>(), soundManager.death.clip.length));
        }

        private IEnumerator Die(PlayerInput playerInput, GameObject virtualCamera, PlayerLife playerLife, PlayerReset playerReset, float time)
        {
            soundManager.PlayDeath(); //play death sound
            yield return new WaitForSeconds(time); //wait for death sound
            if (playerLife.Lives < 0)
            {
                GameOver();
                yield break;
            }
            StartCoroutine(SoftReset(playerInput, playerReset, virtualCamera));
        }

        public IEnumerator SoftReset(PlayerInput playerInput, PlayerReset playerReset, GameObject virtualCamera)
        {
            playerReset.ResetPosition(); //resets player to beginning position
            virtualCamera.SetActive(true); //turns back on virtual camera
            soundManager.PlayLoop(); //plays beginning music
            yield return new WaitForSeconds(5f); //waits for intro tune
            playerInput.enabled = true; //gives player controll back
        }

        private void GameOver()
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
