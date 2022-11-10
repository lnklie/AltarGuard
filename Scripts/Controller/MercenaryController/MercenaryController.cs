using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryController : AllyController
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
        mercenary.CurHp = (int)mercenary.TotalStatus[(int)EStatus.MaxHp];
        mercenary.TriggerStatusUpdate = true;
        mercenary.AIState = EAIState.Idle;
    }


    public EAIState CheckBossState(CharacterStatus _Status)
    {
        return _Status.EnemyRayList[0].GetComponent<Rigidbody>().GetComponent<EnemyStatus>().AIState;
    }


    public override void AttackDamage()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, mercenary.TotalStatus[(int)EStatus.AtkRange], mercenary.TargetDir, 1f, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();
                mercenary.IsAtk = true;

                _enemy.Damaged(AttackTypeDamage());
                if(_enemy.IsLastHit())
                {
                    _enemy.IsDied= true;
                    _enemy.SetKilledAlly(mercenary);
                    mercenary.AquireExp(_enemy);

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
                else if (mercenary.GetDistance(mercenary.Target.transform.position) <= mercenary.TotalStatus[(int)EStatus.AtkRange])
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
    public override void Targeting(List<EnemyController> _targetList)
    {
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, mercenary.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                EnemyController _hitStatus = _hit[i].collider.GetComponent<EnemyController>();
                if (!_hitStatus.IsAllyTargeted[mercenary.AllyNum])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsAllyTargeted[mercenary.AllyNum] = true;
                }
            }

            for (int i = 0; i < _targetList.Count; i++)
            {
                if (mercenary.GetDistance(_targetList[i].transform.position) >= mercenary.SeeRange
                    || _targetList[i].IsDied())
                {

                    if (_targetList[i] == mercenary.Target)
                    {
                        mercenary.Target = null;
                    }
                    _targetList[i].IsAllyTargeted[mercenary.AllyNum] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }
        }
    }
    public override void Targeting(List<AllyController> _targetList)
    {
  
        RaycastHit2D[] _hit = Physics2D.CircleCastAll(this.transform.position, mercenary.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
        if (_hit.Length > 0)
        {
            for (int i = 0; i < _hit.Length; i++)
            {
                AllyController _hitStatus = _hit[i].collider.GetComponent<AllyController>();
                if (!_hitStatus.IsAllyTargeted[mercenary.AllyNum])
                {
                    _targetList.Add(_hitStatus);
                    _hitStatus.IsAllyTargeted[mercenary.AllyNum] = true;
                }
            }
        }
    }
    public override void ResortTarget(List<EnemyController> _targetList)
    {
        if (_targetList.Count > 0)
        {
            SortSightRayListByDistance(_targetList);
            mercenary.Target = _targetList[0];

            for (int i = 0; i < _targetList.Count; i++)
            {
                if (mercenary.GetDistance(_targetList[i].transform.position) >= mercenary.SeeRange
                    || _targetList[i].IsDied())
                {

                    if (_targetList[i] == mercenary.Target)
                    {
                        mercenary.Target = null;
                    }

                    _targetList[i].IsAllyTargeted[mercenary.AllyNum] = false;
                    _targetList.Remove(_targetList[i]);
                }
            }

        }
        else
        {
            mercenary.Target = null;
        }
    }
    public override void ResortTarget(List<AllyController> _targetList)
    {
        if (_targetList.Count > 0)
        {
            switch (mercenary.AllyTargetIndex)
            {
                case EAllyTargetingSetUp.OneSelf:
                    mercenary.Target = this;
                    break;
                case EAllyTargetingSetUp.CloseAlly:
                    SortSightRayListByDistance(_targetList);
                    mercenary.Target = _targetList[1];
                    break;
                case EAllyTargetingSetUp.Random:
                    mercenary.Target = ChooseSightRayListByRandom(_targetList);
                    break;
                case EAllyTargetingSetUp.WeakAlly:
                    SortSightRayListByCurHp(_targetList);
                    mercenary.Target = _targetList[0];
                    break;
            }

            mercenary.Target = _targetList[0];


        }
        else
        {

            mercenary.Target = null;
        }
    }

    public override IEnumerator AIPerception()
    {
        while(true)
        {
            Targeting(mercenary.EnemyRayList);
            Targeting(mercenary.AllyRayList);

            if(mercenary.Target == null)
                ResortTarget(mercenary.EnemyRayList);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public override IEnumerator AIDied()
    {
        mercenary.AIState = EAIState.Died;
        mercenary.IsDied = true;
        mercenary.ActiveLayer(ELayerName.DieLayer);
        mercenary.Rig.velocity = Vector2.zero;
        mercenary.Col.enabled = false;
        yield return new WaitForSeconds(mercenary.RevivalTime);
        Rivive();
        mercenary.IsDied = false;
    }

}
