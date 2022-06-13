using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : SlimeAIController.cs
==============================
*/
public class SlimeAIController : RushEnemyAIController
{
    public override void Attack(EnemyStatus _enemy)
    {
        base.Attack(_enemy);
        Status targetStatus = _enemy.AtkRangeRay.collider.GetComponent<Status>();
        if (!_enemy.IsDelay())
        {
            _enemy.DelayTime = 0f;
            targetStatus.CurHp -= _enemy.Damage;
            targetStatus.IsDamaged = true;
        }
    }
}
