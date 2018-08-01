using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedFSM;

public class FirstPlayerTurn : State<TurnBaseController>
{
    private static FirstPlayerTurn _instance;

    private FirstPlayerTurn()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstPlayerTurn Instance
    {
        get
        {
            if(_instance == null)
            {
                new FirstPlayerTurn();
            }
            return _instance;
        }
    }

    public override void EnterState(TurnBaseController _turnState)
    {
        Debug.Log("Entering First Player State");
        _turnState.endTurn = false;
    }

    public override void ExitState(TurnBaseController _turnState)
    {
        Debug.Log("Exiting First Player State");
    }

    public override void UpdateState(TurnBaseController _turnState)
    {
        Debug.Log("Updating State");
        if(_turnState.stateID == TurnBaseController.states.SecondPlayer)
        {
            _turnState.stateMachine.ChangeState(SecondPlayerTurn.Instance);
        }
    }
}
