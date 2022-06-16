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
    protected GameObject target = null;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    [SerializeField]
    protected float seeRange = 0f;
    public float SeeRange
    {
        get { return seeRange; }
        set { seeRange = value; }
    }

    [SerializeField]
    protected float atkRange = 0f;
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

    protected int curMp = 100;
    public int CurMp
    {
        get { return curMp; }
        set { curMp = value; }
    }

    protected int maxMp = 100;
    public int MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }

    protected int physicalDamage = 0;
    public int PhysicalDamage
    {
        get { return physicalDamage; }
    }

    protected int magicalDamage = 0;
    public int MagicalDamage
    {
        get { return magicalDamage; }
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
    protected float atkSpeed = 2f;
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }

    [SerializeField]
    protected Vector2 dir = Vector2.zero;
    public Vector2 Dir
    {
        get { return dir; }
        set { dir = value; }
    }

    protected float arrowSpd = 1f;
    public float ArrowSpd
    {
        get { return arrowSpd; }
        set { arrowSpd = value; }
    }

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

    [SerializeField]
    protected Vector2 distance = new Vector2(0, 0);
    public Vector2 Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    protected int curLevel = 1;
    public int CurLevel
    {
        get { return curLevel; }
        set { curLevel = value; }
    }
    protected int str = 5;
    public int Str
    {
        get { return str; }
        set { str = value; }
    }

    protected int dex = 5;
    public int Dex
    {
        get { return dex; }
        set { dex = value; }
    }

    protected int wiz = 5;
    public int Wiz
    {
        get { return wiz; }
        set { wiz = value; }
    }

    protected int luck = 5;
    public int Luck
    {
        get { return luck; }
        set { luck = value; }
    }

    protected RaycastHit2D atkRangeRay = default;
    public RaycastHit2D AtkRangeRay
    {
        get { return atkRangeRay; }
        set { atkRangeRay = value; }
    }
    protected RaycastHit2D sightRay = default;
    public RaycastHit2D SightRay
    {
        get { return sightRay; }
        set { sightRay = value; }
    }

    protected int hpRegenValue = 0;

    protected bool isHPRegen = false;



    private void Update()
    {
        if (!isHPRegen)
            StartCoroutine(HpRegenarate());

    }
    public IEnumerator HpRegenarate()
    {
        isHPRegen = true;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (curHp + hpRegenValue >= maxHp)
            {

                curHp = maxHp;
                yield return null;
            }
            else
            {
                curHp += hpRegenValue;
            }
        }
    }
}
