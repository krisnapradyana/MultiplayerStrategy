using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSelectionFSM;

public class FirstCharacter : State<CharacterSelectLogic>
{
    private static FirstCharacter _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private FirstCharacter()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstCharacter Instance
    {
        get
        {
            if (_instance == null)
            {
                new FirstCharacter();
            }
            return _instance;
        }
    }

    public override void EnterState(CharacterSelectLogic _cursorState)
    {
        Debug.Log("Assiged to First Character");
    }

    public override void ExitState(CharacterSelectLogic _cursorState)
    {
        //Debug.Log("Exit First Character");
    }

    public override void UpdateState(CharacterSelectLogic _cursorState)
    {

    }
}
