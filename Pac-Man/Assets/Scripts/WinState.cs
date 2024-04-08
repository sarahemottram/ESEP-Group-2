using System;
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
        FailState playerFail;
        public event Action OnWin;

        // Start is called before the first frame update
        public void Start()
        {
            SoundManager.Instance.SubscribeWin(this);
            pelletController = GetComponent<PelletController>();
            pelletController.OnScoreChanged += ScoreChanged;
            playerInput = GetComponent<PlayerInput>();
            playerFail = GetComponent<FailState>();
        }

        private void ScoreChanged(int score)
        {
            if (score == 244)
            {
                OnWin?.Invoke();
                playerInput.enabled = false; //remove player control
                playerFail.enabled = false;
                StartCoroutine(LoadWinScreen());
            }
        }

        private IEnumerator LoadWinScreen()
        {
            yield return new WaitForSeconds(SoundManager.Instance.EndClipLength);
            SceneManager.LoadScene("Win");
        }
    }
}
