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
    protected EquipmentController equipmentController = null;
    [SerializeField]
    protected GameObject allyTarget = null;
    protected int curExp = 0;
    private int maxExp = 0;
    private int buffPhysicalDamage = 0;
    private int buffMagicalDamage = 0;
    private int buffDefensivePower = 0;
    private float buffSpeed = 0f;
    private int buffHpRegenValue = 0;
    private float dropProbability = 0;
    private float itemRarity = 0;

    private float gracePhysicalDamage = 1f;
    private float graceMagicalDamage = 1f;
    private float graceAttackSpeed = 1f;
    private float graceDefensivePower = 1f;

    private int statusPoint = 0;
    private bool isAlterBuff = false;
    private bool[] checkEquipItems = new bool[9] { false, false, false, false, false, false, false, false, false };
    private bool isStatusUpdate = false;
    #region Properties

    public EquipmentController EquipmentController
    {
        get { return equipmentController; }
        set { equipmentController = value; }
    }
    public GameObject AllyTarget
    {
        get { return allyTarget; }
        set { allyTarget = value; }
    }
    public float GraceMagicalDamage
    {
        get { return graceMagicalDamage; }
        set { graceMagicalDamage = value; }
    }
    public float GracePhysicalDamage
    {
        get { return gracePhysicalDamage; }
        set { gracePhysicalDamage = value; }
    }
    public float GraceAttackSpeed
    {
        get { return graceAttackSpeed; }
        set { graceAttackSpeed = value; }
    }
    public float GraceDefensivePower
    {
        get { return graceDefensivePower; }
        set { graceDefensivePower = value; }
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
    public float DropProbability
    {
        get { return dropProbability; }
    }
    public float ItemRarity
    {
        get { return itemRarity; }
    }
    public int StatusPoint
    {
        get { return statusPoint; }
        set { statusPoint = value; }
    }
    public bool IsAlterBuff
    {
        get { return isAlterBuff; }
        set { isAlterBuff = value; }
    }
    public bool[] CheckEquipItems
    {
        get { return checkEquipItems; }
        set { checkEquipItems = value; }
    }
    public bool IsStatusUpdate
    {
        get { return isStatusUpdate; }
        set { isStatusUpdate = value; }
    }
    #endregion
    private void Awake()
    {
        equipmentController = this.GetComponent<EquipmentController>();
        UpdateAbility();
        LvToExp();
        curHp = maxHp;
        curMp = maxMp;
        statusPoint = 1000;
    }
    private void Update()
    {
        if (CheckMaxExp())
            UpLevel();
        if (!isAlterBuff)
        {
            RemoveBuff();
        }
        if(isStatusUpdate)
        {
            UpdateAbility();
        }

        if (equipmentController.IsChangeItem)
        {
            isStatusUpdate = true;
            checkEquipItems = equipmentController.CheckEquipItems;
            equipmentController.IsChangeItem = false;
        }
    }
    private void LvToExp()
    {
        // 레벨별 경험치 전환
        for (int i = 0; i < DatabaseManager.Instance.expList.Count; i++)
        {
            if (curLevel == DatabaseManager.Instance.expList[i].lv)
                maxExp = DatabaseManager.Instance.expList[i].exp;
        }
    }

    private void UpLevel()
    {
        // 레벨업
        curLevel++;
        curExp -= maxExp;
        statusPoint += 5;
        LvToExp();
        isStatusUpdate = true;
    }
    private bool CheckMaxExp()
    {
        // 최대 경험치 인지 확인
        if (curExp >= maxExp)
            return true;
        else
            return false;
    }
    public void UpStatus(int _index)
    {
        // 스텟 상승
        if (statusPoint > 0)
        {
            switch (_index)
            {
                case 0:
                    str++;
                    break;
                case 1:
                    dex++;
                    break;
                case 2:
                    wiz++;
                    break;
                case 3:
                    luck++;
                    break;
            }
            statusPoint--;
        }
        else
            Debug.Log("스테이터스 포인트가 없습니다.");
        isStatusUpdate = true;
    }

    public void UpdateAbility()
    {
        // 능력 업데이트
        UpdateBasicStatus();
        maxHp = 100 + totalStr * 10;
        maxMp = 100 + totalWiz * 10;
        atkSpeed = 3f - ((equipmentController.EquipItems[7].atkSpeed + (totalDex * 0.1f)) * graceAttackSpeed);
        physicalDamage = Mathf.CeilToInt((totalStr * 5 + equipmentController.GetEquipmentPhysicDamage() + buffPhysicalDamage) * gracePhysicalDamage);
        magicalDamage = Mathf.CeilToInt((totalWiz * 5 + equipmentController.GetEquipmentMagicDamage() + buffMagicalDamage) * graceMagicalDamage);
        speed = 2 + totalDex * 0.1f + buffSpeed;
        dropProbability = totalLuck * 0.001f;
        itemRarity = totalLuck * 0.001f;
        defensivePower = Mathf.CeilToInt((totalStr * 3 + equipmentController.GetEquipmentDefensivePower() + buffDefensivePower) * graceDefensivePower);
        hpRegenValue = totalStr * 1 + buffHpRegenValue;
        isStatusUpdate = false;
    }
    public void RemoveBuff()
    {
        buffPhysicalDamage = 0;
        buffMagicalDamage = 0;
        buffSpeed = 0;
        buffDefensivePower = 0;
        buffHpRegenValue = 0;
        isStatusUpdate = true;
    }
    public void InitGraceStatus()
    {
        gracePhysicalDamage = 1f;
        graceMagicalDamage = 1f;
        graceAttackSpeed = 1f;
        graceDefensivePower = 1f;
    }
}
