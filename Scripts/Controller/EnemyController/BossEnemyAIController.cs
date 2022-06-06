using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : BossEnemyAIController.cs
==============================
*/
public class BossEnemyAIController : EnemyAIController
{
    protected float patternDelay = 0f;
    protected float maxPatternDelay = 0f;
    protected BossState bossState = BossState.Idle;

    public BossState BossState
    {
        get { return bossState; }
        set { bossState = value; }
    }
    public override void Awake()
    {
        base.Awake();
        col = GetComponent<CircleCollider2D>();
    }
    public override void Update()
    {
        base.Update();
        PatternCondition();
    } 
    public override void State()
    {
        if(isStateChange)
            txtMesh.text = bossState.ToString();
        switch (bossState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Chase:
                Chase();
                break;
            case BossState.Wait:
                Wait();
                break;
            case BossState.PatternAttack:
                Pattern();
                break;
            case BossState.Died:
                StartCoroutine(Died());
                break;
        }
    }
    public override void ChangeState()
    {
        distance = enemy.Target.transform.position - this.transform.position;
        dir = distance.normalized;
        if (enemy.CurHp < 0f)
        {
            bossState = BossState.Died;
        }
        else
        {
            if (atkRange)
            {
                if (!IsDelay()) 
                {
                    SetState(BossState.PatternAttack);
                }
                else
                    SetState(BossState.Wait);
            }
            else
            {
                if (enemy.Target == this.gameObject)
                {
                    SetState(BossState.Idle);
                }
                else
                    SetState(BossState.Chase);
            }
            
        }
    }
    public void SetState(BossState _bossState)
    {
        // 상태 할당
        isStateChange = true;
        bossState = _bossState;
    }
    public override void Chase()
    {
        ActiveLayer(LayerName.WalkLayer);
        rig.velocity = enemy.Speed * dir;
    }

    public virtual void Wait()
    {
        isStateChange = false;
        ActiveLayer(LayerName.AttackLayer);
        rig.velocity = Vector2.zero;
        delayTime += Time.deltaTime;
    }
    public virtual void PatternCondition()
    {}
    public virtual void Pattern()
    {
        isStateChange = false;
    }
    public void SetMaxPatternDelay(float _time)
    {
        maxPatternDelay = _time;
    }
    public void InitPatternDelay()
    {
        patternDelay = 0f;
    }
    public bool isPatterning()
    {
        // 패턴을 하고 있는가
        if (patternDelay >= maxPatternDelay)
            return false;
        else
            return true;
    }

    public override void AnimationDirection()
    {
        // 애니메이션 방향
        if (dir.x > 0) this.transform.localScale = new Vector3(-6, 6, 1);
        else if (dir.x < 0) transform.transform.localScale = new Vector3(6, 6, 1);
    } 
}
