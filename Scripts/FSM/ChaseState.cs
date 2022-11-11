using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    CharacterStatus character;
    CharacterController characterController;
    public ChaseState(CharacterStatus character, CharacterController characterController)
    {
        this.character = character;
        this.characterController = characterController;
    }

    public void StateStart()
    {
        character.ActiveLayer(ELayerName.WalkLayer);
        character.AIState = EAIState.Chase;
    }


    public void StateUpdate()
    {
        characterController.AIChase();
    }
    public void StateEnd()
    {
        Debug.Log("³¡");
    }

}
