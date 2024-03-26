using System;
using System.Collections;
using UnityEngine;

namespace PacMan
{
    public class PelletController : MonoBehaviour
    {
        public event Action<int> OnScoreChanged;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnScoreChanged?.Invoke(score);
            }
        }
        SoundManager soundManager;

        private int score;
        private int collisionCount = 0; //this exists to monitor if pac-man is touching any pellets

        public GameObject inky;
        public GameObject blinky;
        public GameObject pinky;
        public GameObject clyde;

        private void Start()
        {
            UIManager.Instance.SubscribeScore(this);
            soundManager = GetComponent<SoundManager>();
            InitializeGhosts();
        }

        public void InitializeGhosts()
        {
            Score = 0;
            inky = GameObject.FindGameObjectWithTag("Inky");
            blinky = GameObject.FindGameObjectWithTag("Blinky");
            pinky = GameObject.FindGameObjectWithTag("Pinky");
            clyde = GameObject.FindGameObjectWithTag("Clyde");
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pellet"))
            {
                soundManager.PlayWaka();
                Score++;
                collisionCount++; 
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (other.gameObject.CompareTag("Power Pellet"))
            {
                soundManager.PlaySiren();
                soundManager.PauseWaka();
                Score++;
                inky.GetComponent<GhostLogic>().Scatter();
                blinky.GetComponent<GhostLogic>().Scatter();
                pinky.GetComponent<GhostLogic>().Scatter();
                clyde.GetComponent<GhostLogic>().Scatter();
                collisionCount++;
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Pellet") || other.gameObject.CompareTag("Power Pellet"))
            {
                collisionCount--;
                Destroy(other.gameObject);
                if (collisionCount == 0) //pacman is touching no pellets
                    soundManager.PauseWaka();
            }
        }
    }
}
