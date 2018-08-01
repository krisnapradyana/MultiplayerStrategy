using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterControlFSM;

/// <summary>
/// State where character are not yet spawned or while defender in Strategy mode
/// </summary>
public class OutfieldState : State<CharacterBehaviour>
{
    public static OutfieldState _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private OutfieldState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static OutfieldState Instance
    {
        get
        {
            if (_instance == null)
            {
                new OutfieldState();
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
