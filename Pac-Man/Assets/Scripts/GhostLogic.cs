using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GhostLogic : MonoBehaviour
{
    private bool scatter;
    // Start is called before the first frame update
    void Start()
    {
        Chase();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pac-Man") && !scatter)
        {
            //this is place holder, this should call the fail state (or loss state if no lives left)
            Destroy(other.gameObject);
        }
        else
        {
            //this is place holder, this should cause ghosts to die and respawn
            Destroy(this.gameObject);
        }
    }
    void Chase ()
    {
        scatter = false;
        Debug.Log("Ghosts in chase!");
        //set model to normal
        //ghost movement behaivor to chase pac man
        //pacman dies on touch
    }
    public void Scatter()
    {
        scatter = true;
        Debug.Log("Ghosts in scatter!");
        //change model to scatter
        //ghost movement behaviero to run from pac man
        //pac man kills ghosts on touch

        //Go back to chase mode after 7 seconds
        Invoke(nameof(Chase), 7f);
    }
}
