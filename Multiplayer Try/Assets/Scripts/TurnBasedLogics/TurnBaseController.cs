using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedFSM;

public class TurnBaseController : MonoBehaviour {

    public StateMachine<TurnBaseController> stateMachine { get; set; }

    public enum states
    {
        NullState = 0,
        FirstPlayer, SecondPlayer, StrategyMode
    }

    public states stateID;
    public bool endTurn;

	// Use this for initialization
	void Start () {
        stateID = states.NullState;
        stateMachine = new StateMachine<TurnBaseController>(this);
        stateMachine.ChangeState(FirstPlayerTurn.Instance);
        stateID = states.FirstPlayer;
	}
	
	// Update is called once per frame
	void Update () {
        #region  Define State and Condition
        if(endTurn == true)
        {
            if(stateID == states.FirstPlayer)
            {
                stateID = states.SecondPlayer;
                stateMachine.Update();
            }
            else if(stateID == states.SecondPlayer)
            {
                stateID = states.FirstPlayer;
                stateMachine.Update();
            }
        }
        #endregion

        #region Action and Checkers
        Sides();
        //Debug.Log("Sides : " + stateID.ToString());
        #endregion
    }

    public void Sides()
    {
        //fill something later
    }
}
