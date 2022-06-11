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
            ProjectionSpawner.Instance.ShotArrow(character, AttackTypeDamage());
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
