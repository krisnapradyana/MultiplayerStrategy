using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycastPointer : MonoBehaviour {
  
    public float RayLength;
    public LayerMask layerMask;
    public static Transform hittedObject;

    CharacterSelectLogic ManagerCharSelectLogic;
    [SerializeField]
   public DirectionControl ManagerDirectionControl;
    TurnBaseController ManagerTurnControl;

    public static Vector3 PosisitionPoints;

    public List<Transform> PositionPoint;

    // Use this for initialization
    void Start () {
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();
        ManagerDirectionControl = FindObjectOfType<DirectionControl>();
        ManagerTurnControl = FindObjectOfType<TurnBaseController>();

       
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
             
                hittedObject = hit.transform;
                if (hit.transform.tag != "Points")
                {
                    return;
                }
                if (ManagerCharSelectLogic.ManagerTurnController.stateID == TurnBaseController.states.StrategyMode)
                {
                    ManagerDirectionControl = ManagerTurnControl.PlayerManager[0].GetComponent<CharacterSelectLogic>().transform.GetChild((int)ManagerTurnControl.PlayerManager[0].GetComponent<CharacterSelectLogic>().currentChar).GetComponent<DirectionControl>();
                    ManagerDirectionControl.AssignDeffense();
                   // ManagerDirectionControl.AssignDeffense();
                }
                else if(ManagerCharSelectLogic.ManagerTurnController.stateID == TurnBaseController.states.Attacker) {
                    if (ManagerTurnControl.AttackFirstPos[(int)ManagerCharSelectLogic.currentChar]== true)
                    {

                        return;
                    }
                    
                    ManagerTurnControl.PlayerManager[1].GetComponent<CharacterSelectLogic>().transform.GetChild((int)ManagerTurnControl.PlayerManager[0].GetComponent<CharacterSelectLogic>().currentChar).GetComponent<DirectionControl>().AssignAttacker();
                    //  ManagerDirectionControl.AssignAttacker();
                }
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


    public void SelectPointButton(int PosisitionPointindex)
    {
        PosisitionPoints = PositionPoint[PosisitionPointindex].transform.position;

        if (ManagerCharSelectLogic.ManagerTurnController.stateID == TurnBaseController.states.StrategyMode)
        {
            ManagerDirectionControl = ManagerTurnControl.PlayerManager[0].GetComponent<CharacterSelectLogic>().transform.GetChild((int)ManagerTurnControl.PlayerManager[0].GetComponent<CharacterSelectLogic>().currentChar).GetComponent<DirectionControl>();
            ManagerDirectionControl.AssignDeffense();
            // ManagerDirectionControl.AssignDeffense();
        }
        else if (ManagerCharSelectLogic.ManagerTurnController.stateID == TurnBaseController.states.Attacker)
        {
            if (ManagerTurnControl.AttackFirstPos[(int)ManagerCharSelectLogic.currentChar] == true)
            {

                return;
            }
            if (PosisitionPointindex == 6 || PosisitionPointindex == 7 || PosisitionPointindex == 8)
            {
                ManagerTurnControl.PlayerManager[1].GetComponent<CharacterSelectLogic>().transform.GetChild((int)ManagerTurnControl.PlayerManager[0].GetComponent<CharacterSelectLogic>().currentChar).GetComponent<DirectionControl>().AssignAttacker();
            }

           
            //  ManagerDirectionControl.AssignAttacker();
        }

    }
}

