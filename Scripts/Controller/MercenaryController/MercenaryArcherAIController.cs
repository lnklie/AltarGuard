using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : MercenaryArcherAIController.cs
==============================
*/
public class MercenaryArcherAIController : MercenaryAIController
{
    private void ShotArrow()
    {
        // Ȱ���
        if (ProjectionSpawner.Instance.Arrows.Count > 0)
        {
            GameObject shotArrow = ProjectionSpawner.Instance.Arrows.Dequeue();
            shotArrow.transform.position = this.transform.position;
            Arrow arrow = shotArrow.GetComponent<Arrow>();
            arrow.Archer = this.gameObject;
            arrow.Dir = dir;
            arrow.Spd = status.ArrowSpd;
            arrow.Dmg = AttackTypeDamage();
            arrow.gameObject.SetActive(true);
        }
        else
            Debug.Log("ȭ�� ����");
    }
    public override void Attack()
    {
        base.Attack();
        if (!IsDelay())
        {
            ani.SetTrigger("AtkTrigger");
            Debug.Log("����");
            SetDelayTime(0f);
            if (ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
            {
                Debug.Log("����");
                ShotArrow();
            }
        }
        else
            isAtk = false;
    }
}
