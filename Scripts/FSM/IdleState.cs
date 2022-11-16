using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    CharacterStatus character;
    public IdleState(CharacterStatus character)
    {
        this.character = character;
    }

    public void StateEnd()
    {
    }

    public void StateStart()
    {
        character.ActiveLayer(ELayerName.IdleLayer);
        character.AIState = EAIState.Idle;
        character.Rig.velocity = Vector2.zero;
    }

    public void StateUpdate()
    {
    }

}
