using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : CharacterStatus.cs
==============================
*/
public class CharacterStatus : Status
{
    [SerializeField]
    protected bool isAtk = false;
    [SerializeField]
    protected Transform target = null;
    [SerializeField]
    protected float seeRange = 8f;
    [SerializeField]
    protected float atkRange = 0f;
    [SerializeField]
    protected int curMp = 0;
    [SerializeField]
    protected int maxMp = 0;
    [SerializeField]
    protected int physicalDamage = 0;
    [SerializeField]
    protected int magicalDamage = 0;
    [SerializeField]
    protected float speed = 0f;
    [SerializeField]
    protected float atkSpeed = 2f;
    protected float arrowSpd = 2f;
    [SerializeField]
    protected Vector2 distance = new Vector2(0, 0);
    protected int curLevel = 30;
    [SerializeField]
    protected int totalStr = 5;
    [SerializeField]
    protected int totalDex = 5;
    [SerializeField]
    protected int totalWiz = 5;
    [SerializeField]
    protected int totalLuck = 5;
    protected int str = 5;
    protected int dex = 5;
    protected int wiz = 5;
    protected int luck = 5;
    protected float attackType = 0f;
    [SerializeField]
    protected RaycastHit2D hitRay = default;
    [SerializeField]
    protected List<EnemyStatus> enemyRayList = new List<EnemyStatus>();
    [SerializeField]
    protected List<Status> allyRayList = new List<Status>();
    [SerializeField]
    protected int hpRegenValue = 0;
    protected bool isHPRegen = false;
    protected int passiveStr = 0;
    protected int passiveDex = 0;
    protected int passiveWiz = 0;
    protected int passiveLuck = 0;
    [SerializeField]
    protected EAIState aiState = EAIState.Idle;
    [SerializeField]
    protected Vector2 targetDir = Vector2.zero;
    [SerializeField]
    protected EquipmentController equipmentController = null;
    [SerializeField]
    protected Transform allyTarget = null;
    protected int curExp = 0;
    protected int maxExp = 0;
    protected int buffPhysicalDamage = 0;
    protected int buffMagicalDamage = 0;
    protected int buffDefensivePower = 0;
    protected float buffSpeed = 0f;
    protected int buffHpRegenValue = 0;
    [SerializeField]
    protected bool isStatusUpdate = false;
    [SerializeField]
    protected float delayTime = 0f;
    [SerializeField]
    private float stiffenTime = 0f;
    [SerializeField]
    private GameObject flag = null;
    [SerializeField]
    private bool isFlagComeback = false;

    [SerializeField]
    private bool[] isAllyTargeted = new bool[5];
    [SerializeField]
    private bool[] isEnemyTargeted = new bool[101];
    #region Properties
    public bool[] IsAllyTargeted
    {
        get { return isAllyTargeted; }
        set { isAllyTargeted = value; }
    }
    public bool[] IsEnemyTargeted
    {
        get { return isEnemyTargeted; }
        set { isEnemyTargeted = value; }
    }
    public bool IsFlagComeBack
    {
        get { return isFlagComeback; }
        set { isFlagComeback = value; }
    }
    public GameObject Flag
    {
        get { return flag; }
    }

    public bool IsHPRegen
    {
        get { return isHPRegen; }
        set { isHPRegen = value; }
    }

