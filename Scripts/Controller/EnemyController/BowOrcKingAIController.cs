using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowOrcKingAIController : BossEnemyAIController
{
    public override void Attack(EnemyStatus _status)
    {
        base.Attack(_status);
        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            Debug.Log("����");
            _status.DelayTime = 0f;
            if (_status.Ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
            {
                Debug.Log("����");
                ShotArrow(_status);
            }
        }
        else
            _status.IsAtk = false;


    }
    private void ShotArrow(EnemyStatus _status)
    {
        // Ȱ���
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(_status, AttackTypeDamage(_status));

        }
        else
            Debug.Log("ȭ�� ����");
    }
    public override int AttackTypeDamage(EnemyStatus _status)
    {
        return _status.PhysicalDamage;
    }
}
