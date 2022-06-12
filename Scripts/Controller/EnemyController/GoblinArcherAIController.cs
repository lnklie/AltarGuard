using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-11
 * 작성자 : Inklie
 * 파일명 : GoblinArcherAIController.cs
==============================
*/
public class GoblinArcherAIController : RushEnemyAIController
{
    private void ShotArrow()
    {
        // 화살쏘기
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(enemy,enemy.Damage);
            Debug.Log("현재 화살 수는 " + ProjectionSpawner.Instance.ArrowCount());
        }
        else
            Debug.Log("화살 없음");
    }
    public override void Attack()
    {
        base.Attack();
        ani.speed = 1 / enemy.AtkSpeed;
        ani.SetFloat("X", enemy.Dir.x);
        ani.SetFloat("Y", enemy.Dir.y);
        if (!IsDelay())
        {
            delayTime = 0f;
            if (ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
            {
                Debug.Log("쐈다");
                ShotArrow();
            }
        }
    }
}
