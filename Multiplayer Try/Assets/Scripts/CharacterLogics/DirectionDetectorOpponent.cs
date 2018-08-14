using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDetectorOpponent : MonoBehaviour {

    DirectionDetector ManagerDirectionDetector;
    public GameObject FriendObject;
    public GameObject EnemyObject;
    public bool OpponentTouchChild ;
    public bool EnemyDetected;
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
                FriendObject = other.transform.gameObject;
                
            }
            else if (other.transform.tag == "Attacker")
            {
                EnemyDetected = true;
                EnemyObject = other.transform.gameObject;
                //  dirPosition = other.transform.position;
                //  triggerColor.color = Color.green;
            }

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
                FriendObject = other.transform.gameObject;
            }
            else if (other.transform.tag == "Defender")
            {
                EnemyDetected = true;
                EnemyObject = other.transform.gameObject;
                //  dirPosition = other.transform.position;
                //  triggerColor.color = Color.green;
            }
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

              //  other.transform.GetComponentInParent<DirectionDetector>().OpponentTouch = false;
            }
            else if (other.transform.tag == "Attacker")
            {
                EnemyDetected = false;
                //  dirPosition = other.transform.position;
                //  triggerColor.color = Color.green;
            }
            return;
        }
        else if (GetComponentInParent<CharacterBehaviour>().stateTypeID == CharacterBehaviour.statesType.Attacker)
        {
            if (other.transform.tag == "Attacker")
            {
                ManagerDirectionDetector.OpponentTouch = false;

                ManagerDirectionDetector.dirPosition = this.transform.position; //if no object collided
                ManagerDirectionDetector.triggerColor.color = Color.yellow;

               // other.transform.GetComponentInParent<DirectionDetector>().OpponentTouch = false;
            }
            else  if (other.transform.tag == "Defender")
            {
                EnemyDetected = false;
                //  dirPosition = other.transform.position;
                //  triggerColor.color = Color.green;
            }
            return;
          
        }

    }

}
