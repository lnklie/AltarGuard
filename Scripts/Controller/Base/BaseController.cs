using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-13
 * 작성자 : Inklie
 * 파일명 : BaseController.cs
==============================
*/
public class BaseController : MonoBehaviour
{
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
