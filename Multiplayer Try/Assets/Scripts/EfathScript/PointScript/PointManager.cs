using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointManager : MonoBehaviour {
    public List<GameObject> PointChild;

    CharacterSelectLogic ManagerCharSelectLogic;

   public int IndexLimit;
    public int TotalPlaced;
    public bool[] CheckPlaced;
	// Use this for initialization

    

	void Start () {
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();
       
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void DeffensePosisition(int IndexPoint) {
        
        if (CheckPlaced[IndexLimit] || CameraRaycastPointer.hittedObject.transform.tag!="Points")
        {
            
            return;
        }
       IndexLimit = (int) ManagerCharSelectLogic.currentChar;
        Debug.Log("Object Hit " + CameraRaycastPointer.hittedObject.transform.position);

        ManagerChar.instance.CharIndex[IndexLimit].GetComponent<Button>().interactable = false;
        CheckPlaced[IndexLimit] = true;
        TotalPlaced += 1;
        ManagerCharSelectLogic.CharacterPoint[IndexLimit].transform.position = CameraRaycastPointer.hittedObject.transform.position;
        for (int i = 0; i < ManagerCharSelectLogic.CharacterPoint[ManagerChar.instance.CharIndexGlobal].GetComponent<DirectionControl>().movTrigger.dirDetector.Length; i++)
        {
            ManagerCharSelectLogic.CharacterPoint[IndexLimit].GetComponent<DirectionControl>().movTrigger.dirDetector[i].gameObject.SetActive(false);
           
        }


        if (TotalPlaced < CheckPlaced.Length && IndexLimit < 3 )
        {
            IndexLimit += 1;
            ManagerCharSelectLogic.currentChar += 1;
            while (CheckPlaced[IndexLimit ])
            {
                IndexLimit += 1;
                ManagerCharSelectLogic.currentChar += 1;
                if (IndexLimit > 3) {
                    IndexLimit = 0;
                    ManagerCharSelectLogic.currentChar =0;
                }
                
                
            }

        }
        else if(TotalPlaced < CheckPlaced.Length && IndexLimit >= 3) {
            IndexLimit =0;
            ManagerCharSelectLogic.currentChar =0;
            while (CheckPlaced[IndexLimit] )
            {
                IndexLimit += 1;
                ManagerCharSelectLogic.currentChar += 1;
                if (IndexLimit > 3)
                {
                    IndexLimit = 0;
                    ManagerCharSelectLogic.currentChar = 0;
                }


            }

        }
    }
    
}
