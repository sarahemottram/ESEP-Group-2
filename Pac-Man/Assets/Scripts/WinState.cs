using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class WinState : MonoBehaviour
    {
        PelletController pelletController;
        PlayerInput playerInput;
        private float pauseTime = 3;
        // Start is called before the first frame update
        public void Start()
        {
            pelletController = GetComponent<PelletController>();
            pelletController.OnScoreChanged += ScoreChanged;
            playerInput = GetComponent<PlayerInput>();
        }

        private void ScoreChanged(int score)
        {
            if (score == 244)
            {
                //play sound
                playerInput.enabled = false; //remove player control
                StartCoroutine(LoadWinScreen());
            }
        }

        private IEnumerator LoadWinScreen()
        {
            yield return new WaitForSeconds(pauseTime);
            SceneManager.LoadScene("Win");
        }
    }
}
