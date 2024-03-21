using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        private int score;

        public GameObject inky;
        public GameObject blinky;
        public GameObject pinky;
        public GameObject clyde;

        private void Start()
        {
            UIManager.Instance.SubscribeScore(this);
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
                Score++;
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Power Pellet"))
            {
                Score++;
                inky.GetComponent<GhostLogic>().Scatter();
                blinky.GetComponent<GhostLogic>().Scatter();
                pinky.GetComponent<GhostLogic>().Scatter();
                clyde.GetComponent<GhostLogic>().Scatter();
                Destroy(other.gameObject);
            }
        }
    }
}
