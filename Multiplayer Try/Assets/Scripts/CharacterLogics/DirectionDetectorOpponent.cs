using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDetectorOpponent : MonoBehaviour {

    DirectionDetector ManagerDirectionDetector;
    public bool OpponentTouchChild ;
	// Use this for initialization
	void Start () {
        ManagerDirectionDetector = GetComponentInParent<DirectionDetector>();
        OpponentTouchChild = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay(Collider other)
    {
        if (GetComponentInParent<CharacterBehaviour>().stateTypeID == CharacterBehaviour.statesType.Defender)
        {
            if (other.transform.tag == "Defender")
            {
                OpponentTouchChild = true;
                ManagerDirectionDetector.OpponentTouch = true;
                //  dirPosition = other.transform.position;
                //  triggerColor.color = Color.green;
            }
            else 

                return;
        }
        else if (GetComponentInParent<CharacterBehaviour>().stateTypeID == CharacterBehaviour.statesType.Attacker)
        {
            if (other.transform.tag == "Attacker")
            {
                OpponentTouchChild = true;
                ManagerDirectionDetector.OpponentTouch = true;
                //  dirPosition = other.transform.position;
                //  triggerColor.color = Color.green;
            }
            else 
            return;
        }
    }


    private void OnTriggerExit(Collider other)
    {
       

        if (GetComponentInParent<CharacterBehaviour>().stateTypeID == CharacterBehaviour.statesType.Defender)
        {
            if (other.transform.tag == "Defender")
            {
                ManagerDirectionDetector.OpponentTouch = false;

                ManagerDirectionDetector.dirPosition = this.transform.position; //if no object collided
                ManagerDirectionDetector.triggerColor.color = Color.yellow;

            }
            else
                return;
        }
        else if (GetComponentInParent<CharacterBehaviour>().stateTypeID == CharacterBehaviour.statesType.Attacker)
        {
            if (other.transform.tag == "Attacker")
            {
                ManagerDirectionDetector.OpponentTouch = false;

                ManagerDirectionDetector.dirPosition = this.transform.position; //if no object collided
                ManagerDirectionDetector.triggerColor.color = Color.yellow;

            }
            else
                return;
          
        }

    }

}
