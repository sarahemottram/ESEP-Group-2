using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PacMan
{
    public class PelletController : MonoBehaviour
    {
        public int score;
        [SerializeField]
        Text scoreCount;

        public GameObject inky;
        public GameObject blinky;
        public GameObject pinky;
        public GameObject clyde;

        private void Start()
        {
            InitializeGhosts();
        }

        private void Update()
        {
            scoreCount.text = score.ToString();
        }

        public void InitializeGhosts()
        {
            score = 0;
            inky = GameObject.FindGameObjectWithTag("Inky");
            blinky = GameObject.FindGameObjectWithTag("Blinky");
            pinky = GameObject.FindGameObjectWithTag("Pinky");
            clyde = GameObject.FindGameObjectWithTag("Clyde");
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Pellet"))
            {
                score++;
                Destroy(other.gameObject);
            }
            else if(other.gameObject.CompareTag("Power Pellet"))
            {
                score++;
                inky.GetComponent<GhostLogic>().Scatter();
                blinky.GetComponent<GhostLogic>().Scatter();
                pinky.GetComponent<GhostLogic>().Scatter();
                clyde.GetComponent<GhostLogic>().Scatter();
                Destroy(other.gameObject);
            }
        }
    }
}
