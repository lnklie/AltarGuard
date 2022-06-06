using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : SlimeAIController.cs
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
