using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterControlFSM;

public class CharacterBehaviour : MonoBehaviour
{
    public int Speed;

    //Tambah Efath

    CharacterSelectLogic ManagerCharSelectLogic;
    PointManager ManagerPointManager;
 

    //

    [HideInInspector]
    public Vector3 targetDir;
    public DirectionControl dir;
    public TurnBaseController turnController;

    public enum states
    {
        NullState = 0,
        CheckDirection, Moving, OutField, Dead, DeffensePosition
    }

    public states stateID;

    public StateMachine<CharacterBehaviour> stateMachine { get; set; }

    /// <summary>
    /// Initializing Constructor for Dynamic Objects
    /// </summary>
    private void Initialize()
    {
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>(); // tambah efath
        turnController = FindObjectOfType<TurnBaseController>();
        ManagerPointManager = FindObjectOfType<PointManager>();
        dir = this.gameObject.GetComponent<DirectionControl>();
    }

    private void Start()
    {
        Initialize();

        stateID = states.NullState;
        stateMachine = new StateMachine<CharacterBehaviour>(this);
        stateMachine.ChangeState(CheckDirectionState.Instance);
        stateID = states.CheckDirection;
        stateMachine.Update();
    }

    private void Update()
    {
        //if (turnController.stateID == TurnBaseController.states.FirstPlayer)
        //{
        #region Define State Condition Here
        if (this.transform.position == dir.tarDir && stateID == states.Moving)
        {
            stateID = states.CheckDirection;
            stateMachine.Update();
            return;
        }
        #endregion

        #region Actions Checkers and Executors
        Moving(stateID);
        DeffensePosisition(turnController.stateID,stateID); // tambah efath

        Debug.Log("End Turn? :" + turnController.endTurn);
        #endregion
        //}
    }

    /// <summary>
    /// Moving state function for Character
    /// </summary>
    /// <param name="_stateID"></param>
    public void Moving(states _stateID)
    {
        if (_stateID == states.Moving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, dir.tarDir, Speed * Time.deltaTime);
        }
        else return;
    }

    /// <summary>
    /// Manage Position For Deffense Player
    /// </summary>
    /// <param name="_stateID"></param>
    /// <param name="_StattesSID"></param>
    public void DeffensePosisition(TurnBaseController.states _stateID,states _StattesSID)
    {
        if (_stateID == TurnBaseController.states.StrategyMode && _StattesSID == states.DeffensePosition)
        {
           Debug.Log("1");


            if (ManagerPointManager.CheckPlaced[ManagerPointManager.IndexLimit] || CameraRaycastPointer.hittedObject.transform.tag != "Points" || ManagerPointManager.TotalPlaced >3)
            {

                return;
            }
            

            ManagerPointManager.IndexLimit = (int)ManagerCharSelectLogic.currentChar;
            ManagerPointManager.PrevIndexLimitCollect.Add(ManagerPointManager.IndexLimit);
            ManagerPointManager.PrevLimit = ManagerPointManager.PrevIndexLimitCollect[ManagerPointManager.PrevIndexLimitCollect.Count-1];

            Debug.Log("Object Hit " + CameraRaycastPointer.hittedObject.transform.position);

            ManagerChar.instance.CharIndex[ManagerPointManager.IndexLimit].GetComponent<Button>().interactable = false;
            ManagerPointManager.CheckPlaced[ManagerPointManager.IndexLimit] = true;
            ManagerPointManager.TotalPlaced += 1;
            ManagerCharSelectLogic.CharacterPoint[ManagerPointManager.IndexLimit].transform.position = CameraRaycastPointer.hittedObject.transform.position;
            for (int i = 0; i < ManagerCharSelectLogic.CharacterPoint[ManagerChar.instance.CharIndexGlobal].GetComponent<DirectionControl>().movTrigger.dirDetector.Length; i++)
            {
                ManagerCharSelectLogic.CharacterPoint[ManagerPointManager.IndexLimit].GetComponent<DirectionControl>().movTrigger.dirDetector[i].gameObject.SetActive(false);

            }

            stateID = states.CheckDirection;

            if (ManagerPointManager.TotalPlaced < ManagerPointManager.CheckPlaced.Length && ManagerPointManager.IndexLimit < 3)
            {
                ManagerPointManager.IndexLimit += 1;
                ManagerCharSelectLogic.currentChar += 1;
                
                while (ManagerPointManager.CheckPlaced[ManagerPointManager.IndexLimit])
                {
                    ManagerPointManager.IndexLimit += 1;
                    ManagerCharSelectLogic.currentChar += 1;
                    if (ManagerPointManager.IndexLimit > 3)
                    {
                        ManagerPointManager.IndexLimit = 0;
                        ManagerCharSelectLogic.currentChar = 0;
                    }


                }

            }
            else if (ManagerPointManager.TotalPlaced < ManagerPointManager.CheckPlaced.Length && ManagerPointManager.IndexLimit >= 3)
            {
                ManagerPointManager.IndexLimit = 0;
                ManagerCharSelectLogic.currentChar = 0;
                
                while (ManagerPointManager.CheckPlaced[ManagerPointManager.IndexLimit])
                {
                    ManagerPointManager.IndexLimit += 1;
                    ManagerCharSelectLogic.currentChar += 1;
                    if (ManagerPointManager.IndexLimit > 3)
                    {
                        ManagerPointManager.IndexLimit = 0;
                        ManagerCharSelectLogic.currentChar = 0;
                    }


                }

            }

        }

    }
}
