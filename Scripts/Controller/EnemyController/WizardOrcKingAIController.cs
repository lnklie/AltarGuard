using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardOrcKingAIController : BossEnemyAIController
{
    public override void Attack(EnemyStatus _status)
    {
        base.Attack(_status);

        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            _status.DelayTime = 0f;
            _status.IsAtk = true;
        }
        else
            _status.IsAtk = false;
    }
    public override int AttackTypeDamage(EnemyStatus _status)
    {
        return _status.MagicalDamage;
    }
}
