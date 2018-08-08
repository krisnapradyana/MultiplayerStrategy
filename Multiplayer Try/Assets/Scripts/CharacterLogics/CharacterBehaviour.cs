using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterControlFSM;

public class CharacterBehaviour : MonoBehaviour
{
    public int Speed;

    //Tambah Efath

    public CharacterSelectLogic ManagerCharSelectLogic;
    public PointManager ManagerPointManager;
    StrategyModeUI ManagerStrategyUI;

    //

    [HideInInspector]
    public Vector3 targetDir;
    public DirectionControl dir;
    public TurnBaseController turnController;

    public enum states
    {
        NullState = 0,
        CheckDirection, Moving, HoldMoving, OutField, Dead, DeffensePosition, AttackerPosition
    }

    public enum statesType
    {
        NullState = 0,
        Attacker,Defender
    }

    public states stateID;
    public statesType stateTypeID;


    public StateMachine<CharacterBehaviour> stateMachine { get; set; }

    /// <summary>
    /// Initializing Constructor for Dynamic Objects
    /// </summary>
    private void Initialize()
    {
        turnController = FindObjectOfType<TurnBaseController>();
        ManagerCharSelectLogic = GetComponentInParent<CharacterSelectLogic>(); // tambah efath
        ManagerPointManager = FindObjectOfType<PointManager>();
        ManagerStrategyUI = FindObjectOfType<StrategyModeUI>();
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
        DefensePosisition(turnController.stateID,stateID); // tambah efath
    
        Debug.Log("End Turn? :" + turnController.endTurn);
        #endregion
        //}
    }

    /// <summary>
    /// Moving state function for Character
    /// Category: Update
    /// </summary>
    /// <param name="_stateID"></param>
    public void Moving(states _stateID)
    {
       
        if (_stateID == states.Moving)
        {
           
           // turnController.PrevDir = ManagerCharSelectLogic.CharacterPoint[turnController.PrevIndexChar].GetComponent<DirectionControl>().tarDir; // tambahan efath
            this.transform.position = Vector3.MoveTowards(this.transform.position, dir.tarDir, Speed * Time.deltaTime);

            // tambahan efath

            //  turnController.PrevIndexChar = (int)ManagerCharSelectLogic.currentChar;



            StrategyModeUI.instace.SaveCharMove.Remove(this);
         

            _stateID = states.CheckDirection;

        }
        else return;
    }

    /// <summary>
    /// Manage Position For Deffense Player
    /// /// Category: Update
    /// </summary>
    /// <param name="_stateID"></param>
    /// <param name="_StattesSID"></param>
    public void DefensePosisition(TurnBaseController.states _stateID,states _StattesSID)
    {
        if (_stateID == TurnBaseController.states.StrategyMode && _StattesSID == states.DeffensePosition)
        { 
      
            ManagerPointManager.IndexLimit = (int)ManagerCharSelectLogic.currentChar;

            if (ManagerPointManager.CheckPlaced[ManagerPointManager.IndexLimit] || CameraRaycastPointer.hittedObject.transform.tag != "Points" || ManagerPointManager.TotalPlaced > 3)
            {

                return;
            }

            ManagerPointManager.PrevIndexLimitCollect.Add(ManagerPointManager.IndexLimit);
            ManagerPointManager.PrevLimit = ManagerPointManager.PrevIndexLimitCollect[ManagerPointManager.PrevIndexLimitCollect.Count - 1];


            ManagerChar.instance.CharIndex[ManagerPointManager.IndexLimit].GetComponent<Button>().interactable = false;
            ManagerPointManager.CheckPlaced[ManagerPointManager.IndexLimit] = true;
            ManagerPointManager.TotalPlaced += 1;

            ManagerCharSelectLogic.CharacterPoint[ManagerPointManager.IndexLimit].transform.position = CameraRaycastPointer.hittedObject.transform.position;
            for (int i = 0; i < ManagerCharSelectLogic.CharacterPoint[ManagerChar.instance.CharIndexGlobal].GetComponent<DirectionControl>().movTrigger.dirDetector.Length; i++)
            {
                ManagerCharSelectLogic.CharacterPoint[ManagerPointManager.IndexLimit].GetComponent<DirectionControl>().movTrigger.dirDetector[i].gameObject.SetActive(false);

            }



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

            else if (ManagerPointManager.TotalPlaced == ManagerPointManager.CheckPlaced.Length || ManagerPointManager.IndexLimit == 3)
            {


                ManagerPointManager.IndexLimit = 0;
                ManagerCharSelectLogic.currentChar = 0;


                if (ManagerPointManager.TotalPlaced == ManagerPointManager.CheckPlaced.Length)
                {
                    stateID = states.CheckDirection;
                    return;
                }
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

            stateID = states.CheckDirection;
        }

        else if (_stateID == TurnBaseController.states.Attacker && _StattesSID == states.AttackerPosition)
        {
            if (CameraRaycastPointer.hittedObject.transform.tag == "Points" && (CameraRaycastPointer.hittedObject.transform.name == "RightPoint1" || CameraRaycastPointer.hittedObject.transform.name == "RightPoint2" ||  CameraRaycastPointer.hittedObject.transform.name == "RightPoint3") && turnController.AttackFirstPos[(int)ManagerCharSelectLogic.currentChar] == false)
            {
                
                ManagerCharSelectLogic.CharacterPoint[(int)ManagerCharSelectLogic.currentChar].transform.position = CameraRaycastPointer.hittedObject.transform.position;

                /*
                for (int i = 0; i < ManagerCharSelectLogic.switchCharacters.Length; i++)
                {
                    ManagerCharSelectLogic.switchCharacters[i].interactable = false;
                }
                */

                turnController.AttackFirstPos[(int)ManagerCharSelectLogic.currentChar] = true;
                stateID = states.CheckDirection;
                turnController.FixMove = true;

             
                //Debug.Log("Enter Attacker Position");
            }
        }
    }
}
