using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-11
 * 작성자 : Inklie
 * 파일명 : Status.cs
==============================
*/
public class Status : MonoBehaviour
{
    [SerializeField]
    protected string objectName = "";
    public string ObjectName
    {
        get { return objectName; }
        set { objectName = value; }
    }

    [SerializeField]
    protected int curHp = 0;
    public int CurHp
    {
        get { return curHp; }
        set { curHp = value; }
    }

    [SerializeField]
    protected int maxHp = 0;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    [SerializeField]
    protected int defensivePower = 0;
    public int DefensivePower
    {
        get { return defensivePower; }
        set { defensivePower = value; }
    }


    [SerializeField]
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



    private void Awake()
    {
        
    }

}
