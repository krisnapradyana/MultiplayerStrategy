using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterControlFSM;

public class MovingState : State<CharacterBehaviour>
{
    public static MovingState _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private MovingState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static MovingState Instance
    {
        get
        {
            if (_instance == null)
            {
                new MovingState();
            }
            return _instance;
        }
    }

    public override void EnterState(CharacterBehaviour _characterState)
    {
        Debug.Log("Entering Move State");     
    }

    public override void ExitState(CharacterBehaviour _characterState)
    {
        Debug.Log("Exit Move State");
        //_characterState.turnController.endTurn = true;
    }

    public override void UpdateState(CharacterBehaviour _characterState)
    {
        if (_characterState.stateID == CharacterBehaviour.states.CheckDirection)
        {
            //Debug.Log("Chaging State");
            _characterState.stateMachine.ChangeState(CheckDirectionState.Instance);
        }
    }
}
