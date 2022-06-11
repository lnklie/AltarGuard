using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : MercenarySwordManAIController.cs
==============================
*/
public class MercenarySwordManAIController : MercenaryAIController
{
    public override void Attack()
    {
        base.Attack();
        if (!IsDelay())
        {
            ani.SetTrigger("AtkTrigger");
            SetDelayTime(0f);
            EnemyAttack(AttackRange());
        }
        else
            isAtk = false;
    }
    public void EnemyAttack(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            EnemyStatus enemy = hits[i].collider.GetComponent<EnemyStatus>();

            enemy.CurHp -= AttackTypeDamage();
            isAtk = true;
            if (IsLastHit(enemy))
            {
                Debug.Log("막타 경험치 확득");
                character.CurExp += enemy.DefeatExp;
            }
        }
    }

    public RaycastHit2D[] AttackRange()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, 1f, character.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, character.Dir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].rigidbody.GetComponent<AIController>().Damaged();
            }
        }
        else
            Debug.Log("아무것도 없음");
        return hits;
    }
}
