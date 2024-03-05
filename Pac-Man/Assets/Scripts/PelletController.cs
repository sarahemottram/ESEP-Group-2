using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    GameObject inky;
    GameObject blinky;
    GameObject pinky;
    GameObject clyde;

    private void Start()
    {
        inky = GameObject.FindGameObjectWithTag("Inky");
        blinky = GameObject.FindGameObjectWithTag("Blinky");
        pinky = GameObject.FindGameObjectWithTag("Pinky");
        clyde = GameObject.FindGameObjectWithTag("Clyde");
    }

    public int score = 0;
    private void OnTriggerEnter(Collider other)
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
