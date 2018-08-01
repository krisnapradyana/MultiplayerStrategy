using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSelectionFSM
{
    public class StateMachine<T>
    {
        public State<T> currentState { get; private set; }
        public T characterState;

        public StateMachine(T _state)
        {
            characterState = _state;
            currentState = null;
        }

        public void ChangeState(State<T> _newState)
        {
            //initialize starting state
            if (currentState != null)
            currentState.ExitState(characterState);
            currentState = _newState;
            currentState.EnterState(characterState);
            //Debug.Log("Initializing State " + currentState.ToString());

        }

        public void Update()
        {
            if (currentState != null)
                currentState.UpdateState(characterState);
        }
    }

    public abstract class State<T>
    {
        public abstract void EnterState(T _cursorState);
        public abstract void ExitState(T _cursorState);
        public abstract void UpdateState(T _cursorState);
    }
}