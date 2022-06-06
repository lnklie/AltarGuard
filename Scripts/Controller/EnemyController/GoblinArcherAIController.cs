using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
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
            GameObject shotArrow = ProjectionSpawner.Instance.Arrows.Dequeue();
            shotArrow.transform.position = this.transform.position;
            Arrow arrow = shotArrow.GetComponent<Arrow>();
            arrow.Archer = this.gameObject;
            arrow.Dir = dir;
            arrow.Spd = enemy.GetComponent<EnemyStatus>().ArrowSpd;
            arrow.Dmg = enemy.Damage;
            arrow.gameObject.SetActive(true);
        }
        else
            Debug.Log("ȭ�� ����");
    }
    public override void Attack()
    {
        base.Attack();
        ani.speed = 1 / enemy.AtkSpeed;
        ani.SetFloat("X", dir.x);
        ani.SetFloat("Y", dir.y);
        if (!IsDelay())
        {
            delayTime = 0f;
        }
    }
}
