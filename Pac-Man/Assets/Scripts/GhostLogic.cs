using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace PacMan
{
    public class GhostLogic : MonoBehaviour
    {
        public event Action OnGhostDeath;
        public bool isScattered;
        float scatterTimer = 7;

        FailState failState;

        void Start()
        {
            Chase();
            SoundManager.Instance.SubscribeGhostDeath(this);
        }

        private void Update()
        {
            if (isScattered)
            {
                scatterTimer -= Time.deltaTime;
                if (scatterTimer < 0 )
                {
                    Chase();
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pac-Man") && !isScattered)
            {
                failState = other.GetComponent<FailState>();
                failState.Die(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Pac-Man") && isScattered)
            {
                //this is place holder, this should cause ghosts to die and respawn
                OnGhostDeath?.Invoke();
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
            scatterTimer = 7;

            if (TryGetComponent<GhostModelSwitcher>(out var switcher))
            {
                switcher.Toggle(isScattered);
            }

            //ghost movement behavier to run from pac man
        }
    }
}
