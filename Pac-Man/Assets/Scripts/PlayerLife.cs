using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PacMan
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField]
        Text lifeCount;

        public int lives = 3;

        public void Update()
        {
            lifeCount.text = lives.ToString();
        }
    }
}
