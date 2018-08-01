﻿using System.Collections;
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
    }
}
