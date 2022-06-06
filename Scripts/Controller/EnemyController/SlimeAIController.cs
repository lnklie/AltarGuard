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
    public override void Attack()
    {
        base.Attack();
        BaseController targetPlayer = atkRange.collider.GetComponent<BaseController>();
        Status targetStatus = atkRange.collider.GetComponent<Status>();
        if (!IsDelay())
        {
            SetDelayTime(0f);
            targetStatus.CurHp -= enemy.Damage;
            targetPlayer.IsDamaged = true;
        }
    }
}
