 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{ 
    [SerializeField] private Status altar = null;
    [SerializeField] protected EnemyStatus enemyStatus = null;

    public override void Awake()
    {
        base.Awake();
        enemyStatus = GetComponent<EnemyStatus>();
    }
    public override void Start()
    {
        base.Start();
        FindAltar();
    }
    public void FindAltar()
    {
        enemyStatus.AltarRay = Physics2D.CircleCast(enemyStatus.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Altar"));
        altar = enemyStatus.AltarRay.rigidbody.GetComponent<Status>();
    }

    public override void AIChangeState()
    {
        if (enemyStatus.Target)
        {
            enemyStatus.Distance = enemyStatus.Target.position - enemyStatus.TargetPos.position;
            enemyStatus.TargetDir = enemyStatus.Distance.normalized;
        } 
        
        if(!enemyStatus.IsDied)
        {
            if(enemyStatus.IsDied)
                StartCoroutine(AIDied());
            else
            {
                if (enemyStatus.Target == null)
                    enemyStatus.AIState = EAIState.Idle;
                else
                {
                    if(IsSkillQueue() && IsSkillRange())
                        enemyStatus.AIState = EAIState.UseSkill;
                    else if (IsAtkRange())
                        enemyStatus.AIState = EAIState.Attack;
                    else
                        enemyStatus.AIState = EAIState.Chase;
                }
            }
        }
    }

    public void Targeting(int _layer, List<Status> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, enemyStatus.SeeRange, Vector2.up, 0, _layer);
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                CharacterStatus _hitStatus = _hit[i].collider.GetComponent<CharacterStatus>();
                if (!_hitStatus.IsEnemyTargeted[enemyStatus.EnemyIndex])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsEnemyTargeted[enemyStatus.EnemyIndex] = true;
                }
            }
        }
    }
    public void ResortTarget(List<Status> _targetList, bool _isEnemy, Transform _defaultTransform = null)
    {
        if (_targetList.Count > 0)
        {
            if (_isEnemy)
                enemyStatus.Target = _targetList[0].TargetPos;
            else
                enemyStatus.AllyTarget = _targetList[0].TargetPos;
            SortSightRayList(_targetList);
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (enemyStatus.GetDistance(_targetList[i].TargetPos.position) >= enemyStatus.SeeRange
                    || _targetList[i].transform.GetComponent<CharacterStatus>().AIState == EAIState.Died)
                {
                    if (_isEnemy)
                    {

                        if (_targetList[i].TargetPos == enemyStatus.Target)
                        {
                            enemyStatus.Target = _defaultTransform;
                        }
                    }
                    else
                    {
                        if (_targetList[i].TargetPos == enemyStatus.AllyTarget)
                        {
                            enemyStatus.AllyTarget = _defaultTransform;
                        }
                    }
                    _targetList[i].transform.GetComponent<CharacterStatus>().IsEnemyTargeted[enemyStatus.EnemyIndex] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }
        }
        else
        {
            if (_isEnemy)
                enemyStatus.Target = _defaultTransform;
            else
                enemyStatus.AllyTarget = _defaultTransform;
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
    public override void AIPerception()
    {
        Targeting(LayerMask.GetMask("Ally"), enemyStatus.AllyRayList);
        if (!altar)
            enemyStatus.Target = null;
        else
            ResortTarget(enemyStatus.AllyRayList,true, altar.TargetPos);
        
        Targeting(LayerMask.GetMask("Enemy"), enemyStatus.EnemyRayList);
        ResortTarget(enemyStatus.EnemyRayList, false);
    }


    public override void AttackDamage()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, enemyStatus.TotalAtkRange, enemyStatus.TargetDir, 1f, LayerMask.GetMask("Ally","Altar"));

        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Status ally = hits[i].collider.GetComponent<Status>();
                if(ally == null)
                {
                    Debug.Log("뭐징?? 뭘 공격한걸까?" + hits[i].collider.name);
                }
                ally.Damaged(AttackTypeDamage());
            }
        }
    }
}
