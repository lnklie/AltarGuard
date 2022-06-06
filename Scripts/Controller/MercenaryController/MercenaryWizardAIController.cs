using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : MercenaryWizardAIController.cs
==============================
*/
public class MercenaryWizardAIController : MercenaryAIController
{
    public override void Attack()
    {
        base.Attack();
        if (!IsDelay())
        {
            ani.SetTrigger("AtkTrigger");
            SetDelayTime(0f);
            isAtk = true;
        }
        else
            isAtk = false;
    }
}
