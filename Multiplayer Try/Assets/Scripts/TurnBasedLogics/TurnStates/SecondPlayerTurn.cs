using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedFSM;

public class SecondPlayerTurn : State<TurnBaseController>
{
    private static SecondPlayerTurn _instance;

    private SecondPlayerTurn()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondPlayerTurn Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondPlayerTurn();
            }
            return _instance;
        }
    }

    public override void EnterState(TurnBaseController _turnState)
    {
        Debug.Log("Entering Second Player State");
        _turnState.endTurn = false;
    }

    public override void ExitState(TurnBaseController _turnState)
    {
        Debug.Log("Exit Second Player State");
    }

    public override void UpdateState(TurnBaseController _turnState)
    { 
        if (_turnState.stateID == TurnBaseController.states.FirstPlayer)
        {
            _turnState.stateMachine.ChangeState(FirstPlayerTurn.Instance);
        }
    }
}
