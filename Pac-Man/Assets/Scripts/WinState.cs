using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class WinState : MonoBehaviour
    {
        PelletController pelletController;
        PlayerController player;
        private float winTimer = 0;
        private float pauseTime = 3;
        // Start is called before the first frame update
        void Start()
        {
            pelletController = this.gameObject.GetComponent<PelletController>();
            player = this.gameObject.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (pelletController.score == 244)
            {
                //play sound
                player.gameObject.GetComponent<PlayerController>().enabled = false; //remove player control
                winTimer += Time.deltaTime;
                if (winTimer >= pauseTime)
                {
                    SceneManager.LoadScene("Win");
                }
            }
        }  
    }
}
