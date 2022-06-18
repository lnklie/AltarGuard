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
    private RushEnemyStatus rushEnemyStatus = null;
    private void Awake()
    {
        rushEnemyStatus = GetComponent<RushEnemyStatus>();
    }

    private void Update()
    {
        Perception(rushEnemyStatus);
        ChangeState(rushEnemyStatus);
        State(rushEnemyStatus);
    }

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
                if (IsAtkRange(_status))
                {
                    SetState(_status, EnemyState.Attack);
                }
                else
                {
                    if (_status.Target == this.gameObject || FrontOtherEnemy(_status.EnemyHitRay,_status))
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
            _status.IsDamaged = false;
        }
    }

    public bool FrontOtherEnemy(RaycastHit2D _enemyHit,Status _enemy)
    {

        // 앞에 다른 적이 있는 지 확인

        if (_enemyHit.collider.gameObject != _enemy.gameObject)
        {
            return true;
        }
        else
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
}
