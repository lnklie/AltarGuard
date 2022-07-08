using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStatus : CharacterStatus
{
    private float gracePhysicalDamage = 1f;
    private float graceMagicalDamage = 1f;
    private float graceAttackSpeed = 1f;
    private float graceDefensivePower = 1f;

    private float dropProbability = 0;
    private float itemRarity = 0;
    private int statusPoint = 0;

    private bool isAlterBuff = false;
    private float revivalTime = 5f;
    private float knuckBackPower = 1;
    #region Properties
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
    #endregion
    public override void Start()
    {
        base.Start();
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
        isStatusUpdate = true;
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
        atkSpeed = 3f - ((equipmentController.EquipItems[7].atkSpeed + (totalDex * 0.1f)) * graceAttackSpeed);
        physicalDamage = Mathf.CeilToInt((totalStr * 5 + equipmentController.GetEquipmentPhysicDamage() + buffPhysicalDamage) * gracePhysicalDamage);
        magicalDamage = Mathf.CeilToInt((totalWiz * 5 + equipmentController.GetEquipmentMagicDamage() + buffMagicalDamage) * graceMagicalDamage);
        defensivePower = Mathf.CeilToInt((totalStr * 3 + equipmentController.GetEquipmentDefensivePower() + buffDefensivePower) * graceDefensivePower);
    }
    public void InitGraceStatus()
    {
        gracePhysicalDamage = 1f;
        graceMagicalDamage = 1f;
        graceAttackSpeed = 1f;
        graceDefensivePower = 1f;
    }
}
