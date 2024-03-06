using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace PacMan
{
    public class GhostLogic : MonoBehaviour
    {
        public bool isScattered;

        // Start is called before the first frame update
        void Start()
        {
            Chase();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pac-Man") && !isScattered)
            {
                //this is place holder, this should call the fail state (or loss state if no lives left)
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Pac-Man") && isScattered)
            {
                //this is place holder, this should cause ghosts to die and respawn
                Destroy(this.gameObject);
            }
        }
        void Chase ()
        {
            isScattered = false;
            Debug.Log("Ghosts in chase!");
            //set model to normal
            //ghost movement behaivor to chase pac man
            //pacman dies on touch
        }
        public void Scatter()
        {
            isScattered = true;
            Debug.Log("Ghosts in scatter!");
            //change model to scatter
            //ghost movement behaviero to run from pac man
            //pac man kills ghosts on touch

            //Go back to chase mode after 7 seconds
            Invoke(nameof(Chase), 7f);
        }
    }
}
