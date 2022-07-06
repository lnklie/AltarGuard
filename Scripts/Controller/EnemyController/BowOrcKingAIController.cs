using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowOrcKingAIController : BossEnemyController
{
    public override void AttackByAttackType(CharacterStatus _status)
    {
        base.Attack(_status);
        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            Debug.Log("공격");
            _status.DelayTime = 0f;
            if (_status.Ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
            {
                Debug.Log("쐈다");
                ShotArrow(_status);
            }
        }
        else
            enemyStatus.IsAtk = false;


    }
    private void ShotArrow(CharacterStatus _status)
    {
        // 활쏘기
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(_status, AttackTypeDamage(_status));

        }
        else
            Debug.Log("화살 없음");
    }
    public override int AttackTypeDamage(CharacterStatus _status)
    {
        return _status.PhysicalDamage;
    }
}
