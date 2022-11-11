using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    CharacterStatus character;
    CharacterController characterController;
    public DieState(CharacterStatus character, CharacterController characterController)
    {
        this.character = character;
        this.characterController = characterController;
    }
    public void StateEnd()
    {
        
    }

    public void StateStart()
    {
        characterController.StartAIDied();
        character.AIState = EAIState.Died;
    }

    public void StateUpdate()
    {
        
    }
}
