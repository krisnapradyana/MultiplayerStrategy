using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSelectionFSM;

public class ThirdCharacter : State<CharacterSelectLogic>
{
    private static ThirdCharacter _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private ThirdCharacter()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThirdCharacter Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThirdCharacter();
            }
            return _instance;
        }
    }

    public override void EnterState(CharacterSelectLogic _cursorState)
    {
        Debug.Log("Assiged to Third Character");
    }

    public override void ExitState(CharacterSelectLogic _cursorState)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(CharacterSelectLogic _cursorState)
    {
   
    }
}
