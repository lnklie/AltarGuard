using System.Collections;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : RushEnemyAIController.cs
==============================
*/
public class RushEnemyAIController : EnemyAIController
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
                    SetState(_status, EnemyState.Attack);
                }
                else
                {
                    if (_status.Target == this.gameObject)
                    {
                        SetState(_status, EnemyState.Idle);
                    }
                    else
                    {
                        SetState(_status, EnemyState.Chase);
                    }
                }
            }
        }
    }

    public bool IsDelay(EnemyStatus _status)
    {
        if (_status.DelayTime >= _status.AtkSpeed)
        {
            _status.DelayTime = _status.AtkSpeed;
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void Stiffen(EnemyStatus _status)
    {
        _status.StiffenTime += Time.deltaTime;
        _status.IsKnuckBack = true;
        if (_status.StiffenTime >= _status.MaxStiffenTime)
        {
            _status.IsKnuckBack = false;
            _status.DmgCombo = 0;
            _status.IsDamaged = false;
        }
    }

    public override void Perception(EnemyStatus _enemy)
    {
        base.Perception(_enemy);
        _enemy.EnemyHitRay = Physics2D.RaycastAll(_enemy.transform.position, _enemy.Dir, 0.5f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(_enemy.transform.position, _enemy.Dir, Color.blue);

    }
    public bool FrontOtherEnemy(RaycastHit2D[] _enemyHit,Status _enemy)
    {
        
        // 앞에 다른 적이 있는 지 확인
        for (int i = 0; i < _enemyHit.Length; i++)
        {
            if (_enemyHit[i].collider.gameObject != _enemy.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator Knockback(float _knockbackDuration, float _knockbackPower, Transform _obj, Rigidbody2D _rig)
    {
        // 넉백 효과
        float timer = 0;

        while(_knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (_obj.transform.position - this.transform.position).normalized;
            _rig.AddForce(-direction * _knockbackPower);
        }

        yield return 0;
    }
    public override IEnumerator Died(EnemyStatus _status)
    {
        _status.IsStateChange = false;
        SetEnabled(_status, false);
        _status.Rig.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        DropManager.Instance.DropItem(this.transform.position, _status.ItemDropKey, _status.ItemDropProb);
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }
}
