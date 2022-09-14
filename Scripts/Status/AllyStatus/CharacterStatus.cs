using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : Status
{
    [SerializeField] protected bool isAtk = false;
    [SerializeField] protected Transform target = null;
    [SerializeField] protected float seeRange = 8f;
    [SerializeField] protected int curMp = 0;
    [SerializeField] protected float maxAtkSpeed = 8f;
    [SerializeField] protected float maxCastingSpeed = 8f;
    protected float arrowSpd = 2f;
    [SerializeField] protected Vector2 distance = new Vector2(0, 0);
    [SerializeField] protected int curLevel = 30;

    [Header("TotalStatus")]
    [SerializeField] protected int totalMaxHp = 0;
    [SerializeField] protected int totalMaxMp = 0;
    [SerializeField] protected int totalStr = 0;
    [SerializeField] protected int totalDex = 0;
    [SerializeField] protected int totalWiz = 0;
    [SerializeField] protected int totalLuck = 0;
    [SerializeField] protected int totalHpRegenValue = 0;
    [SerializeField] protected int totalPhysicalDamage = 0;
    [SerializeField] protected int totalMagicalDamage = 0;
    [SerializeField] protected int totalDefensivePower = 0;
    [SerializeField] protected float totalSpeed = 0;
    [SerializeField] protected float totalAtkSpeed = 0;
    [SerializeField] protected float totalAtkRange = 0;
    [SerializeField] protected float totalCastingSpeed = 0;

    [Header("BasicStatus")]
    [SerializeField] protected int maxMp = 0;
    [SerializeField] protected int str = 5;
    [SerializeField] protected int dex = 5;
    [SerializeField] protected int wiz = 5;
    [SerializeField] protected int luck = 5;
    [SerializeField] protected int hpRegenValue = 0;
    [SerializeField] protected int physicalDamage = 1000;
    [SerializeField] protected int magicalDamage = 0;
    [SerializeField] protected float speed = 0f;
    [SerializeField] protected float atkSpeed = 2f;
    [SerializeField] protected float atkRange = 0f;
    [SerializeField] protected float castingSpeed = 5f;

    [Header("EquipStatus")]
    [SerializeField] protected int equipedStr = 0;
    [SerializeField] protected int equipedDex = 0;
    [SerializeField] protected int equipedWiz = 0;
    [SerializeField] protected int equipedLuck = 0;
    [SerializeField] protected int equipedHpRegenValue = 0;
    [SerializeField] protected int equipedPhysicalDamage = 0;
    [SerializeField] protected int equipedMagicalDamage = 0;
    [SerializeField] protected int equipedDefensivePower = 0;
    [SerializeField] protected float equipedSpeed = 0f;
    [SerializeField] protected float equipedAtkSpeed = 0f;
    [SerializeField] protected float equipedAtkRange = 0f;
    [SerializeField] protected float equipedCastingSpeed = 0f;

    [Header("GraceStatus")]
    [SerializeField] protected int graceMaxHp = 0;
    [SerializeField] protected int graceMaxMp = 0;
    [SerializeField] protected int graceHpRegenValue = 0;
    [SerializeField] protected int graceStr = 0;
    [SerializeField] protected int graceDex = 0;
    [SerializeField] protected int graceWiz = 0;
    [SerializeField] protected int graceLuck = 0;
    [SerializeField] protected int gracePhysicalDamage = 0;
    [SerializeField] protected int graceMagicalDamage = 0;
    [SerializeField] protected int graceDefensivePower = 0;
    [SerializeField] protected float graceSpeed = 0f;
    [SerializeField] protected float graceAttackSpeed = 0f;
    [SerializeField] protected float graceAtkRange = 0f;
    [SerializeField] protected float graceCastingSpeed = 0f;


    [SerializeField] protected EAIState aiState = EAIState.Idle;
    [SerializeField] protected Vector2 targetDir = Vector2.zero;
    [SerializeField] protected bool triggerEquipmentChange = false;
    //[SerializeField] protected EquipmentController equipmentController = null;
    [SerializeField] protected Transform allyTarget = null;
    protected int curExp = 0;
    protected int maxExp = 0;

    [Header("BuffStatus")]
    [SerializeField] protected int buffPhysicalDamage = 0;
    [SerializeField] protected int buffMagicalDamage = 0;
    [SerializeField] protected int buffDefensivePower = 0;
    [SerializeField] protected float buffSpeed = 0f;
    [SerializeField] protected int buffHpRegenValue = 0;



    [SerializeField] protected float delayTime = 0f;
    [SerializeField] private float stiffenTime = 0f;
    [SerializeField] private GameObject flag = null;
    [SerializeField] private bool isFlagComeback = false;

    [SerializeField] protected RaycastHit2D hitRay = default;
    [SerializeField] protected List<EnemyStatus> enemyRayList = new List<EnemyStatus>();
    [SerializeField] protected List<Status> allyRayList = new List<Status>();
    protected bool isHPRegen = false;
    protected float attackType = 0f;
    [SerializeField] private bool[] isAllyTargeted = new bool[5];
    [SerializeField] private bool[] isEnemyTargeted = new bool[101];
    [SerializeField] private bool isDied = false;
    [SerializeField] private bool isSkillChange = false;
    #region Properties
    public bool TriggerEquipmentChange { get { return triggerEquipmentChange; } set { triggerEquipmentChange = value; } }
    public bool IsSkillChange {  get { return isSkillChange; } set { isSkillChange = value; } }
    public int GraceMaxHp { get { return graceMaxHp; } set { graceMaxHp = value; } }
    public int GraceMaxMp { get { return graceMaxMp; } set { graceMaxMp = value; } }
    public int GraceDefensivePower { get { return graceDefensivePower; } set { graceDefensivePower = value; } }
    public int GraceHpRegenValue { get { return graceHpRegenValue; } set { graceHpRegenValue = value; } }
    public bool IsDied { get { return isDied; } set { isDied = value; } }
    public bool[] IsAllyTargeted { get { return isAllyTargeted; } set { isAllyTargeted = value; } }
    public bool[] IsEnemyTargeted { get { return isEnemyTargeted; } set { isEnemyTargeted = value; } }
    public bool IsFlagComeBack {get { return isFlagComeback; } set { isFlagComeback = value; } }
    public GameObject Flag { get { return flag; } set { flag = value; } }
    public bool IsHPRegen { get { return isHPRegen; } set { isHPRegen = value; } }
    public List<Status> AllyRayList { get { return allyRayList; } set { allyRayList = value; } }
    public bool IsAtk { get { return isAtk; } set { isAtk = value; } }
    public RaycastHit2D HitRay { get { return hitRay; } set { hitRay = value; } }
    public float StiffenTime { get { return stiffenTime; } set { stiffenTime = value; } }
    public float DelayTime { get { return delayTime; } set { delayTime = value; } }
    public List<EnemyStatus> EnemyRayList { get { return enemyRayList; } set { enemyRayList = value; } }
    public float AttackType { get { return attackType; } set { attackType = value; } }
    public EAIState AIState { get { return aiState; } set { aiState = value; } }
    public int GraceLuck { get { return graceLuck; } set { graceLuck = value; } }
    public int GraceWiz { get { return graceWiz; } set { graceWiz = value; } }
    public int GraceDex { get { return graceDex; } set { graceDex = value; } }
    public int GraceStr { get { return graceStr; } set { graceStr = value; } }
    public float GraceSpeed { get { return graceSpeed; } set { graceSpeed = value; } }
    public float GraceAtkRange { get { return graceAtkRange; } set { graceAtkRange = value; } }
    public int GraceMagicalDamage { get { return graceMagicalDamage; } set { graceMagicalDamage = value; } }
    public int GracePhysicalDamage { get { return gracePhysicalDamage; } set { gracePhysicalDamage = value; } }
    public float GraceAttackSpeed { get { return graceAttackSpeed; } set { graceAttackSpeed = value; } }
    public float GraceCastingSpeed { get { return graceCastingSpeed; } set { graceCastingSpeed = value; } }
    public int Luck { get { return luck; } set { luck = value; } }
    public int Wiz { get { return wiz; } set { wiz = value; } }
    public int Dex { get { return dex; } set { dex = value; } }
    public int Str { get { return str; } set { str = value; } }
    public int HpRegenValue { get { return hpRegenValue; } set { hpRegenValue = value; } }
    public int TotalHpRegenValue { get { return totalHpRegenValue; } set { totalHpRegenValue = value; } }
    public int TotalLuck { get { return totalLuck; } set { totalLuck = value; } }
    public int TotalWiz { get { return totalWiz; } set { totalWiz = value; } }
    public int TotalDex { get { return totalDex; } set { totalDex = value; } }
    public int TotalStr { get { return totalStr; } set { totalStr = value; } }
    public int TotalMaxHp { get { return totalMaxHp; } set { totalMaxHp = value; } }
    public int TotalMaxMp { get { return totalMaxMp; } set { totalMaxMp = value; } }
    public int TotalPhysicalDamage { get { return totalPhysicalDamage; } set { totalPhysicalDamage = value; } }
    public int TotalMagicalDamage { get { return totalMagicalDamage; } set { totalMagicalDamage = value; } }
    public float TotalSpeed { get { return totalSpeed; } set { totalSpeed = value; } }
    public float TotalAtkRange { get { return totalAtkRange; } set { totalAtkRange = value; } }
    public float TotalAtkSpeed { get { return totalAtkSpeed; } set { totalAtkSpeed = value; } }
    public float TotalCastingSpeed { get { return totalCastingSpeed; } set { totalCastingSpeed = value; } }
    public int CurLevel { get { return curLevel; } set { curLevel = value; } }
    public Vector2 Distance { get { return distance; } set { distance = value; } }
    public float ArrowSpd { get { return arrowSpd; } set { arrowSpd = value; } }
    public Vector2 TargetDir { get { return targetDir; } set { targetDir = value; } }
    public float AtkSpeed { get { return atkSpeed; } set { atkSpeed = value; } }
    public float Speed { get { return speed; } }
    public int PhysicalDamage { get { return physicalDamage; } }
    public int MagicalDamage { get { return magicalDamage; } }
    public int CurMp { get { return curMp; } set { curMp = value; } }
    public int MaxMp { get { return maxMp; } set { maxMp = value; } }
    public float AtkRange { get { return atkRange; } set { atkRange = value; } }
    public float SeeRange { get { return seeRange; } set { seeRange = value; } }
    public float CastingSpeed { get { return castingSpeed; } set { castingSpeed = value; } }
    public Transform Target { get { return target; } set { target = value; } }
    //public EquipmentController EquipmentController { get { return equipmentController; } set { equipmentController = value; } }
    public Transform AllyTarget { get { return allyTarget; } set { allyTarget = value; } }
    public int CurExp { get { return curExp; } set { curExp = value; } }
    public int MaxExp { get { return maxExp; } set { maxExp = value; } }
    public int BuffPhysicalDamage { get { return buffPhysicalDamage; } set { buffPhysicalDamage = value; } }
    public int BuffMagicalDamage { get { return buffMagicalDamage; } set { buffMagicalDamage = value; } }
    public int BuffDefensivePower { get { return buffDefensivePower; } set { buffDefensivePower = value; } }
    public float BuffSpeed { get { return buffSpeed; } set { buffSpeed = value; } }
    public int BuffHpRegenValue { get { return buffHpRegenValue; } set { buffHpRegenValue = value; } }


    #endregion
    public override void Awake()
    {
        base.Awake();
        //equipmentController = this.GetComponent<EquipmentController>();
        UpdateTotalAbility();
        curHp = totalMaxHp;
        curMp = totalMaxMp;
        
    }
    public virtual void Start()
    {
        triggerStatusUpdate = true;
    }
    public override void Update()
    {
        base.Update();

        if (triggerEquipmentChange)
        {
            triggerEquipmentChange = false;
            UpdateTotalAbility();
        }
        if (!isHPRegen)
            StartCoroutine(HpRegenarate());
    }
    public void AquireExp(Status status)
    {
        curExp += status.DefeatExp;
    }
    public virtual void UpdateBasicStatus()
    {
        totalStr = str + equipedStr + graceStr;
        totalDex = dex + equipedDex + graceDex;
        totalWiz = wiz + equipedWiz + graceWiz;
        totalLuck = luck + equipedLuck + graceLuck;

        maxHp = totalStr * 10;
        MaxMp = totalWiz * 10;
        speed = totalDex * 0.1f;
    }
    public virtual void UpdateEquipAbility(Item[] _items)
    {
        InitEquipAbility();
        for (int i = 0;  i < _items.Length; i++)
        {
            equipedPhysicalDamage += _items[i].physicalDamage;
            equipedMagicalDamage += _items[i].magicalDamage;
            equipedDefensivePower += _items[i].defensivePower;
            equipedAtkRange += _items[i].atkRange;
            equipedAtkSpeed += _items[i].atkSpeed;
        }
    }
    public void InitEquipAbility()
    {
        equipedStr = 0;
        equipedDex = 0;
        equipedWiz = 0;
        equipedLuck = 0;
        equipedHpRegenValue = 0;
        equipedPhysicalDamage = 0;
        equipedMagicalDamage = 0;
        equipedDefensivePower = 0;
        equipedSpeed = 0f;
        equipedAtkSpeed = 0f;
        equipedAtkRange = 0f;
        equipedCastingSpeed = 0f;
    }
    public virtual void UpdateTotalAbility()
    {
        // 능력 업데이트
        UpdateBasicStatus();
        totalMaxHp = maxHp + graceMaxHp;
        totalMaxMp = maxMp + graceMaxMp;

        totalDefensivePower = defensivePower + equipedDefensivePower + graceDefensivePower + buffDefensivePower;

        totalAtkSpeed = maxAtkSpeed - (atkSpeed + equipedAtkSpeed + graceAttackSpeed);
        totalAtkRange = atkRange + equipedAtkRange + graceAtkRange;
        totalCastingSpeed = maxCastingSpeed - (castingSpeed + equipedCastingSpeed + graceCastingSpeed);
        
        totalPhysicalDamage = physicalDamage + equipedPhysicalDamage + gracePhysicalDamage + buffPhysicalDamage;
        totalMagicalDamage = magicalDamage + equipedMagicalDamage + graceMagicalDamage + buffMagicalDamage;

        totalSpeed = speed + equipedSpeed + graceSpeed + buffSpeed;
        totalHpRegenValue = hpRegenValue + equipedHpRegenValue + graceHpRegenValue + buffHpRegenValue;

        delayTime = totalAtkSpeed;
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
                if (curHp + totalHpRegenValue >= totalMaxHp)
                {

                    curHp = totalMaxHp;
                    yield return null;
                }
                else
                {
                    curHp += totalHpRegenValue;
                }
            } 
            triggerStatusUpdate = true;
        }
    }
}
