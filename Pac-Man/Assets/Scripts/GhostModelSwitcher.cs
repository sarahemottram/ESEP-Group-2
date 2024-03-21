using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class GhostModelSwitcher : MonoBehaviour
    {
        [SerializeField]
        private Transform chase;

        [SerializeField]
        private Transform scatter;
        public void Toggle(bool scattered)
        {
            chase.gameObject.SetActive(!scattered);
            scatter.gameObject.SetActive(scattered);
        }
    }
}
