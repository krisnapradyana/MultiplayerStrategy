using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSelectionFSM;

public class SecondCharacter : State<CharacterSelectLogic>
{
    private static SecondCharacter _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private SecondCharacter()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondCharacter Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondCharacter();
            }
            return _instance;
        }
    }

    public override void EnterState(CharacterSelectLogic _cursorState)
    {
        Debug.Log("Assiged to Second Character");
    }

    public override void ExitState(CharacterSelectLogic _cursorState)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(CharacterSelectLogic _cursorState)
    {
 
    }
}
