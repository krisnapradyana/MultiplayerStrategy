using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterControlFSM;

public class DiedState : State<CharacterBehaviour>
{
    public static DiedState _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private DiedState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static DiedState Instance
    {
        get
        {
            if (_instance == null)
            {
                new DiedState();
            }
            return _instance;
        }
    }

    public override void EnterState(CharacterBehaviour _characterState)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(CharacterBehaviour _characterState)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(CharacterBehaviour _characterState)
    {
        throw new System.NotImplementedException();
    }
}
