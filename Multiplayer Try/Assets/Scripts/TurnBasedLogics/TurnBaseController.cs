using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedFSM;

public class TurnBaseController : MonoBehaviour {

    public StateMachine<TurnBaseController> stateMachine { get; set; }

    public enum states
    {
        NullState = 0,
        Defender, Attacker, StrategyMode
    }


    //tambahan efath
    DirectionControl ManagerDirectControl;
    public GameObject[] PlayerManager; //Player 1 and Player 2
    public int PrevIndexChar;
    public int PrevIndexEnumDir;
    public Vector3 PrevDir;
    public bool FixMove;

    public bool[] AttackFirstPos;
    //

    public states stateID;
    public bool endTurn;
   

	// Use this for initialization
	void Start () {
        stateID = states.NullState;
        stateMachine = new StateMachine<TurnBaseController>(this);
        stateMachine.ChangeState(FirstPlayerTurn.Instance);
        stateID = states.StrategyMode;

        ManagerDirectControl = FindObjectOfType<DirectionControl>();

        PlayerManager[1].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        #region  Define State and Condition
        if(endTurn == true)
        {
            if(stateID == states.Defender)
            {
                stateID = states.Attacker;
                stateMachine.Update();
                PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = false ;
                PlayerManager[1].GetComponent<CharacterSelectLogic>().enabled = true;
                // Tambahan sementara untuk mode offline

            }
            else if(stateID == states.Attacker)
            {
                stateID = states.Defender;
                stateMachine.Update();
                PlayerManager[1].GetComponent<CharacterSelectLogic>().enabled = false;
                PlayerManager[0].GetComponent<CharacterSelectLogic>().enabled = true;
                // Tambahan sementara untuk mode offline

            }
            else if (stateID == states.StrategyMode)
            {
                // Efath Comment
                //stateID = states.StrategyMode;
                //stateMachine.Update();

                stateID = states.Attacker;
                stateMachine.Update();

                // Tambahan sementara untuk mode offline
                PlayerManager[1].SetActive(true);
                //PlayerManager[0].SetActive(false);

            }
            endTurn = false; // tambahan efath
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
