using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : BossEnemyAIController.cs
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
            if (_status.IsDamaged)
            {
                SetState(_status, EnemyState.Damaged);
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
    }


    public override void AnimationDirection(EnemyStatus _enemy)
    {
        // 애니메이션 방향
        if (_enemy.Dir.x > 0) this.transform.localScale = new Vector3(-6, 6, 1);
        else if (_enemy.Dir.x < 0) transform.transform.localScale = new Vector3(6, 6, 1);
    }
    public override IEnumerator Died(EnemyStatus _status)
    {
        _status.IsStateChange = false;
        SetEnabled(_status, false);
        _status.Rig.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        DropManager.Instance.DropItem(this.transform.position, _status.ItemDropKey, _status.ItemDropProb);
        EnemySpawner.Instance.ReturnBossEnemy(this.gameObject);
    }
}
