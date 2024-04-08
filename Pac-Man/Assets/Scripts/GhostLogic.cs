using System;
using UnityEngine;

namespace PacMan
{
    public class GhostLogic : MonoBehaviour
    {
        public event Action OnGhostDeath;
        public bool isScattered;
        internal bool isDead;
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
                OnGhostDeath?.Invoke();
                isDead = true;
            }
        }
        internal void Chase()
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
