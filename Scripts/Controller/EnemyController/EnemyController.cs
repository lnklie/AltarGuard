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
    [SerializeField] private Status altar = null;
    [SerializeField] protected EnemyStatus enemyStatus = null;

    public override void Awake()
    {
        base.Awake();
        enemyStatus = GetComponent<EnemyStatus>();
        FindAltar(enemyStatus);
    }

    public void FindAltar(EnemyStatus _enemy)
    {
        _enemy.AltarRay = Physics2D.CircleCast(_enemy.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Altar"));
        altar = _enemy.AltarRay.rigidbody.GetComponent<Status>();
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


    public override void AIChangeState(CharacterStatus _status)
    {
        if (_status.Target)
        {
            _status.Distance = _status.Target.position - _status.TargetPos.position;
            _status.TargetDir = _status.Distance.normalized;
        } 
        
        if(!_status.IsDied)
        {
            if(_status.CurHp < 0f)
            {
                StartCoroutine(AIDied(_status));
            }
            else
            {
                if (_status.Target == null)
                {
                    _status.AIState = EAIState.Idle;
                }
                else
                {
                    _status.AIState = EAIState.Chase;
                    if (IsAtkRange(_status))
                    {
                        _status.AIState = EAIState.Attack;
                    }
                }
            }


        }
    }


    public override void AIPerception(CharacterStatus _status)
    {
        RaycastHit2D[] _allyHit = Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if(_allyHit.Length > 0)
        {
            for (int i = 0; i < _allyHit.Length; i++)
            {
                CharacterStatus _allyHitStatus = _allyHit[i].collider.GetComponent<CharacterStatus>();
                if (!_allyHitStatus.IsEnemyTargeted[((EnemyStatus)_status).EnemyIndex])
                {
                    _status.AllyRayList.Add(_allyHitStatus);
                    _allyHitStatus.IsEnemyTargeted[((EnemyStatus)_status).EnemyIndex] = true;
                }
            }
        }

        RaycastHit2D[] _enemyHit = Physics2D.CircleCastAll(this.transform.position, _status.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if(_enemyHit.Length > 0)
        {
            for(int i =0; i < _enemyHit.Length; i++)
            {
                EnemyStatus _enemyHitStatus = _enemyHit[i].collider.GetComponent<EnemyStatus>();
                if (_enemyHitStatus.EnemyIndex != ((EnemyStatus)_status).EnemyIndex && !_enemyHitStatus.IsEnemyTargeted[((EnemyStatus)_status).EnemyIndex])
                {
                    _status.EnemyRayList.Add(_enemyHitStatus);
                    _enemyHitStatus.IsEnemyTargeted[((EnemyStatus)_status).EnemyIndex] = true;
                }
            }
        }

        if (!altar)
            _status.Target = null;
        else
        {
            if (_status.AllyRayList.Count > 0)
            {
                _status.Target = _status.AllyRayList[0].TargetPos;
                SortSightRayList(_status.AllyRayList);
                for (int i = 0; i < _status.AllyRayList.Count; i++)
                {
                    if (GetDistance(this.transform.position, _status.AllyRayList[i].transform.position) >= _status.SeeRange
                        || _status.AllyRayList[i].transform.GetComponent<CharacterStatus>().AIState == EAIState.Died)
                    {
                        _status.AllyRayList[i].transform.GetComponent<CharacterStatus>().IsEnemyTargeted[((EnemyStatus)_status).EnemyIndex] = false;
                        _status.AllyRayList.Remove(_status.AllyRayList[i]);
                    }
                } 
            }
            else
            {
                _status.Target = altar.TargetPos;
            }
        }
        if (_status.EnemyRayList.Count > 0)
        {
            _status.AllyTarget = _status.EnemyRayList[0].TargetPos;
            SortSightRayList(_status.EnemyRayList);
            for (int i = 0; i < _status.EnemyRayList.Count; i++)
            {
                if (GetDistance(this.transform.position, _status.EnemyRayList[i].transform.position) >= _status.SeeRange
                    || _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().AIState == EAIState.Died)
                {
                    if (_status.EnemyRayList[i].TargetPos == _status.AllyTarget)
                    {
                        _status.AllyTarget = null;
                    }
                    _status.EnemyRayList[i].transform.GetComponent<EnemyStatus>().IsEnemyTargeted[((EnemyStatus)_status).EnemyIndex] = false;
                    _status.EnemyRayList.Remove(_status.EnemyRayList[i]);
                }
            }
        }
        else
        {
            _status.Target = null;
        }
    }

    public override IEnumerator AIDied(CharacterStatus _status)
    {
        yield return null;
    }


    public override void AttackDamage(CharacterStatus _status)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, _status.TotalAtkRange, _status.TargetDir, 1f, LayerMask.GetMask("Ally"));

        for (int i = 0; i < hits.Length; i++)
        {
            Status ally = hits[i].collider.GetComponent<Status>();
            ally.Damaged(AttackTypeDamage(_status));
            
        }
    }
}
