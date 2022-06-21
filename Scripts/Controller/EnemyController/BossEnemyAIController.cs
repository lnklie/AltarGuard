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
    private BossEnemyStatus bossEnemyStatus = null;
    private void Awake()
    {
        bossEnemyStatus = GetComponent<BossEnemyStatus>();
    }
    private void Start()
    {
        FindAltar(bossEnemyStatus);
    }
    private void Update()
    {
        Perception(bossEnemyStatus);
        ChangeState(bossEnemyStatus);
        State(bossEnemyStatus);
    }
    //public override void ChangeState(EnemyStatus _status)
    //{
    //    if (IsDied(_status))
    //    {
    //        SetState(_status, EnemyState.Died);
    //    }
    //    else
    //    {
    //        if (_status.IsDamaged)
    //        {
    //            SetState(_status, EnemyState.Damaged);
    //        }
    //        else
    //        {
    //            if (IsAtkRange(_status))
    //            {
    //                if (!_status.IsDelay()) 
    //                {
    //                    SetState(_status, EnemyState.Attack);
    //                }
    //                else
    //                    SetState(_status, EnemyState.Idle);
    //            }
    //            else
    //            {
    //                if (_status.Target == this.gameObject)
    //                {
    //                    SetState(_status, EnemyState.Idle);
    //                }
    //                else
    //                    SetState(_status, EnemyState.Chase);
    //            }
    //        }
            
    //    }
    //}
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
                    if (_status.Target == this.gameObject || FrontOtherEnemy(_status.EnemyHitRay, _status))
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
        UIManager.Instance.UpdateBossInfo();
        Debug.Log("�´� ��");
        base.Stiffen(_status);
        if (_status.StiffenTime >= 0.5f)
        {
            Debug.Log("���� ����");
            _status.IsDamaged = false;
            _status.StiffenTime = 0f;
        }
    }
    public override void AnimationDirection(EnemyStatus _enemy)
    {
        // �ִϸ��̼� ����
        if (_enemy.Dir.x > 0) this.transform.localScale = new Vector3(-6, 6, 1);
        else if (_enemy.Dir.x < 0) transform.transform.localScale = new Vector3(6, 6, 1);
    }
    public override IEnumerator Died(EnemyStatus _status)
    {
        base.Died(_status);
        Debug.Log("����");
        UIManager.Instance.UpdateBossInfo();
        EnemySpawner.Instance.ReturnBossEnemy(this.gameObject);
        yield return null;
    }
}
