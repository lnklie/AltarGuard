using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : RushEnemyAIController.cs
==============================
*/
public class RushEnemyAIController : EnemyAIController
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void State()
    {
        txtMesh.text = enemy.EnemyState.ToString();
        switch (enemy.EnemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                Stiffen();
                break;
            case EnemyState.Died:
                StartCoroutine(Died());
                break;
        }
    }
    public override void ChangeState()
    {
        distance = enemy.Target.transform.position - enemy.transform.position;
        enemy.Dir = distance.normalized;
        if (enemy.CurHp < 0f)
        {
            enemy.EnemyState = EnemyState.Died;
        }
        else
        {
            if (isDamaged)
            {
                enemy.EnemyState = EnemyState.Damaged;
            }
            else
            {
                if (atkRange)
                {
                    enemy.EnemyState = EnemyState.Attack;
                }
                else
                {
                    if (enemy.Target == this.gameObject || FrontOtherEnemy())
                    {
                        enemy.EnemyState = EnemyState.Idle;
                    }
                    else
                        enemy.EnemyState = EnemyState.Chase;
                }
            }
        }
    }

    public override void Stiffen()
    {
        enemy.StiffenTime += Time.deltaTime;
        enemy.IsKnuckBack = true;
        if (enemy.StiffenTime >= enemy.MaxStiffenTime)
        {
            enemy.IsKnuckBack = false;
            enemy.DmgCombo = 0;
            isDamaged = false;
        }
    }

    public override void Perception()
    {
        enemyHit = Physics2D.RaycastAll(this.transform.position, enemy.Dir, 0.5f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(this.transform.position, enemy.Dir, Color.blue);

        base.Perception();
    }

    public override void Attack()
    {
        base.Attack();
    }
    public bool FrontOtherEnemy()
    {
        // 앞에 다른 적이 있는 지 확인
        for (int i = 0; i < enemyHit.Length; i++)
        {
            if (enemyHit[i].collider.gameObject != this.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        // 넉백 효과
        float timer = 0;

        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rig.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
}
