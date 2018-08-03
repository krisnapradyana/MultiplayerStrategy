using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterSelectionFSM;

public class CharacterSelectLogic : MonoBehaviour {

    //Tambahan Efath
   
    public GameObject[] CharacterPoint;
    TurnBaseController ManagerTurnController;
    //Tambahan Efath
    
    public DirectionControl[] charactersControl;
    public Button[] switchCharacters;
    public Button[] directionalButtons;

    public enum character
    {
        FirstCharacter = 0, SecondCharacter = 1, ThirdCharacter = 2, FourthCharacter = 3
    }

    public character currentChar;

    public StateMachine<CharacterSelectLogic> stateMachine { get; set;}

    private void Initialize()
    {
        stateMachine = new StateMachine<CharacterSelectLogic>(this);
        ManagerTurnController = FindObjectOfType<TurnBaseController>();

        #region AssignSwitch Character Buttons
        switchCharacters[(int)character.FirstCharacter].onClick.AddListener(delegate { FirstChar(); });
        switchCharacters[(int)character.SecondCharacter].onClick.AddListener(delegate { SecondChar(); });
        switchCharacters[(int)character.ThirdCharacter].onClick.AddListener(delegate { ThirdChar(); });
        switchCharacters[(int)character.FourthCharacter].onClick.AddListener(delegate { FourthChar(); });
        #endregion
        return;
    }

    private void Start()
    {
        Initialize();
        stateMachine.ChangeState(FirstCharacter.Instance);
        currentChar = character.FirstCharacter;

        AssignCharacterControls();
    }

    private void FixedUpdate()
    {
        Debug.Log("Current Char : " + currentChar.ToString());
        AcceptInputs();
    }

    public void AssignCharacterControls()
    {
        //Debug.Log("Assigning Character control");
        if (currentChar == character.FirstCharacter)
        {
            directionalButtons[(int)DirectionControl.Directions.up].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.up); });
            directionalButtons[(int)DirectionControl.Directions.down].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.down); });
            directionalButtons[(int)DirectionControl.Directions.left].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.left); });
            directionalButtons[(int)DirectionControl.Directions.right].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.right); });
        }
        else if (currentChar == character.SecondCharacter)
        {
            directionalButtons[(int)DirectionControl.Directions.up].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.up); });
            directionalButtons[(int)DirectionControl.Directions.down].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.down); });
            directionalButtons[(int)DirectionControl.Directions.left].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.left); });
            directionalButtons[(int)DirectionControl.Directions.right].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.right); });
        }
        else if (currentChar == character.ThirdCharacter)
        {
            directionalButtons[(int)DirectionControl.Directions.up].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.up); });
            directionalButtons[(int)DirectionControl.Directions.down].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.down); });
            directionalButtons[(int)DirectionControl.Directions.left].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.left); });
            directionalButtons[(int)DirectionControl.Directions.right].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.right); });
        }
        else if (currentChar == character.FourthCharacter)
        {
            directionalButtons[(int)DirectionControl.Directions.up].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.up); });
            directionalButtons[(int)DirectionControl.Directions.down].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.down); });
            directionalButtons[(int)DirectionControl.Directions.left].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.left); });
            directionalButtons[(int)DirectionControl.Directions.right].onClick.AddListener(delegate { charactersControl[(int)currentChar].AssignDirection((int)DirectionControl.Directions.right); });
        }   
    }

    /// <summary>
    /// Read all inputs according to controlled character
    /// </summary>
    public void AcceptInputs()
    {
        //there is 4 directions, so the range hardcoded as 4 
        for (int i = 0; i < charactersControl[(int)currentChar].movTrigger.dirDetector.Length ; i++)
        {
            // Tambahan Efath
             if ( ManagerTurnController.FixMove)
            {
                directionalButtons[i].interactable = false;
            }
             //

           else if (charactersControl[(int)currentChar].movTrigger.dirDetector[i].dirAvailable == true && charactersControl[(int)currentChar].charBehave.stateID == CharacterBehaviour.states.CheckDirection)
            {
                directionalButtons[i].interactable = true;
            }

            else if (charactersControl[(int)currentChar].movTrigger.dirDetector[i].dirAvailable == false && charactersControl[(int)currentChar].charBehave.stateID == CharacterBehaviour.states.CheckDirection )
            {
                directionalButtons[i].interactable = false;
            }

            else if (charactersControl[(int)currentChar].movTrigger.dirDetector[i].dirAvailable == false && charactersControl[(int)currentChar].charBehave.stateID != CharacterBehaviour.states.CheckDirection)
            {
                directionalButtons[i].interactable = false;
            }

            
        
        }
    }

    void FirstChar()
    {
        currentChar = character.FirstCharacter;
        stateMachine.ChangeState(FirstCharacter.Instance);
        stateMachine.Update();
    }
    void SecondChar()
    {
        currentChar = character.SecondCharacter;
        stateMachine.ChangeState(SecondCharacter.Instance);
        stateMachine.Update();
    }
    void ThirdChar()
    {
        currentChar = character.ThirdCharacter;
        stateMachine.ChangeState(ThirdCharacter.Instance);
        stateMachine.Update();
    }
    void FourthChar()
    {
        currentChar = character.FourthCharacter;
        stateMachine.ChangeState(FourthCharacter.Instance);
        stateMachine.Update();
    }
}
