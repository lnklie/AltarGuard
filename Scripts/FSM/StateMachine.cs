using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; set; }

    public StateMachine(IState currentState)
    {
        CurrentState = currentState;
    }


    public void SetState(IState state)
    {
        if(CurrentState == state)
        {
            return;
        }
        CurrentState.StateEnd();

        CurrentState = state;

        CurrentState.StateStart();
    }

    public void DoUpdateState()
    {
        CurrentState.StateUpdate();
    }
}
