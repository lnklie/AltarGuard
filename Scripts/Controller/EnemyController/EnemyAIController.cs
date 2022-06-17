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
public class EnemyAIController : AIController
{
    
    public override void ChangeState(EnemyStatus _status)
    {}

    public override void State(EnemyStatus _status)
    {
        _status.Distance = _status.Target.transform.position - this.transform.position;
        _status.Dir = _status.Distance.normalized;
        switch (_status.EnemyState)
        {
            case EnemyState.Idle:
                Idle(_status);
                break;
            case EnemyState.Chase:
                Chase(_status);
                break;
            case EnemyState.Damaged:
                Stiffen(_status);
                break;
            case EnemyState.Attack:
                Attack(_status);
                break;
            case EnemyState.Died:
                StartCoroutine(Died(_status));
                break;
        }
    }
    public override void Chase(EnemyStatus _status)
    {
        _status.IsStateChange = false;
        ActiveLayer(_status.Ani, LayerName.WalkLayer);
        _status.Rig.velocity = _status.Speed * _status.Dir;
    }

    public override void Damaged(EnemyStatus _status)
    {
        _status.IsStateChange = false;
        _status.StiffenTime = 0f;
        _status.IsDamaged = true;
    }

    public override IEnumerator Died(EnemyStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.IdleLayer);
        _status.IsStateChange = false;
        SetEnabled(_status, false);
        _status.Rig.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        DropManager.Instance.DropItem(this.transform.position, _status.ItemDropKey, _status.ItemDropProb);
        EnemySpawner.Instance.ReturnEnemy(this.gameObject);
    }

    public override void Idle(EnemyStatus _status)
    {
        _status.IsStateChange = false;
        ActiveLayer(_status.Ani, LayerName.IdleLayer);
        _status.Rig.velocity = Vector2.zero;
    }


    public virtual void Stiffen(EnemyStatus _status)
    {
        float _stiffenTime = 0f;
        _stiffenTime += Time.deltaTime;
        if (_stiffenTime >= 1f)
        {
            _status.IsDamaged = false;
        }
    }
    public override void Perception(EnemyStatus _enemy)
    {
        // 레이를 이용한 인식
        AnimationDirection(_enemy);
        _enemy.SightRay = Physics2D.CircleCast(_enemy.transform.position, _enemy.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        //_enemy.AtkRangeRay = Physics2D.CircleCast(_enemy.transform.position, _enemy.AtkRange, _enemy.Dir, 0, LayerMask.GetMask("Ally"));
        _enemy.AltarRay = Physics2D.CircleCastAll(_enemy.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Ally"));
        _enemy.EnemyHitRay = Physics2D.BoxCast(_enemy.transform.position,Vector2.one, 90f,_enemy.Dir, 1f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(_enemy.transform.position, _enemy.Dir*2f,Color.cyan);
        GameObject altar = null;
        for(int i =0; i < _enemy.AltarRay.Length; i++)
        {
            if (_enemy.AltarRay[i].collider.gameObject.CompareTag("Altar"))
                altar = _enemy.AltarRay[i].collider.gameObject;
        }

        if (!altar)
            _enemy.Target = null;
        else
        {
            if (_enemy.SightRay)
                _enemy.Target = _enemy.SightRay.collider.gameObject;
            else
                _enemy.Target = altar;
        }
    }
    public bool IsAtkRange(EnemyStatus _status)
    {
        if (GetDistance(_status.transform.position, _status.Target.transform.position) < _status.AtkRange)
            return true;
        else
            return false;
    }
    public override bool IsDied(EnemyStatus _status)
    {
        if (_status.CurHp <= 0)
            return true;
        else
            return false;
    }
    public override void Attack(EnemyStatus _status)
    {
        ActiveLayer(_status.Ani, LayerName.AttackLayer);
        _status.Ani.SetFloat("AtkType", attackType);
        _status.Rig.velocity = Vector2.zero;
        _status.DelayTime += Time.deltaTime;
    }
    public void SetEnabled(EnemyStatus _status, bool _bool)
    {
        _status.Col.enabled = _bool;
    }
    public virtual void AnimationDirection(EnemyStatus _status)
    {
        // 애니메이션 방향
        if (_status.Dir.x > 0) this.transform.localScale = new Vector3(-1, 1, 1);
        else if (_status.Dir.x < 0) transform.transform.localScale = new Vector3(1, 1, 1);
    }

    public override int AttackTypeDamage(EnemyStatus _status)
    {
        return 0;
    }
}
