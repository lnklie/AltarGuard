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
    private void Start()
    {
        FindAltar(rushEnemyStatus);
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
        base.Stiffen(_status);
        _status.IsKnuckBack = true;
        if (_status.StiffenTime >= 0.5f)
        {
            _status.IsKnuckBack = false;
            _status.IsDamaged = false;
            _status.StiffenTime = 0f;
        }
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
        base.Died(_status);
        StageManager.Instance.SpawnedEneies--;
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
        yield return null;
    }
}
