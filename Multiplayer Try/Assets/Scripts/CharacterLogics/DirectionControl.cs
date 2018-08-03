using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionControl : MonoBehaviour {

    public enum Directions
    {
        up, down, left, right
    }

    [System.Serializable]
    public class MovementTrigger
    {
        //public Button[] buttonTriggers;
        public DirectionDetector[] dirDetector;
    }

    //public static bool moveAvailable;
    public Vector3 tarDir;
    public CharacterBehaviour charBehave;
    public MovementTrigger movTrigger = new MovementTrigger();

    private void Initialize()
    {
        charBehave = GetComponent<CharacterBehaviour>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {

    }

    public void AssignDirection(int _triggerIndex)
    {
        charBehave.stateID = CharacterBehaviour.states.Moving;
        charBehave.stateMachine.Update();
        tarDir = movTrigger.dirDetector[_triggerIndex].transform.position;

        charBehave.turnController.FixMove = true;
        if (_triggerIndex % 2 == 0)
        {
            charBehave.turnController.PrevIndexEnumDir = _triggerIndex + 1;
        }
        else {
            charBehave.turnController.PrevIndexEnumDir = _triggerIndex - 1;
        }


    }
    /// <summary>
    /// Assign Position For Deffense Player when Strategy Mode
    /// </summary>
    public void AssignDeffense()
    {
        charBehave.stateID = CharacterBehaviour.states.DeffensePosition;
        charBehave.stateMachine.Update();
        tarDir = CameraRaycastPointer.hittedObject.transform.position;
    }
}
