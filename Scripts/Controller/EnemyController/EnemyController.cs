using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{ 
    [SerializeField] private Status altar = null;
    [SerializeField] protected EnemyStatus enemyStatus = null;
    [SerializeField] private TargetingBoxController targetingBoxController = null;
    [SerializeField] private bool isPlayerTargeting = false;
    public override void Awake()
    {
        base.Awake();
        enemyStatus = GetComponent<EnemyStatus>();
    }
    public override void Start()
    {
        FindAltar();
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if (enemyStatus.AllyTarget != null)
            destination = enemyStatus.AllyTarget.transform.position;
        else
            destination = altar.TargetPos.position;

    }

    public void FindAltar()
    {
        enemyStatus.AltarRay = Physics2D.CircleCast(enemyStatus.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Altar"));
        altar = enemyStatus.AltarRay.rigidbody.GetComponent<Status>();
    }

    public override void AIChangeState()
    {
        if (enemyStatus.AllyTarget)
        {
            enemyStatus.Distance = enemyStatus.AllyTarget.transform.position - enemyStatus.TargetPos.position;
        } 
        else
        {
            enemyStatus.Distance = altar.transform.position - enemyStatus.TargetPos.position;
        }
        enemyStatus.TargetDir = enemyStatus.Distance.normalized;
        if (enemyStatus.IsDied)
        {
            StartCoroutine(AIDied());
        }
        if (!enemyStatus.IsDied)
        {
            if (enemyStatus.AllyTarget == null)
            {
                enemyStatus.AIState = EAIState.Chase;
            }
            else
            {
                if (IsSkillQueue() && IsSkillRange(enemyStatus.AllyTarget))
                    enemyStatus.AIState = EAIState.UseSkill;
                else if (IsAtkRange(enemyStatus.AllyTarget))
                    enemyStatus.AIState = EAIState.Attack;
                else
                    enemyStatus.AIState = EAIState.Chase;
            }

        }
    }

    public void Targeting(List<AllyController> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, enemyStatus.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                AllyController _hitStatus = _hit[i].collider.GetComponent<AllyController>();
                if (!_hitStatus.IsEnemyTargeted[enemyStatus.EnemyIndex])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsEnemyTargeted[enemyStatus.EnemyIndex] = true;
                }
            }
        }
    }
    public void Targeting(List<EnemyController> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, enemyStatus.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                EnemyController _hitStatus = _hit[i].collider.GetComponent<EnemyController>();
                if (!_hitStatus.IsEnemyTargeted[enemyStatus.EnemyIndex])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsEnemyTargeted[enemyStatus.EnemyIndex] = true;
                }
            }
        }
    }
    public void ResortTarget(List<AllyController> _targetList )
    {
        if (_targetList.Count > 0) 
        {
            enemyStatus.AllyTarget = _targetList[0];
            SortSightRayListByDistance(_targetList);
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (enemyStatus.GetDistance(_targetList[i].transform.position) >= enemyStatus.SeeRange
                    || _targetList[i].IsDied())
                {

                    if (_targetList[i] == enemyStatus.AllyTarget)
                    {
                        enemyStatus.AllyTarget = null;
                    }

                    _targetList[i].IsEnemyTargeted[enemyStatus.EnemyIndex] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }
        }
        else
        {
            enemyStatus.AllyTarget = null;
        }
    }
    public void ResortTarget(List<EnemyController> _targetList)
    {
        if (_targetList.Count > 0)
        {
            enemyStatus.EnemyTarget = _targetList[0];

            SortSightRayListByDistance(_targetList);
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (enemyStatus.GetDistance(_targetList[i].transform.position) >= enemyStatus.SeeRange
                    || _targetList[i].IsDied())
                {
                    if (_targetList[i] == enemyStatus.EnemyTarget)
                    {
                        enemyStatus.EnemyTarget = null;
                    }
                    _targetList[i].IsEnemyTargeted[enemyStatus.EnemyIndex] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }
        }
        else
        {
                enemyStatus.EnemyTarget = null;
        }
    }
    public override IEnumerator AIPerception()
    {
        while(true)
        {
            Targeting(enemyStatus.AllyRayList);
            ResortTarget(enemyStatus.AllyRayList);
        
            Targeting( enemyStatus.EnemyRayList);
            ResortTarget(enemyStatus.EnemyRayList);

            yield return new WaitForSeconds(0.5f);
        }
    }

    //public override void AIChase()
    //{
    //    if (pathFindController.FinalNodeList.Count > 1)
    //    {
    //        Vector2 _moveDir = new Vector2(pathFindController.FinalNodeList[1].x, pathFindController.FinalNodeList[1].y);
    //        characterStatus.ActiveLayer(LayerName.WalkLayer);
    //        characterStatus.transform.position = Vector2.MoveTowards(characterStatus.transform.position, _moveDir, characterStatus.TotalSpeed * Time.deltaTime);
    //    }
    //    else if (pathFindController.FinalNodeList.Count == 1)
    //    {
    //        pathFindController.FinalNodeList.RemoveAt(0);
    //    }
    //}
    public override void AttackDamage()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, enemyStatus.TotalStatus[(int)EStatus.AtkRange], enemyStatus.TargetDir, 1f, LayerMask.GetMask("Ally","Altar"));

        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Status ally = hits[i].collider.GetComponent<Status>();
                ally.Damaged(AttackTypeDamage());
                if (ally.IsLastHit())
                {
                    ally.IsDied = true;
                }
            }
        }
    }

    public void SetTargetingBox(bool _bool)
    {
        targetingBoxController.IsTargeting = _bool;
    }
}
