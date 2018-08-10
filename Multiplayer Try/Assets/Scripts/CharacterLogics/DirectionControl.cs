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

    /// <summary>
    /// Move Char with Button
    /// Category: Button
    /// </summary>
    /// <param name="_triggerIndex"></param>
    public void AssignDirection(int _triggerIndex)
    {
        if ( !transform.GetComponentInParent<CharacterSelectLogic>().isActiveAndEnabled )
        {
            return;
        }
        charBehave.stateID = CharacterBehaviour.states.HoldMoving;
        
        if (StrategyModeUI.instace.SaveCharMove.Count == 0)
        {
            StrategyModeUI.instace.SaveCharMove.Add(charBehave);
        } else 
        {
            if (StrategyModeUI.instace.SaveCharMove[0].Speed < charBehave.Speed)
            {
                CharacterBehaviour Temp;
                Temp = StrategyModeUI.instace.SaveCharMove[0];
                StrategyModeUI.instace.SaveCharMove.Remove(StrategyModeUI.instace.SaveCharMove[0]);
                StrategyModeUI.instace.SaveCharMove.Add(charBehave);
                StrategyModeUI.instace.SaveCharMove.Add(Temp);
            }
            else {
                StrategyModeUI.instace.SaveCharMove.Add(charBehave);
            }
            
            
        }
        charBehave.stateMachine.Update();
        tarDir = movTrigger.dirDetector[_triggerIndex].transform.position;
        

        //Tambahan Efath
        charBehave.turnController.FixMove = true;

        for (int i = 0; i < ManagerChar.instance.CharIndex.Count; i++)
        {

            ManagerChar.instance.CharIndex[i].GetComponent<Button>().interactable = false;
        }


    }
    /// <summary>
    /// Assign Position For Deffense Player when Strategy 
    /// Category: Button
    /// </summary>
    public void AssignDeffense()
    {
      
        charBehave.stateID = CharacterBehaviour.states.DeffensePosition;
        charBehave.stateMachine.Update();
        tarDir = CameraRaycastPointer.PosisitionPoints;
    }

    public void AssignAttacker()
    {

        charBehave.stateID = CharacterBehaviour.states.AttackerPosition;
      
        charBehave.stateMachine.Update();
        tarDir = CameraRaycastPointer.PosisitionPoints;

        for (int i = 0; i < ManagerChar.instance.CharIndex.Count; i++)
        {

            ManagerChar.instance.CharIndex[i].GetComponent<Button>().interactable = false;
        }

    }
}
