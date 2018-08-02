using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycastPointer : MonoBehaviour {
  
    public float RayLength;
    public LayerMask layerMask;
    public static Transform hittedObject;

    CharacterSelectLogic ManagerCharSelectLogic;
    DirectionControl ManagerDirectionControl;

    

    // Use this for initialization
    void Start () {
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();
        ManagerDirectionControl = FindObjectOfType<DirectionControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, RayLength, layerMask))
            {
                Debug.Log(hit.transform.name);
                hittedObject = hit.transform;
                ManagerDirectionControl.AssignDeffense();
            }


            #region edited
                /*
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray,out hit,RayLength,layerMask))
                {
                    if (hit.transform!=null)
                    {

                        if (CheckPlaced[IndexLimit])
                        {
                            return;
                        }

                        Debug.Log(hit.transform.name);
                        ManagerCharSelectLogic.CharacterPoint[(int)ManagerCharSelectLogic.currentChar].transform.position = hit.transform.position;

                        IndexLimit = (int)ManagerCharSelectLogic.currentChar;


                        ManagerChar.instance.CharIndex[IndexLimit].GetComponent<Button>().interactable = false;
                        CheckPlaced[IndexLimit] = true;
                        TotalPlaced += 1;
                        for (int i = 0; i < ManagerCharSelectLogic.CharacterPoint[ManagerChar.instance.CharIndexGlobal].GetComponent<DirectionControl>().movTrigger.dirDetector.Length; i++)
                        {
                            ManagerCharSelectLogic.CharacterPoint[IndexLimit].GetComponent<DirectionControl>().movTrigger.dirDetector[i].gameObject.SetActive(false);

                        }


                        if (TotalPlaced < CheckPlaced.Length && IndexLimit < 3)
                        {
                            IndexLimit += 1;
                            ManagerCharSelectLogic.currentChar += 1;
                            while (CheckPlaced[IndexLimit])
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
                        else if (TotalPlaced < CheckPlaced.Length && IndexLimit >= 3)
                        {
                            IndexLimit = 0;
                            ManagerCharSelectLogic.currentChar = 0;
                            while (CheckPlaced[IndexLimit])
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
                    */
                #endregion
        }
    }
}