    public List<Status> AllyRayList
    {
        get { return allyRayList; }
        set { allyRayList = value; }
    }
    public bool IsAtk
    {
        get { return isAtk; }
        set { isAtk = value; }
    }
    public RaycastHit2D HitRay
    {
        get { return hitRay; }
        set { hitRay = value; }
    }
    public float StiffenTime
    {
        get { return stiffenTime; }
        set { stiffenTime = value; }
    }
    public float DelayTime
    {
        get { return delayTime; }
        set { delayTime = value; }
    }
    public List<EnemyStatus> EnemyRayList
    {
        get { return enemyRayList; }
        set { enemyRayList = value; }
    }
    public float AttackType
    {
        get { return attackType; }
        set { attackType = value; }
    }
    public EAIState AIState
    {
        get { return aiState; }
        set { aiState = value; }
    }
    public int PassiveLuck
    {
        get { return passiveLuck; }
        set { passiveLuck = value; }
    }
    public int PassiveWiz
    {
        get { return passiveWiz; }
        set { passiveWiz = value; }
    }
    public int PassiveDex
    {
        get { return passiveDex; }
        set { passiveDex = value; }
    }
    public int PassiveStr
    {
        get { return passiveStr; }
        set { passiveStr = value; }
    }
    public int Luck
    {
        get { return luck; }
        set { luck = value; }
    }
    public int Wiz
    {
        get { return wiz; }
        set { wiz = value; }
    }
    public int Dex
    {
        get { return dex; }
        set { dex = value; }
    }
    public int Str
    {
        get { return str; }
        set { str = value; }
    }
    public int TotalLuck
    {
        get { return totalLuck; }
        set { totalLuck = value; }
    }
    public int TotalWiz
    {
        get { return totalWiz; }
        set { totalWiz = value; }
    }
    public int TotalDex
    {
        get { return totalDex; }
        set { totalDex = value; }
    }
    public int TotalStr
    {
        get { return totalStr; }
        set { totalStr = value; }
    }
    public int CurLevel
    {
        get { return curLevel; }
        set { curLevel = value; }
    }
    public Vector2 Distance
    {
        get { return distance; }
        set { distance = value; }
    }
    public float ArrowSpd
    {
        get { return arrowSpd; }
        set { arrowSpd = value; }
    }
    public Vector2 TargetDir
    {
        get { return targetDir; }
        set { targetDir = value; }
    }
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public int PhysicalDamage
    {
        get { return physicalDamage; }
    }
    public int MagicalDamage
    {
        get { return magicalDamage; }
    }
    public int CurMp
    {
        get { return curMp; }
        set { curMp = value; }
    }
    public int MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }
    public float AtkRange
    {
        get { return atkRange; }
        set { atkRange = value; }
    }
    public float SeeRange
    {
        get { return seeRange; }
        set { seeRange = value; }
    }
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    public EquipmentController EquipmentController
    {
        get { return equipmentController; }
        set { equipmentController = value; }
    }
    public Transform AllyTarget
    {
        get { return allyTarget; }
        set { allyTarget = value; }
    }

    public int CurExp
    {
        get { return curExp; }
        set { curExp = value; }
    }
    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }

    public int BuffPhysicalDamage
    {
        get { return buffPhysicalDamage; }
        set { buffPhysicalDamage = value; }
    }
    public int BuffMagicalDamage
    {
        get { return buffMagicalDamage; }
        set { buffMagicalDamage = value; }
    }
    public int BuffDefensivePower
    {
        get { return buffDefensivePower; }
        set { buffDefensivePower = value; }
    }
    public float BuffSpeed
    {
        get { return buffSpeed; }
        set { buffSpeed = value; }
    }
    public int BuffHpRegenValue
    {
        get { return buffHpRegenValue; }
        set { buffHpRegenValue = value; }
    }

    public bool IsStatusUpdate
    {
        get { return isStatusUpdate; }
        set { isStatusUpdate = value; }
    }
    #endregion
    public override void Awake()
    {
        base.Awake();
        equipmentController = this.GetComponent<EquipmentController>();
        UpdateAbility();
        curHp = maxHp;
        curMp = maxMp;
        delayTime = atkSpeed;
    }

    public override void Update()
    {
        base.Update();
        if(isStatusUpdate)
        {
            isStatusUpdate = false;
        }
        if (equipmentController.IsChangeItem)
        {
            equipmentController.IsChangeItem = false;
            UpdateAbility();
        }
        if (!isHPRegen)
            StartCoroutine(HpRegenarate());
    }
    public void AquireExp(Status status)
    {
        curExp += status.DefeatExp;
        
    }
    public void UpdateBasicStatus()
    {
        totalStr = str + passiveStr;
        totalDex = dex + passiveDex;
        totalWiz = wiz + passiveWiz;
        totalLuck = luck + passiveLuck;
    }

    public virtual void UpdateAbility()
    {
        // 능력 업데이트
        UpdateBasicStatus();
        maxHp = 100 + totalStr * 10;
        maxMp = 100 + totalWiz * 10;
        speed = 2 + totalDex * 0.1f + buffSpeed;
        //hpRegenValue = totalStr * 1 + buffHpRegenValue;
    }
    public void RemoveBuff()
    {
        buffPhysicalDamage = 0;
        buffMagicalDamage = 0;
        buffSpeed = 0;
        buffDefensivePower = 0;
        buffHpRegenValue = 0;
    }

    public IEnumerator HpRegenarate()
    {
        isHPRegen = true;
        while (isHPRegen)
        {
            yield return new WaitForSeconds(2f);
            if (curHp <= 0)
            {
                yield return null;
            }
            else
            {
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
            isStatusUpdate = true;
        }
    }
}
