using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : BaseController.cs
==============================
*/
public class BaseController : MonoBehaviour
{
    protected bool isDamaged = false;
    public bool IsDamaged
    {
        get { return isDamaged; }
        set { isDamaged = value; }
    }

    protected bool isStateChange = false;
    public bool IsStateChange
    {
        get { return isStateChange; }
        set { isStateChange = value; }
    }

    private Debuff debuff = Debuff.Not;
    public Debuff Debuff
    {
        get { return debuff; }
        set { debuff = value; }
    }

    protected float attackType = 0f;
    public float AttackType
    {
        get { return attackType; }
        set { attackType = value; }
    }
}
