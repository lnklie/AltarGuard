using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    CharacterStatus character;
    CharacterController characterController;
    public AttackState(CharacterStatus character, CharacterController characterController)
    {
        this.character = character;
        this.characterController = characterController;
    }
    public void StateEnd()
    {
        
    }

    public void StateStart()
    {
        character.ActiveLayer(ELayerName.AttackLayer);
        character.Ani.SetFloat("AtkType", character.AttackType);
        character.AIState = EAIState.Attack;
        character.Rig.velocity = Vector2.zero;
        characterController.StartAttack();
    }

    public void StateUpdate()
    {
        

    }
}
