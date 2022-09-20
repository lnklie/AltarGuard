using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryController : CharacterController
{
    protected MercenaryStatus mercenary = null;
    [SerializeField] private GameObject rivivePoint = null;

    public override  void Awake()
    {
        base.Awake();
        mercenary = this.GetComponent<MercenaryStatus>();
    }

    private void Rivive()
    {
        this.gameObject.transform.position = rivivePoint.transform.position;

        mercenary.Rig.isKinematic = false;
        mercenary.Col.enabled = true;
        mercenary.CurHp = mercenary.TotalMaxHp;
        mercenary.TriggerStatusUpdate = true;
        mercenary.AIState = EAIState.Idle;
    }

    public bool IsLastHit(CharacterStatus _status)
    {
        if (_status.CurHp <= 0f)
            return true;
        else
            return false;
    }

    public EAIState CheckBossState(CharacterStatus _Status)
    {
        return _Status.EnemyRayList[0].GetComponent<Rigidbody>().GetComponent<EnemyStatus>().AIState;
    }


    public override void AttackDamage()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, mercenary.TotalAtkRange, mercenary.TargetDir, 1f, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();
                mercenary.IsAtk = true;

                _enemy.Damaged(AttackTypeDamage());
                if(_enemy.IsLastHit())
                {
                    mercenary.AquireExp(_enemy);

                    bool[] _isDrops = _enemy.RandomChoose(_enemy.ItemDropProb, mercenary.TotalDropProbability);
                    for (int j = 0; j < 5; j++)
                    {
                        if (_isDrops[j])
                        {
                            InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(_enemy.ItemDropKey[j]));
                        }
                    }
                }
            }
        }
    }




    public override void AIChangeState()
    {
        if (mercenary.Target)
        {
            mercenary.Distance = mercenary.Target.transform.position - mercenary.TargetPos.position;
            mercenary.TargetDir = mercenary.Distance.normalized;
        }
        if (mercenary.CurHp < 0f && !mercenary.IsDied)
        {
            StartCoroutine(AIDied());
        }

        if(!mercenary.IsDied)
        {
            if (mercenary.Target == null)
            {

                if (pathFindController.FinalNodeList.Count == 0)
                    mercenary.AIState = EAIState.Idle;
                else
                    mercenary.AIState = EAIState.Chase;
            }
            else
            {

                if (skillController.SkillQueue.Count > 0 && !skillController.IsSkillDelay &&
                    mercenary.GetDistance(mercenary.Target.transform.position) <= skillController.SkillQueue[0].skillRange)
                {
                    mercenary.AIState = EAIState.UseSkill;
                }
                else if (mercenary.GetDistance(mercenary.Target.transform.position) <= mercenary.TotalAtkRange)
                {
                    mercenary.AIState = EAIState.Attack;
                }
                else
                {
                    mercenary.AIState = EAIState.Chase;
                }
            }

        }
    }
    public bool Targeting(int _layer, List<Status> _targetList)
    {
        bool _bool = false;
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, mercenary.SeeRange, Vector2.up, 0, _layer);
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                CharacterStatus _hitStatus = _hit[i].collider.GetComponent<CharacterStatus>();
                if (!_hitStatus.IsAllyTargeted[mercenary.AllyNum])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsAllyTargeted[mercenary.AllyNum] = true;
                }
            }
            _bool = true;
        }
        return _bool;
    }
    public void ResortTarget(List<Status> _targetList,bool _isEnemy, Transform _defaultTransform = null)
    {
        if (_targetList.Count > 0)
        {
            SortSightRayList(_targetList);
            if (_isEnemy)
                mercenary.Target = _targetList[0].TargetPos;
            else
                mercenary.AllyTarget = _targetList[0].TargetPos;
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (mercenary.GetDistance(_targetList[i].transform.position) >= mercenary.SeeRange
                    || _targetList[i].transform.GetComponent<CharacterStatus>().AIState == EAIState.Died)
                {
                    if (_isEnemy)
                    {

                        if (_targetList[i].TargetPos == mercenary.Target)
                        {
                            mercenary.Target = _defaultTransform;
                        }
                    }
                    else
                    {
                        if (_targetList[i].TargetPos == mercenary.AllyTarget)
                        {
                            mercenary.AllyTarget = _defaultTransform;
                        }
                    }
                    _targetList[i].transform.GetComponent<CharacterStatus>().IsAllyTargeted[mercenary.AllyNum] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }

        }
        else
        {
            if (_isEnemy)
                mercenary.Target = _defaultTransform;
            else
                mercenary.AllyTarget = _defaultTransform;
        }
    }


    public override void AIPerception()
    {
        if(Targeting(LayerMask.GetMask("Enemy"), mercenary.EnemyRayList))
            ResortTarget(mercenary.EnemyRayList, true);

        if(Targeting(LayerMask.GetMask("Ally"), mercenary.AllyRayList))
        {
            ResortTarget(mercenary.AllyRayList, false);
            //mercenary.AllyTarget = mercenary.AllyRayList[0].TargetPos;
        }
    }

    public override IEnumerator AIDied()
    {
        mercenary.AIState = EAIState.Died;
        mercenary.IsDied = true;
        mercenary.ActiveLayer(LayerName.DieLayer);
        mercenary.Rig.velocity = Vector2.zero;
        mercenary.Col.enabled = false;
        yield return new WaitForSeconds(mercenary.RevivalTime);
        Rivive();
        mercenary.IsDied = false;
    }

}
