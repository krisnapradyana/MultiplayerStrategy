using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterControlFSM;

public class CharacterBehaviour : MonoBehaviour
{ 
    public int Speed;

    [HideInInspector]
    public Vector3 targetDir;
    public DirectionControl dir;
    public TurnBaseController turnController;

    public enum states
    {
        NullState = 0,
        CheckDirection, Moving, OutField, Dead
    }

    public states stateID;

    public StateMachine<CharacterBehaviour> stateMachine { get; set; }

    /// <summary>
    /// Initializing Constructor for Dynamic Objects
    /// </summary>
    private void Initialize()
    {
        turnController = GameObject.Find("TurnManager").GetComponent<TurnBaseController>();
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
       
           Debug.Log("End Turn? :" +  turnController.endTurn);
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


}
