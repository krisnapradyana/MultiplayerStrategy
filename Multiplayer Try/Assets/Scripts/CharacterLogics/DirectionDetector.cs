using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDetector : MonoBehaviour {

    public bool dirAvailable;
    public bool OpponentTouch;
    [HideInInspector]
    public Vector3 dirPosition;
    public Material triggerColor;

    private void Start()
    {
        dirAvailable = false;
        OpponentTouch = false;
        triggerColor = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
         if (other.transform.tag == "Points" )
        {
            if (OpponentTouch)
            {
                dirAvailable = false;
                return;
            }
            dirAvailable = true;
            dirPosition = other.transform.position;
            triggerColor.color = Color.green;
        }
         
        

        else
            
        return;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Points" )
        {
            dirAvailable = false;
            
            dirPosition = this.transform.position; //if no object collided
            triggerColor.color = Color.yellow;
            if (OpponentTouch)
            {
                OpponentTouch = false;
               
            }
        }
        else
            return;
    }
}
