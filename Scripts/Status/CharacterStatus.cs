using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : CharacterStatus.cs
==============================
*/
public class CharacterStatus: Status
{
    protected EquipmentController equipmentController = null;
    public EquipmentController EquipmentController
    {
        get { return equipmentController; }
        set { equipmentController = value; }
    }
    protected int curExp = 0;
    public int CurExp
    {
        get { return curExp; }
        set { curExp = value; }
    }

    private int maxExp = 0;
    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }


    [SerializeField]
    private int buffPhysicalDamage = 0;
    public int BuffPhysicalDamage
    {
        get { return buffPhysicalDamage; }
        set { buffPhysicalDamage = value; }
    }
    [SerializeField]
    private int buffMagicalDamage = 0;
    public int BuffMagicalDamage
    {
        get { return buffMagicalDamage; }
        set { buffMagicalDamage = value; }
    }

    private int buffDefensivePower = 0;
    public int BuffDefensivePower
    {
        get { return buffDefensivePower; }
        set { buffDefensivePower = value; }
    }

    private float buffSpeed = 0f;
    public float BuffSpeed
    {
        get { return buffSpeed; }
        set { buffSpeed = value; }
    }

    private int buffHpRegenValue = 0;
    public int BuffHpRegenValue
    {
        get { return buffHpRegenValue; }
        set { buffHpRegenValue = value; }
    }

    private float dropProbability = 0;
    public float DropProbability
    {
        get { return dropProbability; }
    }

    private float itemRarity = 0;
    public float ItemRarity
    {
        get { return itemRarity; }
    }

    private int statusPoint = 0;
    public int StatusPoint
    {
        get { return statusPoint; }
        set { statusPoint = value; }
    }


    private bool isAlterBuff = false;
    public bool IsAlterBuff
    {
        get { return isAlterBuff; }
        set { isAlterBuff = value; }
    }

    private bool[] checkEquipItems = new bool[9] { false, false, false, false, false, false, false, false, false };
    public bool[] CheckEquipItems
    {
        get { return checkEquipItems; }
        set { checkEquipItems = value; }
    }


    private void Awake()
    {
        equipmentController = this.GetComponent<EquipmentController>();
    }
    private void Start()
    {
        UpdateAbility();
        LvToExp();
        curHp = maxHp;
        statusPoint = 5;
    }
    private void Update()
    {
        if (CheckMaxExp())
            UpLevel();
        if (!isAlterBuff)
        {
            RemoveBuff();
            UpdateAbility();
        }

        if (equipmentController.IsChangeItem == true)
        {
            UpdateAbility();
            checkEquipItems = equipmentController.CheckEquipItems;
            equipmentController.IsChangeItem = false;
        }
    }
    private void LvToExp()
    {
        // ������ ����ġ ��ȯ
        for (int i = 0; i < DatabaseManager.Instance.expList.Count; i++)
        {
            if (curLevel == DatabaseManager.Instance.expList[i].lv)
                maxExp = DatabaseManager.Instance.expList[i].exp;
        }
    }

    private void UpLevel()
    {
        // ������
        curLevel++;
        curExp -= maxExp;
        statusPoint += 5;
        LvToExp();
    }
    private bool CheckMaxExp()
    {
        // �ִ� ����ġ ���� Ȯ��
        if (curExp >= maxExp)
            return true;
        else
            return false;
    }
    public void UpStatus(int _index)
    {
        // ���� ���
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
            Debug.Log("�������ͽ� ����Ʈ�� �����ϴ�.");
        UpdateAbility();
    }

    public override void UpdateAbility()
    {
        // �ɷ� ������Ʈ
        base.UpdateAbility();
        physicalDamage = str * 5 + equipmentController.GetEquipmentPhysicDamage() + buffPhysicalDamage;
        magicalDamage = wiz * 5 + equipmentController.GetEquipmentMagicDamage() + buffMagicalDamage;
        speed = 2 + dex * 0.1f + buffSpeed;
        dropProbability = luck * 0.001f;
        itemRarity = luck * 0.001f;
        defensivePower = str * 3 + equipmentController.GetEquipmentDefensivePower() + buffDefensivePower;
        hpRegenValue = str * 1 + buffHpRegenValue;
    }
    public void RemoveBuff()
    {
        buffPhysicalDamage = 0;
        buffMagicalDamage = 0;
        buffSpeed = 0;
        buffDefensivePower = 0;
        buffHpRegenValue = 0;
    }
}
