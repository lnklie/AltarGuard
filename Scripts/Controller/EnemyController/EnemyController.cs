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
            destination = enemyStatus.Target.transform.position;
            enemyStatus.TargetDir = enemyStatus.Distance.normalized;
        }
    }

    public void FindAltar()
    {
        enemyStatus.AltarRay = Physics2D.CircleCast(enemyStatus.transform.position, 100f, Vector2.up, 0, LayerMask.GetMask("Altar"));
        altar = enemyStatus.AltarRay.rigidbody.GetComponent<Status>();
    }

    public override void AIChangeState()
    {
        if (enemyStatus.IsDied)
        {
            stateMachine.SetState(stateDic[EAIState.Died]);
        }
        if (!enemyStatus.IsDied)
        {
            if(enemyStatus.Target != null)
            {

                if (IsSkillQueue() && IsSkillRange(enemyStatus.Target))
                    stateMachine.SetState(stateDic[EAIState.UseSkill]);
                else if (IsAtkRange(enemyStatus.Target))
                    stateMachine.SetState(stateDic[EAIState.Attack]);
                else
                    stateMachine.SetState(stateDic[EAIState.Chase]);

            }

        }
    }

    public override void Targeting(int _layer)
    {

        RaycastHit2D _hit = Physics2D.CircleCast(this.transform.position, enemyStatus.SeeRange, Vector2.up, 0, _layer);
        if (_hit)
        {

            CharacterStatus _hitStatus = _hit.collider.GetComponent<CharacterStatus>();
            enemyStatus.Target = _hitStatus;

        }
        else
            enemyStatus.Target = altar;
    }


    public override IEnumerator AIPerception()
    {
        while(true)
        {
            if(enemyStatus.Target == altar || enemyStatus.Target == null)
                Targeting(LayerMask.GetMask("Ally"));
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


}
