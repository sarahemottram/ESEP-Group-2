using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class WinState : MonoBehaviour
    {
        SoundManager soundManager;
        PelletController pelletController;
        PlayerInput playerInput;

        // Start is called before the first frame update
        public void Start()
        {
            soundManager = GetComponent<SoundManager>();
            pelletController = GetComponent<PelletController>();
            pelletController.OnScoreChanged += ScoreChanged;
            playerInput = GetComponent<PlayerInput>();
        }

        private void ScoreChanged(int score)
        {
            if (score == 244)
            {
                soundManager.PlayEnd();
                playerInput.enabled = false; //remove player control
                StartCoroutine(LoadWinScreen());
            }
        }

        private IEnumerator LoadWinScreen()
        {
            yield return new WaitForSeconds(soundManager.end.clip.length);
            SceneManager.LoadScene("Win");
        }
    }
}
