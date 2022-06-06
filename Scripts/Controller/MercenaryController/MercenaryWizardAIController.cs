using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : MercenaryWizardAIController.cs
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
