using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIController
{
    public void AIChangeState();
    public void AIState();
    public IEnumerator AIPerception();
    public void AIIdle();
    public void AIChase();
    public void AIAttack();

    public void AIUseSkill();
    public IEnumerator AIDied();
}
