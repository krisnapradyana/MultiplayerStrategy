using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSelectionFSM;

public class FourthCharacter : State<CharacterSelectLogic>
{
    private static FourthCharacter _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private FourthCharacter()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FourthCharacter Instance
    {
        get
        {
            if (_instance == null)
            {
                new FourthCharacter();
            }
            return _instance;
        }
    }

    public override void EnterState(CharacterSelectLogic _cursorState)
    {
        Debug.Log("Assiged to Fourth Character");
    }

    public override void ExitState(CharacterSelectLogic _cursorState)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(CharacterSelectLogic _cursorState)
    {

    }
}
