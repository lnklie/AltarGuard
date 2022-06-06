using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : MercenaryArcherAIController.cs
==============================
*/
public class MercenaryArcherAIController : MercenaryAIController
{
    private void ShotArrow()
    {
        // 활쏘기
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
            Debug.Log("화살 없음");
    }
    public override void Attack()
    {
        base.Attack();
        if (!IsDelay())
        {
            ani.SetTrigger("AtkTrigger");
            Debug.Log("공격");
            SetDelayTime(0f);
            if (ani.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.35f)
            {
                Debug.Log("쐈다");
                ShotArrow();
            }
        }
        else
            isAtk = false;
    }
}
