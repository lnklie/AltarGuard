using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIController
{
    public void AIChangeState(CharacterStatus _status);
    public void AIState(CharacterStatus _status);
    public void AIPerception(CharacterStatus _status);
    public void AIIdle(CharacterStatus _status);
    public void AIChase(CharacterStatus _status);
    public void AIAttack(CharacterStatus _status);

    public void AIUseSkill(CharacterStatus _status);
    public IEnumerator AIDied(CharacterStatus _status);
}
