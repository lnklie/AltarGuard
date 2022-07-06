using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardOrcKingAIController : BossEnemyController
{
    public override void AttackByAttackType(CharacterStatus _status)
    {
        base.Attack(_status);

        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            _status.DelayTime = 0f;
            enemyStatus.IsAtk = true;
        }
        else
            enemyStatus.IsAtk = false;
    }
    public override int AttackTypeDamage(CharacterStatus _status)
    {
        return _status.MagicalDamage;
    }
}
