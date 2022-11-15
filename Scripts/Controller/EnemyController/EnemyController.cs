using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{ 
    [SerializeField] private Status altar = null;
    [SerializeField] protected EnemyStatus enemyStatus = null;
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
        if(enemyStatus.Target)
        {
            enemyStatus.Distance = enemyStatus.Target.transform.position - enemyStatus.TargetPos.position;
            enemyStatus.TargetDir = enemyStatus.Distance.normalized;
            destination = enemyStatus.Target.transform.position;
            if (enemyStatus.Target != altar)
                CheckTarget();
        }
    }

    public void FindAltar()
    {
        enemyStatus.AltarRay = Physics2D.CircleCast(enemyStatus.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Altar"));
        altar = enemyStatus.AltarRay.rigidbody.GetComponent<Status>();
    }

    public override void AIChangeState()
    {
        if (enemyStatus.CurHp <= 0f)
        {
            stateMachine.SetState(stateDic[EAIState.Died]);
        }
        else
        {
            if (!enemyStatus.IsDied)
            {
                if(enemyStatus.Target != null)
                {
                    if (IsSkillQueue() && IsSkillRange(enemyStatus.Target))
                        stateMachine.SetState(stateDic[EAIState.UseSkill]);
                    else if (IsAtkRange(enemyStatus.Target) && IsTargetEnemy(character.Target))
                        stateMachine.SetState(stateDic[EAIState.Attack]);
                    else
                        stateMachine.SetState(stateDic[EAIState.Chase]);
                }
            }
        }
    }

    public override void Targeting(bool _isAlly = false)
    {

        if (!_isAlly)
        {
            RaycastHit2D _hit = Physics2D.CircleCast(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Ally"));
            if (_hit)
            {
                CharacterStatus _hitStatus = _hit.collider.GetComponent<CharacterStatus>();
                character.Target = _hitStatus;
            }
            else
                enemyStatus.Target = altar;
        }
        else
        {
            List<RaycastHit2D> _hit = new List<RaycastHit2D>();
            _hit.AddRange(Physics2D.CircleCastAll(this.transform.position, character.SeeRange, Vector2.up, 0, LayerMask.GetMask("Enemy")));
            if (_hit.Count > 0)
            {
                SortSightRayListByCurHp(_hit);
                character.Target = _hit[0].collider.GetComponent<CharacterStatus>();

            }
        }
    }


    public override IEnumerator AIPerception()
    {
        while(true)
        {
            if(enemyStatus.Target == altar || enemyStatus.Target == null)
                Targeting();
            yield return new WaitForSeconds(0.5f);
        }
    }

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


}
