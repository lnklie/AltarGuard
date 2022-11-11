using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryController : AllyController
{
    protected MercenaryStatus mercenary = null;
    [SerializeField] private GameObject rivivePoint = null;

    public override void Awake()
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

        if (mercenary.CurHp < 0f && !mercenary.IsDied)
        {
            stateMachine.SetState(stateDic[EAIState.Died]);
        }

        if(!mercenary.IsDied)
        {
            if (mercenary.Target == null)
            {

                if (pathFindController.startPos == pathFindController.targetPos)
                    stateMachine.SetState(stateDic[EAIState.Idle]);
                else
                    stateMachine.SetState(stateDic[EAIState.Chase]);
            }
            else
            {

                if (IsSkillQueue() && !skillController.IsSkillDelay && IsSkillRange(mercenary.Target))
                {
                    stateMachine.SetState(stateDic[EAIState.UseSkill]);
                }
                else if (IsAtkRange(mercenary.Target))
                {
                    stateMachine.SetState(stateDic[EAIState.Attack]);
                }
                else
                {
                    stateMachine.SetState(stateDic[EAIState.Chase]);
                }
            }

        }
    }

    public override void Targeting(int _layer)
    {
  
        RaycastHit2D _hit = Physics2D.CircleCast(this.transform.position, mercenary.SeeRange, Vector2.up, 0, _layer);
        if (_hit)
        {

            CharacterStatus _hitStatus = _hit.collider.GetComponent<CharacterStatus>();
            mercenary.Target = _hitStatus;

        }
    }

    //public override void ResortTarget(List<AllyStatus> _targetList)
    //{
    //    if (_targetList.Count > 0)
    //    {
    //        switch (mercenary.AllyTargetIndex)
    //        {
    //            case EAllyTargetingSetUp.OneSelf:
    //                mercenary.Target = mercenary;
    //                break;
    //            case EAllyTargetingSetUp.CloseAlly:
    //                SortSightRayListByDistance(_targetList);
    //                mercenary.Target = _targetList[1];
    //                break;
    //            case EAllyTargetingSetUp.Random:
    //                mercenary.Target = ChooseSightRayListByRandom(_targetList);
    //                break;
    //            case EAllyTargetingSetUp.WeakAlly:
    //                SortSightRayListByCurHp(_targetList);
    //                mercenary.Target = _targetList[0];
    //                break;
    //        }

    //        mercenary.Target = _targetList[0];


    //    }
    //    else
    //    {

    //        mercenary.Target = null;
    //    }
    //}

    public override IEnumerator AIPerception()
    {
        while(true)
        {
            if(mercenary.Target == null)
                Targeting(LayerMask.GetMask("Enemy"));
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
