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
public class SlimeAIController : EnemyAIController
{
    public override void Attack(EnemyStatus _enemy)
    {
        //base.Attack(_enemy);
        //Status targetStatus = _enemy.collider.GetComponent<Status>();
        //if (!_enemy.IsDelay())
        //{
        //    _enemy.DelayTime = 0f;
        //    targetStatus.CurHp -= AttackTypeDamage(_enemy);
        //    targetStatus.IsDamaged = true;
        //}
    }
}
