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
public class EnemyController : BaseController, IAIController
{
    [SerializeField]
    private GameObject altar = null;
    protected EnemyStatus enemyStatus = null;

    private void Awake()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }

    public virtual void Stiffen(CharacterStatus _status)
    {
        _status.StiffenTime += Time.deltaTime;
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

    public void SetEnabled(CharacterStatus _status, bool _bool)
    {
        _status.Col.enabled = _bool;
    }
    public virtual void AnimationDirection(CharacterStatus _status)
    {
        // 애니메이션 방향
        if (_status.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else if (_status.Dir.x < 0) transform.transform.localScale = new Vector3(1, 1, 1);
    }


    public virtual void ChangeState(CharacterStatus _status)
    {
        if (IsDied(_status))
        {
            SetState(_status, AIState.Died);
        }
        else
        {
            if (_status.IsDamaged)
            {
                SetState(_status, AIState.Damaged);
            }
            else
            {
                if (IsAtkRange(_status))
                {
                    SetState(_status, AIState.Attack);
                }
                else
                {
                    if (_status.Target == this.gameObject || FrontOtherEnemy(enemyStatus.HitRay, _status))
                    {
                        SetState(_status, AIState.Idle);
                    }
                    else
                    {
                        SetState(_status, AIState.Walk);
                    }
                }
            }
        }
    }
    public bool IsDelay(CharacterStatus _status)
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
    public virtual void State(CharacterStatus _status)
    {
        _status.Distance = _status.Target.transform.position - this.transform.position;
        _status.Dir = _status.Distance.normalized;
        switch (_status.AIState)
        {
            case AIState.Idle:
                Idle(_status);
                break;
            case AIState.Walk:
                Chase(_status);
                break;
            case AIState.Damaged:
                Stiffen(_status);
                break;
            case AIState.Attack:
                Attack(_status);
                break;
            case AIState.Died:
                StartCoroutine(Died(_status));
                break;
        }
    }

    public virtual void Perception(CharacterStatus _status)
    {
        AnimationDirection(_status);
        _status.SightRay = Physics2D.CircleCast(_status.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        _status.HitRay = Physics2D.BoxCast(_status.transform.position, Vector2.one, 90f, _status.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(_status.transform.position, _status.Dir * 2f, Color.cyan);

        if (!altar)
            _status.Target = null;
        else
        {
            if (_status.SightRay)
                _status.Target = _status.SightRay.collider.gameObject;
            else
                _status.Target = altar;
        }
    }

    public virtual void Idle(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.IdleLayer);
        _status.IsStateChange = false;
        _status.Rig.velocity = Vector2.zero;
    }

    public virtual void Chase(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.WalkLayer);
        _status.IsStateChange = false;
        _status.Rig.velocity = _status.Speed * _status.Dir;
    }

    public virtual void Attack(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.AttackLayer);
        _status.Ani.SetFloat("AtkType", _status.AttackType);
        _status.Rig.velocity = Vector2.zero;
        _status.DelayTime += Time.deltaTime;
        AttackByAttackType(_status);
    }

    public virtual IEnumerator Died(CharacterStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.IdleLayer);
        _status.IsStateChange = false;
        _status.Rig.velocity = Vector2.zero;
        SetEnabled(_status, false);
        yield return new WaitForSeconds(2f);
        DropManager.Instance.DropItem(this.transform.position, enemyStatus.ItemDropKey, enemyStatus.ItemDropProb);
        StageManager.Instance.SpawnedEneies--;
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }
    public virtual void AttackByAttackType(CharacterStatus _status)
    {

    }

    public virtual void Damaged(CharacterStatus _status)
    {
        _status.IsStateChange = false;
        _status.StiffenTime = 0f;
        _status.IsDamaged = true;
    }

    public bool IsDied(CharacterStatus _status)
    {
        if (_status.CurHp <= 0)
            return true;
        else
            return false;
    }

    public virtual int AttackTypeDamage(CharacterStatus _status)
    {
        return 0;
    }
}
