using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStatus : CharacterStatus
{
    [SerializeField] private float totalDropProbability = 0;
    [SerializeField] private float totalItemRarity = 0;

    [SerializeField] private float dropProbability = 0;
    [SerializeField] private float itemRarity = 0;

    [SerializeField] private float equipDropProbability = 0;
    [SerializeField] private float equipItemRarity = 0;

    [SerializeField] private float graceDropProbability = 0;
    [SerializeField] private float graceItemRarity = 0;

    private int statusPoint = 10;
    [SerializeField] private bool isAlterBuff = false;
    private float revivalTime = 5f;
    private float knuckBackPower = 1;

    [SerializeField] private int allyNum = 0;
    #region Properties

    public float GraceDropProbability { get { return graceDropProbability; }  set { graceDropProbability = value; } }
    public float GraceItemRarity { get { return graceItemRarity; } set { graceItemRarity = value; } }
    public int AllyNum { get{ return allyNum; } set{ allyNum = value; } }
    public float KnuckBackPower { get { return knuckBackPower; } set { knuckBackPower = value; } }
    public float RevivalTime { get { return revivalTime; } set { revivalTime = value; } }

    public int StatusPoint { get { return statusPoint; } set { statusPoint = value; } }

    public bool IsAlterBuff {get { return isAlterBuff; } set { isAlterBuff = value; } }
    public float TotalDropProbability { get { return totalDropProbability; } }
    public float TotalItemRarity { get { return totalItemRarity; } }

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
            Debug.Log("�������ͽ� ����Ʈ�� ���ϴ�.");
        UpdateTotalAbility();
    }

    private void UpLevel()
    {
        curLevel++;
        curExp -= maxExp;
        statusPoint += 5;
        LvToExp();
        UpdateTotalAbility();
    }

    private void LvToExp()
    {
        for (int i = 0; i < DatabaseManager.Instance.expList.Count; i++)
        {
            if (curLevel == DatabaseManager.Instance.expList[i].lv)
                maxExp = DatabaseManager.Instance.expList[i].exp;
        }
    }
    private bool CheckMaxExp()
    {
        if (curExp >= maxExp)
            return true;
        else
            return false;
    }
    public override void UpdateBasicStatus()
    {
        base.UpdateBasicStatus();

        dropProbability = totalLuck * 0.001f;
        itemRarity = totalLuck * 0.001f;
    }
    public override void UpdateEquipAbility(Item[] _items)
    {
        InitEquipAbility();
        for (int i = 0; i < _items.Length; i++)
        {
            equipedPhysicalDamage += _items[i].physicalDamage;
            equipedMagicalDamage += _items[i].magicalDamage;
            equipedDefensivePower += _items[i].defensivePower;
            equipedAtkRange += _items[i].atkRange;
            equipedAtkSpeed += _items[i].atkSpeed;
            //equipDropProbability += _items[i].;
            //equipItemRarity += _items[i].;
        }
    }
    public override void UpdateTotalAbility()
    {
        // �ɷ� ����Ʈ
        base.UpdateTotalAbility();
        totalDropProbability = dropProbability + equipDropProbability + graceDropProbability ;
        totalItemRarity = itemRarity + equipItemRarity + graceItemRarity;
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
        graceDropProbability = 0f;
        graceItemRarity = 0f;

        graceMagniMaxHp = 0;
        graceMagniMaxMp = 0;
        graceMagniHpRegenValue = 0;
        graceMagniStr = 0;
        graceMagniDex = 0;
        graceMagniWiz = 0;
        graceMagniLuck = 0;
        graceMagniPhysicalDamage = 0;
        graceMagniMagicalDamage = 0;
        graceMagniDefensivePower = 0;
        graceMagniSpeed = 0;
        graceMagniAttackSpeed = 0;
        graceMagniAtkRange = 0;
        graceMagniCastingSpeed = 0;
    }
}
