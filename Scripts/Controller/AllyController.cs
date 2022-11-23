using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : CharacterController
{
    [SerializeField] private GameObject rivivePoint = null;

    protected Vector2 lookDir = Vector2.down;
    private AllyStatus ally = null;
    public override void Awake()
    {
        base.Awake();
        ally = this.GetComponent<AllyStatus>();
    }


    public override void Update()
    {
        base.Update();

        if (character.Target)
        {
            character.Distance = character.Target.transform.position - character.TargetPos.position;
            character.TargetDir = character.Distance.normalized;
            destination = character.Target.transform.position;
            CheckTarget();
        }
        else
            destination = character.Flag.transform.position;
    }

    public override void AIChangeState()
    {
        if (character.CurHp <= 0f)
        {
            stateMachine.SetState(stateDic[EAIState.Died]);
        }
        else
        {
            if (!character.IsDied)
            {
                if (character.Target == null)
                {
                    if (pathFindController.startPos == pathFindController.targetPos)
                        stateMachine.SetState(stateDic[EAIState.Idle]);
                    else
                    {
                        stateMachine.SetState(stateDic[EAIState.Chase]);
                    }
                }
                else
                {
                    if (IsSkillQueue() && IsSkillRange(character.Target))
                    {
                        stateMachine.SetState(stateDic[EAIState.UseSkill]);
                    }
                    else if (IsAtkRange(character.Target) && IsTargetEnemy(character.Target))
                    {
                        stateMachine.SetState(stateDic[EAIState.Attack]);
                    }
                    else if (!character.IsAtk)
                    {
                        stateMachine.SetState(stateDic[EAIState.Chase]);
                    }
                }
            }
        }
    }
    public override void Targeting(bool _isAlly = false)
    {
        if(!_isAlly)
        {
            RaycastHit2D _hit = Physics2D.CircleCast(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy"));
            if (_hit)
            {
                CharacterStatus _hitStatus = _hit.collider.GetComponent<CharacterStatus>();
                character.Target = _hitStatus;
            }
            else
                character.Target = null;
        }
        else
        {
            List<RaycastHit2D> _hit = new List<RaycastHit2D>();
            _hit.AddRange(Physics2D.CircleCastAll(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally")));
            if(_hit.Count >0)
            {
                switch (character.AllyTargetIndex)
                {
                    case EAllyTargetingSetUp.OneSelf:
                        character.Target = character;
                        break;
                    case EAllyTargetingSetUp.CloseAlly:
                        SortSightRayListByDistance(_hit);
                        character.Target = _hit[1].collider.GetComponent<CharacterStatus>();
                        break;
                    case EAllyTargetingSetUp.Random:
                        character.Target = ChooseSightRayListByRandom(_hit).collider.GetComponent<CharacterStatus>();
                        break;
                    case EAllyTargetingSetUp.WeakAlly:
                        SortSightRayListByCurHp(_hit);
                        character.Target = _hit[0].collider.GetComponent<CharacterStatus>();
                        break;
                }

            }
        }
    }
    public override void CheckTarget()
    {
        if (character.Target)
        {
            if (character.GetDistance(character.Target.transform.position) >= character.SeeRange || character.Target.IsDied)
            {
                character.Target.IsAllyTargeted[ally.AllyNum] = false;
                character.Target = null;
            }
        }
    }

    public override void AttackDamage()
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, character.TotalStatus[(int)EStatus.AtkRange], character.TargetDir, 1f, LayerMask.GetMask("Enemy"));
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyStatus _enemy = hits[i].collider.GetComponent<EnemyStatus>();

                _enemy.Damaged(AttackTypeDamage(),Color.red);
                if (_enemy.IsLastHit())
                {
                    _enemy.IsDied = true;
                    _enemy.SetKilledAlly(ally);
                    character.AquireExp(_enemy);
                }
            }
        }
    }
    public override IEnumerator AIDied()
    {
        ally.CurRevivalTime = ally.MaxRevivalTime;
        character.IsDied = true;
        character.Rig.velocity = Vector2.zero;
        character.Col.enabled = false;
        yield return new WaitForSeconds(ally.MaxRevivalTime);
        Rivive();
        character.IsDied = false;
    }
    public void Rivive()
    {
        this.gameObject.transform.position = rivivePoint.transform.position;
        character.Rig.isKinematic = false;
        character.Col.enabled = true;
        character.CurHp = (int)character.TotalStatus[(int)EStatus.MaxHp];
        character.TriggerStatusUpdate = true;
        character.AIState = EAIState.Idle;
    }
    public override IEnumerator AIPerception()
    {
        while (true)
        {
            if (character.Target == null)
                Targeting();
            yield return new WaitForSeconds(0.5f);
        }
    }

    //public override void ResortTarget(List<AllyStatus> _targetList)
    //{
    //    if (_targetList.Count > 0)
    //    {

    //        mercenary.Target = _targetList[0];


    //    }
    //    else
    //    {

    //        mercenary.Target = null;
    //    }
    //}
}
