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

    // Use this for initialization
    void Start()
    {
        ManagerTurnBase = FindObjectOfType<TurnBaseController>();
        ManagerPoint = FindObjectOfType<PointManager>();
        ManagerCharSelectLogic = FindObjectOfType<CharacterSelectLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        InteractableUICheck();
    }

     void InteractableUICheck()
    {
        if (ManagerPoint.TotalPlaced > 3)
        {
            StrategyUI[1].interactable = true;
        }
        else {
            StrategyUI[1].interactable = false;
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
            StrategyUI[1].enabled = false;
            ManagerTurnBase.stateID = TurnBaseController.states.FirstPlayer;
        }

    }
}
