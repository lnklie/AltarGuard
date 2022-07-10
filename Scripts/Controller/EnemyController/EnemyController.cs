using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : EnemyAIController.cs
==============================
*/
public class EnemyController : CharacterController
{
    [SerializeField]
    private GameObject altar = null;
    protected EnemyStatus enemyStatus = null;

    public override void Awake()
    {
        base.Awake();
        enemyStatus = GetComponent<EnemyStatus>();
    }
    private void Start()
    {
        FindAltar(enemyStatus);
    }

    public void FindAltar(EnemyStatus _enemy)
    {
        _enemy.AltarRay = Physics2D.CircleCastAll(_enemy.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Ally"));
        for (int i = 0; i < _enemy.AltarRay.Length; i++)
        {
            if (_enemy.AltarRay[i].collider.gameObject.CompareTag("Altar"))
                altar = _enemy.AltarRay[i].collider.gameObject;
        }
    }
    public bool FrontOtherEnemy(RaycastHit2D _enemyHit, Status _enemy)
    {

        // 앞에 다른 적이 있는 지 확인

        if (_enemyHit.collider.gameObject != _enemy.gameObject)
        {
            return true;
        }
        else
            return false;
    }
    public bool IsAtkRange(CharacterStatus _status)
    {
        if (GetDistance(_status.transform.position, _status.Target.transform.position) < _status.AtkRange)
            return true;
        else
            return false;
    }




    public override void AIChangeState(CharacterStatus _status)
    {
        if (IsDied(_status))
        {
            SetState(_status, global::EAIState.Died);
        }
        else
        {
            if (_status.IsDamaged)
            {
                SetState(_status, global::EAIState.Damaged);
            }
            else
            {
                if (IsAtkRange(_status))
                {
                    SetState(_status, global::EAIState.Attack);
                }
                else
                {
                    if (_status.Target == this.gameObject || FrontOtherEnemy(_status.HitRay, _status))
                    {
                        SetState(_status, global::EAIState.Idle);
                    }
                    else
                    {
                        SetState(_status, global::EAIState.Walk);
                    }
                }
            }
        }
    }


    public override void AIPerception(CharacterStatus _status)
    {
        _status.SightRay.AddRange(Physics2D.CircleCastAll(_status.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally")));
        SortSightRayList(_status.SightRay);
        _status.HitRay = Physics2D.BoxCast(_status.transform.position, Vector2.one, 90f, _status.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(_status.transform.position, _status.Dir * 2f, Color.cyan);

        if (!altar)
            _status.Target = null;
        else
        {
            if (_status.SightRay[0])
                _status.Target = _status.SightRay[0].collider.gameObject;
            else
                _status.Target = altar;
        }
    }

    public override IEnumerator AIDied(CharacterStatus _status)
    {
        base.AIDied(_status);
        yield return new WaitForSeconds(2f);
        DropManager.Instance.DropItem(this.transform.position, enemyStatus.ItemDropKey, enemyStatus.ItemDropProb);
        StageManager.Instance.SpawnedEneies--;
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }
    public override RaycastHit2D[] AttackRange(CharacterStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, _status.AtkRange, _status.Dir, 1f, LayerMask.GetMask("Ally"));
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
    public override void AttackDamage(RaycastHit2D[] hits, CharacterStatus _status)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            Status ally = hits[i].collider.GetComponent<Status>();

            ally.CurHp -= ReviseDamage(AttackTypeDamage(_status), ally.DefensivePower);
        }
    }
    public override void AttackByAttackType(CharacterStatus _status)
    {
        if (!IsDelay(_status))
        {
            _status.Ani.SetTrigger("AtkTrigger");
            _status.DelayTime = 0f;
            _status.IsAtk = true;

            if (_status.AttackType == 0)
            {
                AttackDamage(AttackRange(_status), _status);
            }
            else if(_status.AttackType == 0.5f)
            {
                if (_status.Ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
                {
                    ShotArrow(_status);
                }
            }
            else
            {

                
            }
        }
        else
            _status.IsAtk = false;

    }
    public override void AIDamaged(CharacterStatus _status)
    {
        base.AIDamaged(_status);
        _status.IsStateChange = false;
        _status.StiffenTime = 0f;
        _status.IsDamaged = true;
    }

}
