using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class FailState : MonoBehaviour
    {
        public event Action OnDeath;
        public event Action OnReset;

        [SerializeField]
        public GhostNavigation blinky;
        [SerializeField]
        public GhostNavigation pinky;
        [SerializeField]
        public GhostNavigation inky;
        [SerializeField]
        public GhostNavigation clyde;
        private void Start()
        {
            SoundManager.Instance.SubcribeFailState(this);
        }

        public void Die(GameObject player)
        {
            var playerInput = player.GetComponent<PlayerInput>();
            playerInput.enabled = false;//remove player control
            var virtualCamera = GameObject.Find("Virtual Camera");
            virtualCamera.SetActive(false); //turns off virtual camera
            player.transform.GetChild(0).transform.rotation = Quaternion.Euler(-90, 0, 0); //flips pac-man on his back
            var playerLife = player.GetComponent<PlayerLife>();
            playerLife.LoseLife();
            blinky.SoftReset();
            pinky.SoftReset();
            inky.SoftReset();
            clyde.SoftReset();
            StartCoroutine(Die(playerInput, virtualCamera, playerLife, player.GetComponent<PlayerReset>(), SoundManager.Instance.DeathClipLength));
        }

        private IEnumerator Die(PlayerInput playerInput, GameObject virtualCamera, PlayerLife playerLife, PlayerReset playerReset, float time)
        {
            OnDeath?.Invoke(); //play death sound
            yield return new WaitForSeconds(time); //wait for death sound
            if (playerLife.Lives <= 0)
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
            playerInput.GetComponentInParent<PelletController>().collisionCount = 0; //reset for wakas to function properly
            OnReset?.Invoke(); //plays beginning music
            yield return new WaitForSeconds(5f); //waits for intro tune
            playerInput.enabled = true; //gives player controll back
        }

        private void GameOver()
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
