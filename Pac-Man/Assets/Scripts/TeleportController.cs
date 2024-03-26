using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PacMan
{
    public class TeleportController : MonoBehaviour
    {
        Vector3 tele1Location;
        Vector3 tele2Location;
        Vector3 tele1Teleport;
        Vector3 tele2Teleport;
        private void Start()
        {
            tele1Location = GameObject.Find("TelePortal S1").transform.position;
            tele1Teleport = tele1Location;
            tele1Teleport.x+=2; //adjust to avoid infinite teleportation
            tele2Location = GameObject.Find("TelePortal S2").transform.position;
            tele2Teleport = tele2Location;
            tele2Teleport.x-=2; //adjust to avoid infinite teleportation
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Pac-Man"))
                other.gameObject.GetComponent<CharacterController>().enabled = false;

            if(tele1Location == this.transform.position)
            {
                other.gameObject.transform.position = tele2Teleport;
            }
            else
            {
                other.gameObject.transform.position = tele1Teleport;
            }

            if (other.gameObject.CompareTag("Pac-Man"))
                other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
