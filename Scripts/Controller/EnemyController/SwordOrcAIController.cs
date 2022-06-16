using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOrcAIController : RushEnemyAIController
{
    public override void Attack(EnemyStatus _status)
    {
        base.Attack(_status);

        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            _status.DelayTime = 0f;
            EnemyAttack(AttackRange(_status), _status);
        }
        else
            _status.IsAtk = false;
    }
    public RaycastHit2D[] AttackRange(EnemyStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, 1f, _status.Dir, 1f, LayerMask.GetMask("Ally"));
        Debug.DrawRay(this.transform.position, _status.Dir, Color.red, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].rigidbody.GetComponent<Status>().IsDamaged = true;
            }
        }
        else
            Debug.Log("아무것도 없음");
        return hits;
    }
    public void EnemyAttack(RaycastHit2D[] hits, EnemyStatus _status)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            Status ally = hits[i].collider.GetComponent<Status>();

            ally.CurHp -= AttackTypeDamage(_status);
            _status.IsAtk = true;
        }
    }
    public override int AttackTypeDamage(EnemyStatus _status)
    {
        return _status.PhysicalDamage;
    }
}
