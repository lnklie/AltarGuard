using System.Collections;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : MercenaryController.cs
==============================
*/
public class MercenaryController : CharacterController
{
    protected MercenaryStatus mercenary = null;


    public override  void Awake()
    {
        base.Awake();
        mercenary = this.GetComponent<MercenaryStatus>();

    }

    private void Rivive(CharacterStatus _Status)
    {
        _Status.Rig.isKinematic = false;
        _Status.Col.enabled = true;
        _Status.CurHp = mercenary.MaxHp;
        _Status.AIState = global::EAIState.Idle;
    }

    public bool IsLastHit(EnemyStatus _enemy, CharacterStatus _Status)
    {
        if (_Status.IsAtk == true && _enemy.CurHp <= 0f)
            return true;
        else
            return false;
    }

    public EAIState CheckBossState(CharacterStatus _Status)
    {
        return _Status.SightRayList[0].GetComponent<Rigidbody>().GetComponent<EnemyStatus>().AIState;
    }


    public override void AttackDamage(CharacterStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position,_status.AtkRange, _status.TargetDir, 1f, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Status _enemy = hits[i].collider.GetComponent<Status>();
                _status.IsAtk = true;

                _enemy.Damaged(AttackTypeDamage(_status));
                _status.AquireExp(_enemy);
            }
        }
    }




    public override void AIChangeState(CharacterStatus _status)
    {
        if (_status.Target)
        {
            _status.Distance = _status.Target.transform.position - this.transform.position;
            _status.TargetDir = _status.Distance.normalized;
        }
        if (_status.CurHp < 0f)
        {
            _status.AIState = EAIState.Died;
        }
        else
        {
            if (_status.Target == null)
            {
                _status.AIState = EAIState.Idle;
            }
            else
            {
                _status.AIState = EAIState.Walk;
                if (GetDistance(this.transform.position, _status.Target.transform.position) <= _status.AtkRange)
                {
                    _status.AIState = EAIState.Attack;
                }
            }

        }
    }



    public override void AIPerception(CharacterStatus _status)
    {
        RaycastHit2D _enemyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        CharacterStatus _enemyHitStatus = _enemyHit.collider.GetComponent<CharacterStatus>();
        if (_enemyHit && !CheckRayList(_enemyHitStatus, _status.SightRayList))
            _status.SightRayList.Add(_enemyHitStatus);

        SortSightRayList(_status.SightRayList);
        RaycastHit2D _allyHit = Physics2D.CircleCast(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        CharacterStatus _allyHitStatus = _allyHit.collider.GetComponent<CharacterStatus>();
        if (_allyHit && !CheckRayList(_allyHitStatus, _status.AllyRayList))
            _status.AllyRayList.Add(_allyHitStatus);


        if (_status.SightRayList.Count > 0)
        {
            _status.Target = _status.SightRayList[0].TargetPos;
        }

        for (int i = 0; i < _status.SightRayList.Count; i++)
        {
            if (GetDistance(this.transform.position, _status.SightRayList[i].transform.position) >= _status.SeeRange)
            {
                if (_status.Target == _status.SightRayList[i])
                    _status.Target = null;
                _status.SightRayList.Remove(_status.SightRayList[i]);
            }
        }
    }




    public override IEnumerator AIDied(CharacterStatus _status)
    {
        base.AIDied(_status);
        yield return new WaitForSeconds(mercenary.RevivalTime);
        Rivive(_status);
    }

}
