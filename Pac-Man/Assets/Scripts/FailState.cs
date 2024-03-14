using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class FailState : MonoBehaviour
    {
        GameObject player = GameObject.FindGameObjectWithTag("Pac-Man");

        public void Die()
        {
            var lives = player.GetComponent<PlayerLife>().lives;
            if (lives > 0)
            {
                var modelTransform = player.transform.Find("Pac-Model");
                var x = -90;

                player.gameObject.GetComponent<PlayerController>().enabled = false; //remove player controll
                modelTransform.rotation = Quaternion.Euler(x, 0, 0); //flips pac-man on his back
                player.GetComponent<PlayerLife>().lives--; //removes life

                //todo: reset level keeping collected dots and score
                //todo: play sound effect and wait for sound effect
            }
            //todo: reset full level
        }
    }
}
