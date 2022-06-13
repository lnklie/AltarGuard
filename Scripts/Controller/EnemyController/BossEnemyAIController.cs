using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : BossEnemyAIController.cs
==============================
*/
public class BossEnemyAIController : EnemyAIController
{

    public override void ChangeState(EnemyStatus _status)
    {
        if (IsDied(_status))
        {
            SetState(_status, EnemyState.Died);
        }
        else
        {
            if (_status.AtkRangeRay)
            {
                if (!_status.IsDelay()) 
                {
                    SetState(_status, EnemyState.Attack);
                }
                else
                    SetState(_status, EnemyState.Idle);
            }
            else
            {
                if (_status.Target == this.gameObject)
                {
                    SetState(_status, EnemyState.Idle);
                }
                else
                    SetState(_status, EnemyState.Chase);
            }
            
        }
    }


    public override void AnimationDirection(EnemyStatus _enemy)
    {
        // �ִϸ��̼� ����
        if (_enemy.Dir.x > 0) this.transform.localScale = new Vector3(-6, 6, 1);
        else if (_enemy.Dir.x < 0) transform.transform.localScale = new Vector3(6, 6, 1);
    } 
}
