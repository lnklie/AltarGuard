using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStatus : CharacterStatus
{


    [SerializeField]
    private float dropProbability = 0;
    [SerializeField]
    private float itemRarity = 0;
    private int statusPoint = 10;
    [SerializeField]
    private bool isAlterBuff = false;
    private float revivalTime = 5f;
    private float knuckBackPower = 1;

    [SerializeField]
    private int allyNum = 0;
    #region Properties
    public int AllyNum
    {
        get{ return allyNum; }
        set{ allyNum = value; }
    }
    public float KnuckBackPower
    {
        get { return knuckBackPower; }
        set { knuckBackPower = value; }
    }
    public float RevivalTime
    {
        get { return revivalTime; }
        set { revivalTime = value; }
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
    public float DropProbability
    {
        get { return dropProbability; }
    }
    public float ItemRarity
    {
        get { return itemRarity; }
    }

    #endregion
    public void Start()
    {
        LvToExp();
    }

    public override void Update()
    {
        base.Update();
        if (!isAlterBuff)
        {
            RemoveBuff();
        }
        if (CheckMaxExp())
            UpLevel();
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
        UpdateAbility();
    }

    private void UpLevel()
    {
        // 레벨업
        curLevel++;
        curExp -= maxExp;
        statusPoint += 5;
        LvToExp();
        UpdateAbility();
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
    private bool CheckMaxExp()
    {
        // 최대 경험치 인지 확인
        if (curExp >= maxExp)
            return true;
        else
            return false;
    }
    public override void UpdateAbility()
    {
        // 능력 업데이트
        base.UpdateAbility();
        dropProbability = totalLuck * 0.001f;
        itemRarity = totalLuck * 0.001f;
        atkSpeed = 5f - ((equipmentController.EquipItems[7] != null ? atkSpeed : 0f + (totalDex * 0.1f)) * graceAttackSpeed);
        physicalDamage = Mathf.CeilToInt((totalStr * 5 + equipmentController.GetEquipmentPhysicDamage() + buffPhysicalDamage) * gracePhysicalDamage);
        magicalDamage = Mathf.CeilToInt((totalWiz * 5 + equipmentController.GetEquipmentMagicDamage() + buffMagicalDamage) * graceMagicalDamage);
        defensivePower = Mathf.CeilToInt((totalStr * 3 + equipmentController.GetEquipmentDefensivePower() + buffDefensivePower) * graceDefensivePower);
    }
    public void InitGraceStatus()
    {
        graceMaxHp = 0;
        graceMaxMp = 0;
        graceHpRegenValue = 0;
        graceStr = 0;
        graceDex = 0;
        graceWiz = 0;
        graceDex = 0;
        gracePhysicalDamage = 0;
        graceMagicalDamage = 0;
        graceDefensivePower = 0;
        graceSpeed = 0f;
        graceAttackSpeed = 0f;
        graceAtkRange = 0f;
    }
}
