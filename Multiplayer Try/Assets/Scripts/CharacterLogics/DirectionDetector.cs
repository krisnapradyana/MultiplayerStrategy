using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDetector : MonoBehaviour {

    public bool dirAvailable;
    [HideInInspector]
    public Vector3 dirPosition;
    private Material triggerColor;

    private void Start()
    {
        dirAvailable = false;
        triggerColor = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Points" ) 
        {
            Debug.Log("Hit");
            dirAvailable = true;
            dirPosition = other.transform.position;
            triggerColor.color = Color.green;
        }

        else if(other.transform.tag == "Defender")
        {
            dirAvailable = false;
        }

        else
            return;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Points" || other.transform.tag == "Defender")
        {
            dirAvailable = false;
            dirPosition = this.transform.position; //if no object collided
            triggerColor.color = Color.yellow;
        }
        else
            return;
    }
}
