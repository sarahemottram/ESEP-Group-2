using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace PacMan
{
    public class GhostLogic : MonoBehaviour
    {
        public bool isScattered;

        FailState failState;

        // Start is called before the first frame update
        void Start()
        {
            failState = this.gameObject.AddComponent<FailState>();
            Chase();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pac-Man") && !isScattered)
            {
                failState.Die(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Pac-Man") && isScattered)
            {
                //this is place holder, this should cause ghosts to die and respawn
                Destroy(this.gameObject);
            }
        }
        void Chase()
        {
            isScattered = false;

            if (TryGetComponent<GhostModelSwitcher>(out var switcher))
            {
                switcher.Toggle(isScattered);
            }

            //ghost movement behaivor to chase pac man
        }
        public void Scatter()
        {
            isScattered = true;
            Debug.Log("Ghosts in scatter!");

            if (TryGetComponent<GhostModelSwitcher>(out var switcher))
            {
                switcher.Toggle(isScattered);
            }

            //ghost movement behaviero to run from pac man

            Invoke(nameof(Chase), 7f); //Go back to chase mode after 7 seconds
        }
    }
}
