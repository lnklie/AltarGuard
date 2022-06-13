using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-13
 * �ۼ��� : Inklie
 * ���ϸ� : RushEnemyAIController.cs
==============================
*/
public class RushEnemyAIController : EnemyAIController
{
    public override void ChangeState( EnemyStatus _status)
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
                        Debug.Log("������");
                    }
                    else
                    {
                        SetState(_status, EnemyState.Chase);
                        Debug.Log("�޷���");
                    }
                }
            }
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
        
        // �տ� �ٸ� ���� �ִ� �� Ȯ��
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
        // �˹� ȿ��
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
