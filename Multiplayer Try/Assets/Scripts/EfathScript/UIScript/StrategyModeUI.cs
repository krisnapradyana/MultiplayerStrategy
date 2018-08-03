using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyModeUI : MonoBehaviour
{

    TurnBaseController ManagerTurnBase;
    PointManager ManagerPoint;
    CharacterSelectLogic ManagerCharSelectLogic;

    public Button[] StrategyUI; //Undo, Done
    public bool EndStrategyPressed;

    // Use this for initialization
    void Start()
    {
        ManagerTurnBase = FindObjectOfType<TurnBaseController>();
        ManagerPoint = FindObjectOfType<PointManager>();
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();

        //   StrategyUI[1].interactable = true;
        StrategyUI[2].gameObject.SetActive(false);
        StrategyUI[3].gameObject.SetActive(false);

        EndStrategyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        EndStrategyButtonManager();
        ManageFixMove();
    }

    void EndTurnButtonManager()
    {

    }

    void EndStrategyButtonManager()
    {
        if (ManagerPoint.TotalPlaced < 4 || EndStrategyPressed)
        {
            StrategyUI[1].gameObject.SetActive(false);
        }
        else {
            StrategyUI[1].gameObject.SetActive(true);
        }

        if (ManagerPoint.TotalPlaced <= 0 || EndStrategyPressed)
        {
            StrategyUI[0].gameObject.SetActive(false);
        }
        else {
            StrategyUI[0].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Button Undo Position Deffense Player when StrategyMode
    /// </summary>
    /// 
    public void UndoStrategyPlaced()
    {

        if (ManagerPoint.PrevIndexLimitCollect.Count < 1)
        {

            return;
        }
        ManagerCharSelectLogic.currentChar = (CharacterSelectLogic.character)ManagerPoint.PrevLimit;
        ManagerPoint.TotalPlaced -= 1;
        ManagerCharSelectLogic.CharacterPoint[ManagerPoint.PrevLimit].transform.position = new Vector3(100, 0, 0);
        ManagerPoint.PrevIndexLimitCollect.Remove(ManagerPoint.PrevIndexLimitCollect[ManagerPoint.PrevIndexLimitCollect.Count - 1]);

        ManagerChar.instance.CharIndex[ManagerPoint.PrevLimit].GetComponent<Button>().interactable = true;
        ManagerPoint.CheckPlaced[ManagerPoint.PrevLimit] = false;

        if (ManagerPoint.PrevIndexLimitCollect.Count >=1)
        {
            ManagerPoint.PrevLimit = ManagerPoint.PrevIndexLimitCollect[ManagerPoint.PrevIndexLimitCollect.Count - 1];
        }
        



    }

    /// <summary>
    /// Button Done/Finish When Deffense Player in Strategy Mode
    /// </summary>
    public void EndStrategyMode()
    {
        if (ManagerPoint.TotalPlaced >= ManagerPoint.CheckPlaced.Length)
        {
            EndStrategyPressed = true;
             StrategyUI[2].gameObject.SetActive(true);
            ManagerTurnBase.endTurn = true;

            ManagerTurnBase.stateID = TurnBaseController.states.Attacker;
            ManagerTurnBase.stateMachine.Update();
            // SetActive Direction Control
            
            for (int j = 0; j < ManagerCharSelectLogic.CharacterPoint.Length; j++)
            {
                for (int i = 0; i < ManagerCharSelectLogic.CharacterPoint[ManagerChar.instance.CharIndexGlobal].GetComponent<DirectionControl>().movTrigger.dirDetector.Length; i++)
                {
                    ManagerCharSelectLogic.CharacterPoint[j].GetComponent<DirectionControl>().movTrigger.dirDetector[i].gameObject.SetActive(true);

                }
            }
            
            // Set Active Button Choose Player
            for (int i = 0; i < ManagerCharSelectLogic.switchCharacters.Length; i++)
            {
                ManagerCharSelectLogic.switchCharacters[i].interactable = true;
            }
        }   
    }

    /// <summary>
    ///  End Turn Every Player -> Prototype 
    /// </summary>
    public void EndTurn()
    {
        ManagerTurnBase.endTurn = true;
        
        //ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();
    }

    public void UndoTurn()
    {
        
        ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<DirectionControl>().tarDir = ManagerTurnBase.PrevDir;
        ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<DirectionControl>().AssignDirection(ManagerTurnBase.PrevIndexEnumDir);
        ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<CharacterBehaviour>().Moving(ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<CharacterBehaviour>().stateID);
       
        StrategyUI[3].gameObject.SetActive(false);
        ManagerTurnBase.FixMove = false;
        /*
        for (int i = 0; i < ManagerChar.instance.CharIndex.Count; i++)
        {

            ManagerChar.instance.CharIndex[i].GetComponent<Button>().interactable = true;
        }
        */
    }

    public void ManageFixMove()
    {
        if (ManagerTurnBase.FixMove)
        {
            for (int j = 0; j < ManagerCharSelectLogic.CharacterPoint.Length; j++)
            {
                for (int i = 0; i < ManagerCharSelectLogic.CharacterPoint[ManagerChar.instance.CharIndexGlobal].GetComponent<DirectionControl>().movTrigger.dirDetector.Length; i++)
                {
                    ManagerCharSelectLogic.CharacterPoint[j].GetComponent<DirectionControl>().movTrigger.dirDetector[i].gameObject.SetActive(false);

                }
            }
        }
    }
}
