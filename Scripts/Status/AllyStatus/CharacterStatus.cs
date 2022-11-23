using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : Status
{
    [SerializeField] protected int curMp = 0;
    [SerializeField] protected int curLevel = 30;
    [SerializeField] protected bool isAtk = false;
    [SerializeField] protected bool isUseSkill = false;

    [SerializeField] protected Status target = null;
    [SerializeField] protected float seeRange = 8f;
    [SerializeField] protected float maxAtkSpeed = 8f;
    [SerializeField] protected float maxCastingSpeed = 8f;
    [SerializeField] protected Vector2 distance = new Vector2(0, 0);
    [SerializeField] EAllyTargetingSetUp allyTargetIndex = 0;
    [SerializeField] byte debuff = 0;


    [Header("EquipStatus")]
    [SerializeField] protected float[] equipStatus = new float[16];

    [Header("BuffStatus")]
    [SerializeField] protected float[] buffStatus = new float[16];

    [Header("DeBuffStatus")]
    [SerializeField] protected float[] debuffStatus = new float[16];

    [SerializeField] protected EAIState aiState = EAIState.Idle;
    [SerializeField] protected Vector2 targetDir = Vector2.zero;
    
    protected int curExp = 0;
    protected int maxExp = 0;
    protected float arrowSpd = 2f;

    [SerializeField] protected float delayTime = 0f;
    [SerializeField] protected float maxDelayTime = 0f;
    [SerializeField] private float stiffenTime = 0f;
    [SerializeField] private GameObject flag = null;
    [SerializeField] private bool isFlagComeback = false;

    [SerializeField] protected RaycastHit2D hitRay = default;

    protected bool isHPRegen = false;
    [SerializeField] protected float attackType = 0f;

    [SerializeField] private bool isSkillChange = false;

    #region Properties

    public float[] EquipStatus { get { return equipStatus; } set { equipStatus = value; } }
    public float[] BuffStatus { get { return buffStatus; } set { buffStatus = value; } }

    public float[] DebuffStatus { get { return debuffStatus; } set { debuffStatus = value; } }
    public bool IsSkillChange {  get { return isSkillChange; } set { isSkillChange = value; } }
    public bool IsUseSkill { get { return isUseSkill; } set { isUseSkill = value; } }


    public EAllyTargetingSetUp AllyTargetIndex { get { return allyTargetIndex; } set { allyTargetIndex = value; } }
    public bool IsFlagComeBack {get { return isFlagComeback; } set { isFlagComeback = value; } }
    public GameObject Flag { get { return flag; } set { flag = value; } }
    public bool IsHPRegen { get { return isHPRegen; } set { isHPRegen = value; } }
    public bool IsAtk { get { return isAtk; } set { isAtk = value; } }
    public RaycastHit2D HitRay { get { return hitRay; } set { hitRay = value; } }
    public float StiffenTime { get { return stiffenTime; } set { stiffenTime = value; } }
    public float DelayTime { get { return delayTime; } set { delayTime = value; } }
    public float MaxDelayTime { get { return maxDelayTime; } set { maxDelayTime = value; } }
    public float AttackType { get { return attackType; } set { attackType = value; } }
    public EAIState AIState { get { return aiState; } set { aiState = value; } }


    public int CurLevel { get { return curLevel; } set { curLevel = value; } }
    public Vector2 Distance { get { return distance; } set { distance = value; } }
    public float ArrowSpd { get { return arrowSpd; } set { arrowSpd = value; } }
    public Vector2 TargetDir { get { return targetDir; } set { targetDir = value; } }

    public int CurMp { get { return curMp; } set { curMp = value; } }
    public float SeeRange { get { return seeRange; } set { seeRange = value; } }
    public Status Target { get { return target; } set { target = value; } }

    public int CurExp { get { return curExp; } set { curExp = value; } }
    public int MaxExp { get { return maxExp; } set { maxExp = value; } }



    #endregion
    public override void Awake()
    {
        base.Awake();
    }
    public virtual void Start()
    {
        UpdateAllStatus();
        UpdateAllBasicStatus();
        curHp = (int)totalStatus[(int)EStatus.MaxHp];
        curMp = (int)totalStatus[(int)EStatus.MaxMp];
        triggerStatusUpdate = true;
        StartCoroutine(Debuff(EDebuff.Slowed, 3, 0));
        
        //Debuff(EDebuff.Stunned, 0, 0);
        //Debuff(EDebuff.DecreaseDefence, 0, 0);
    }
    public virtual void Update()
    {
        if (!isHPRegen)
            StartCoroutine(HpRegenarate());
    }
    public void UpdateAllStatus()
    {
        for (int i = 0; i < 16; i++)
            UpdateTotalAbility((EStatus)i);
    }
    public void UpdateAllBasicStatus()
    {
        for (int i = 0; i < 4; i++)
            UpdateBasicStatus((EStatus)i);
    }
    public void AquireExp(Status status)
    {
        curExp += status.DefeatExp;
    }
    public IEnumerator Debuff(EDebuff _debuff, float _time, int _value = 0)
    {
        
        if(HasDebuff(_debuff))
        {
            Debug.Log("이미 가지고 있는 디버프입니다.");
            yield return null;
        }
        else
        {            
            debuff |= (byte)_debuff;
            switch(_debuff)
            {
                case EDebuff.Slowed:
                    Debug.Log("슬로우 중");
                    break;
                case EDebuff.Stunned:
                    Debug.Log("스턴 중");
                    break;
                case EDebuff.DecreasePhysic:
                    Debug.Log("물리 공격력 감소 중");
                    break;
                case EDebuff.DecreaseMagic:
                    Debug.Log("마법 공격력 감소 중");
                    break;
                case EDebuff.DecreaseDefence:
                    Debug.Log("방어력 감소 중");
                    break;
            }
            yield return new WaitForSeconds(_time);
            switch (_debuff)
            {
                case EDebuff.Slowed:
                    Debug.Log("슬로우 해제");
                    break;
                case EDebuff.Stunned:
                    Debug.Log("스턴 해제");
                    break;
                case EDebuff.DecreasePhysic:
                    Debug.Log("물리 공격력 감소 해제");
                    break;
                case EDebuff.DecreaseMagic:
                    Debug.Log("마법 공격력 감소 해제");
                    break;
                case EDebuff.DecreaseDefence:
                    Debug.Log("방어력 감소 해제");
                    break;
            }
            debuff ^= (byte)_debuff;
        }
    }
    private bool HasDebuff(EDebuff _debuff)
    {
        return (debuff & (byte)_debuff) > 0 ? true : false;
    }
    public virtual void UpdateBasicStatus(EStatus _eStatus)
    {

        // 힘, 민, 지 올릴때 바뀌는 것들
        switch(_eStatus)
        {
            case EStatus.Str:
                basicStatus[(int)EStatus.MaxHp] = totalStatus[(int)EStatus.Str] * 10;
                basicStatus[(int)EStatus.PhysicalDamage] = totalStatus[(int)EStatus.Str] * 5;
                UpdateTotalAbility(EStatus.MaxHp);
                UpdateTotalAbility(EStatus.PhysicalDamage);
                break;
            case EStatus.Dex:
                basicStatus[(int)EStatus.Speed] = totalStatus[(int)EStatus.Dex] * 0.1f;
                UpdateTotalAbility(EStatus.Speed);
                break;
            case EStatus.Wiz:
                basicStatus[(int)EStatus.MaxMp] = totalStatus[(int)EStatus.Wiz] * 10;
                UpdateTotalAbility(EStatus.MaxMp);
                break;
            case EStatus.Luck:
                break;
        }
    }
    public virtual void UpdateEquipAbility(Item[] _items)
    {
        InitEquipAbility();
        for (int i = 0;  i < _items.Length; i++)
        {
            if(_items[i] != null)
            {
                equipStatus[(int)EStatus.PhysicalDamage] += _items[i].physicalDamage;
                equipStatus[(int)EStatus.MagicalDamage] += _items[i].magicalDamage;
                equipStatus[(int)EStatus.DefensivePower] += _items[i].defensivePower;
                equipStatus[(int)EStatus.AtkRange] += _items[i].atkRange;
                equipStatus[(int)EStatus.AttackSpeed] += _items[i].atkSpeed;
            }
        }
    }
    public void InitEquipAbility()
    {
        for (int i = 0; i < equipStatus.Length; i++)
        {
            equipStatus[i] = 0;
        }
    }
    public virtual void UpdateTotalAbility(EStatus _eStatus)
    {
        // 능력 업데이트
        
    }
    public void RemoveBuff()
    {
        for (int i = 0; i < buffStatus.Length; i++)
            buffStatus[i] = 0;
    }
    
    public IEnumerator HpRegenarate()
    {
        isHPRegen = true;
        while (isHPRegen)
        {
            yield return new WaitForSeconds(2f);
            if (isDied)
            {
                yield return null;
            }
            else
            {
                if (curHp + totalStatus[(int)EStatus.HpRegenValue] >= totalStatus[(int)EStatus.MaxHp])
                {

                    curHp = (int)totalStatus[(int)EStatus.MaxHp];
                    yield return null;
                }
                else
                {
                    curHp += (int)totalStatus[(int)EStatus.HpRegenValue];
                }
            } 
            triggerStatusUpdate = true;
        }
    }
}
