using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Status.cs
==============================
*/
public class Status : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }
    [SerializeField]
    private float seeRange = 2f;
    public float SeeRange
    {
        get { return seeRange; }
        set { seeRange = value; }
    }
    [SerializeField]
    private float atkRange = 2f;
    public float AtkRange
    {
        get { return atkRange; }
        set { atkRange = value; }
    }

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
    protected int maxHp = 100;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    [SerializeField]
    protected float defensivePower = 0;
    public float DefensivePower
    {
        get { return defensivePower; }
        set { defensivePower = value; }
    }
    [SerializeField]
    protected float speed = 0f;
    public float Speed
    {
        get { return speed; }
    }
    [SerializeField]
    protected float atkSpeed = 0f;
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }
}
