using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : IState
{
    CharacterStatus character;
    CharacterController characterController;
    public SkillState(CharacterStatus character, CharacterController characterController)
    {
        this.character = character;
        this.characterController = characterController;
    }
    public void StateEnd()
    {
        
    }

    public void StateStart()
    {
        character.IsUseSkill = true;
        character.AIState = EAIState.UseSkill;
        character.ActiveLayer(ELayerName.AttackLayer);
        character.Ani.SetFloat("AtkType", character.AttackType);
        
    }

    public void StateUpdate()
    {
        if(characterController.IsSkillDelay())
            characterController.StartAIUseSkill();
        character.Rig.velocity = Vector2.zero;
    }


}
