﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyModeUI : MonoBehaviour
{

    TurnBaseController ManagerTurnBase;
    PointManager ManagerPoint;
    public CharacterSelectLogic ManagerCharSelectLogic;

    public Button[] StrategyUI; //Undo, Done
    public bool EndStrategyPressed;

    public static StrategyModeUI instace;

    public int IndexHoldMoving;
    public List<CharacterBehaviour> SaveCharMove;
    public static bool ChangeMove;

    private void Awake() 
    {
        instace = this;
    }

    // Use this for initialization
    void Start()
    {
        ManagerTurnBase = FindObjectOfType<TurnBaseController>();
        ManagerPoint = FindObjectOfType<PointManager>();
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();

        //   StrategyUI[1].interactable = true;
        StrategyUI[2].gameObject.SetActive(false);
       

        EndStrategyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        EndStrategyButtonManager();
       
    }

    /// <summary>
    /// Set Not Active Button in Strategy Mode
    /// Category: Button
    /// </summary>
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
    /// Category: Button
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
    /// Category: Button
    /// </summary>
    public void EndStrategyMode()
    {
        if (ManagerPoint.TotalPlaced >= ManagerPoint.CheckPlaced.Length)
        {
            EndStrategyPressed = true;
             StrategyUI[2].gameObject.SetActive(true);
            ManagerTurnBase.endTurn = true;

           
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
    ///  Category: Button
    /// </summary>
    public void EndTurn()
    {
        IndexHoldMoving += 1;
       
        ManagerTurnBase.endTurn = true;
        ManagerTurnBase.FixMove = false;
        GameManagerAll._instance.CharPrefabIndex[(int)ManagerTurnBase.stateID - 1] = (int) ManagerCharSelectLogic.currentChar;

        //ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();
        /*
        for (int i = 0; i < ManagerCharSelectLogic.switchCharacters.Length; i++)
        {
            ManagerTurnBase.PlayerManager[(int)ManagerTurnBase.stateID - 1].GetComponent<CharacterSelectLogic>().switchCharacters[i].interactable = true;
        }
        */

        for (int i = 0; i < ManagerChar.instance.CharIndex.Count; i++)
        {

            ManagerChar.instance.CharIndex[i].GetComponent<Button>().interactable = true;
        }
        if (IndexHoldMoving == 2)
        {
            IndexHoldMoving = 0;
            /*
            for (int i = 0; i < SaveCharMove.Count; i++)
            {
                SaveCharMove[i].stateID = CharacterBehaviour.states.Moving;
            }
            */
            SaveCharMove[0].stateID = CharacterBehaviour.states.Moving;
            if (SaveCharMove.Count > 1)
            {
                StartCoroutine(DelayMove());
            }
 
     
        }
    }

    /// <summary>
    /// Undo Turn (Never Used)
    /// Category: Button
    /// </summary>
    public void UndoTurn()
    {
        ManagerTurnBase.FixMove = false;
        ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<DirectionControl>().tarDir = ManagerTurnBase.PrevDir;
        ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<DirectionControl>().AssignDirection(ManagerTurnBase.PrevIndexEnumDir);
        ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<CharacterBehaviour>().Moving(ManagerCharSelectLogic.CharacterPoint[ManagerTurnBase.PrevIndexChar].GetComponent<CharacterBehaviour>().stateID);
        
        //StrategyModeUI.instace.StrategyUI[3].gameObject.SetActive(false);
      

        for (int i = 0; i < ManagerChar.instance.CharIndex.Count; i++)
        {

            ManagerChar.instance.CharIndex[i].GetComponent<Button>().interactable = true;
        }
        Debug.Log("masukUndo");
    }

    IEnumerator DelayMove()
    {
        if (ManagerTurnBase.stateID != TurnBaseController.states.BattleMode)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("2");
            SaveCharMove[0].stateID = CharacterBehaviour.states.Moving;
        }
        
                
    }

    void DelayMove2()
    {
        if (SaveCharMove[0].stateID == CharacterBehaviour.states.CheckDirection)
        {
            SaveCharMove[0].stateID = CharacterBehaviour.states.Moving;
        }
    }
}
