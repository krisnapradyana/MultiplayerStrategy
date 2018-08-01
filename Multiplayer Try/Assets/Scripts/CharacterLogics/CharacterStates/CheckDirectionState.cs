using UnityEngine;
using CharacterControlFSM;

public class CheckDirectionState : State<CharacterBehaviour>
{
    private static CheckDirectionState _instance;

    /// <summary>
    /// Create an instance of this class function 
    /// </summary>
    private CheckDirectionState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static CheckDirectionState Instance
    {
        get
        {
            if(_instance == null)
            {
                new CheckDirectionState();
            }
            return _instance;
        }
    }

    public override void EnterState(CharacterBehaviour _characterState)
    {
        Debug.Log("Start Checking Direction");
    }

    public override void ExitState(CharacterBehaviour _characterState)
    {
        Debug.Log("Exit Checking Direction");
    }

    public override void UpdateState(CharacterBehaviour _characterState)
    {
        if (_characterState.stateID == CharacterBehaviour.states.Moving)
        {
            _characterState.stateMachine.ChangeState(MovingState.Instance);
        }
    }
}