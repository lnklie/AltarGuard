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
    protected int maxHp = 0;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    [SerializeField]
    protected int curMp = 0;
    public int CurMp
    {
        get { return curMp; }
        set { curMp = value; }
    }
    [SerializeField]
    protected int maxMp = 0;
    public int MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }

    [SerializeField]
    protected int physicalDamage = 0;
    public int PhysicalDamage
    {
        get { return physicalDamage; }
    }
    [SerializeField]
    protected int magicalDamage = 0;
    public int MagicalDamage
    {
        get { return magicalDamage; }
    }

    [SerializeField]
    protected int defensivePower = 0;
    public int DefensivePower
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

    protected float arrowSpd = 2f;
    public float ArrowSpd
    {
        get { return arrowSpd; }
        set { arrowSpd = value; }
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

    [SerializeField]
    protected Vector2 distance = new Vector2(0, 0);
    public Vector2 Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    protected int curLevel = 100;
    public int CurLevel
    {
        get { return curLevel; }
        set { curLevel = value; }
    }
    [SerializeField]
    protected int totalStr = 5;
    public int TotalStr
    {
        get { return totalStr; }
        set { totalStr = value; }
    }
    [SerializeField]
    protected int totalDex = 5;
    public int TotalDex
    {
        get { return totalDex; }
        set { totalDex = value; }
    }
    [SerializeField]
    protected int totalWiz = 5;
    public int TotalWiz
    {
        get { return totalWiz; }
        set { totalWiz = value; }
    }
    [SerializeField]
    protected int totalLuck = 5;
    public int TotalLuck
    {
        get { return totalLuck; }
        set { totalLuck = value; }
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

    protected float attackType = 0f;
    public float AttackType
    {
        get { return attackType; }
        set { attackType = value; }
    }
    [SerializeField]
    protected RaycastHit2D sightRay = default;
    public RaycastHit2D SightRay
    {
        get { return sightRay; }
        set { sightRay = value; }
    }

    protected int hpRegenValue = 0;

    protected bool isHPRegen = false;

    protected int passiveStr = 0;
    public int PassiveStr
    {
        get { return passiveStr; }
        set { passiveStr = value; }
    }
    protected int passiveDex = 0;
    public int PassiveDex
    {
        get { return passiveDex; }
        set { passiveDex = value; }
    }
    protected int passiveWiz = 0;
    public int PassiveWiz
    {
        get { return passiveWiz; }
        set { passiveWiz = value; }
    }
    protected int passiveLuck = 0;
    public int PassiveLuck
    {
        get { return passiveLuck; }
        set { passiveLuck = value; }
    }
    private void Update()
    {
        if (!isHPRegen)
            StartCoroutine(HpRegenarate());
    }
    public void UpdateBasicStatus()
    {
        totalStr = str + passiveStr;
        totalDex = dex + passiveDex;
        totalWiz = wiz + passiveWiz;
        totalLuck = luck + passiveLuck;
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
