using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-11
 * �ۼ��� : Inklie
 * ���ϸ� : GoblinArcherAIController.cs
==============================
*/
public class GoblinArcherAIController : RushEnemyAIController
{
    private void ShotArrow()
    {
        // ȭ����
        if (ProjectionSpawner.Instance.ArrowCount() > 0)
        {
            ProjectionSpawner.Instance.ShotArrow(enemy,enemy.Damage);
            Debug.Log("���� ȭ�� ���� " + ProjectionSpawner.Instance.ArrowCount());
        }
        else
            Debug.Log("ȭ�� ����");
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
                Debug.Log("����");
                ShotArrow();
            }
        }
    }
}
