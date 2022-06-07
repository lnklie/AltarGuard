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
public class CharacterStatus: Status
{
    private EquipmentController equipmentController = null;
    public EquipmentController EquipmentController
    {
        get { return equipmentController; }
        set { equipmentController = value; }
    }

    protected int money = 0;
    public int Money
    { 
        get { return money; } 
        set { money = value; }
    }
     
    protected int curMp = 100;
    public int CurMp
    {
        get { return curMp; }
        set { curMp = value; }
    }

    private int curLevel = 1;
    public int CurLevel
    {
        get { return curLevel; }
        set { curLevel = value; }
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

    private int maxMp = 100;
    public int MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }

    private int physicalDamage = 0;
    public int PhysicalDamage
    {
        get { return physicalDamage; }
    }

    private int magicalDamage = 0;
    public int MagicalDamage
    {
        get { return magicalDamage; }
    }

    private int buffPhysicalDamage = 0;
    public int BuffPhysicalDamage
    {
        get { return buffPhysicalDamage; }
        set { buffPhysicalDamage = value; }
    }

    private int buffMagicalDamage = 0;
    public int BuffMagicalDamage
    {
        get { return buffMagicalDamage; }
        set { buffMagicalDamage = value; }
    }

    private float buffDefensivePower = 0;
    public float BuffDefensivePower
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

    private int str = 5;
    public int Str
    {
        get { return str; }
        set { str = value; }
    }

    private int dex = 5;
    public int Dex
    {
        get { return dex; }
        set { dex = value; }
    }

    private int wiz = 5;
    public int Wiz
    {
        get { return wiz; }
        set { wiz = value; }
    }

    private int luck = 5;
    public int Luck
    {
        get { return luck; }
        set { luck = value; }
    }

    private bool isAlterBuff = false;
    public bool IsAlterBuff
    {
        get { return isAlterBuff; }
        set { isAlterBuff = value; }
    }

    protected float arrowSpd = 1f;
    public float ArrowSpd
    {
        get { return arrowSpd; }
        set { arrowSpd = value; }
    }

    private bool[] checkEquipItems = new bool[9] { false, false, false, false, false, false, false, false, false };
    public bool[] CheckEquipItems
    {
        get { return checkEquipItems; }
        set { checkEquipItems = value; }
    }

    private ItemEquipedCharacter itemEquipedCharacter = ItemEquipedCharacter.NULL;
    public ItemEquipedCharacter ItemEquipedCharacter
    {
        get { return itemEquipedCharacter; }
        set { itemEquipedCharacter = value; }
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
    }
    private void Update()
    {
        if (CheckMaxExp())
            UpLevel();

        if (equipmentController.IsChangeItem == true)
        {
            Debug.Log("아이템 변경");
            UpdateAbility();
            checkEquipItems = equipmentController.CheckEquipItems;
            equipmentController.IsChangeItem = false;
        }
    }
    private void LvToExp()
    {
        // 레벨별 경험치 전환
        for (int i = 0; i < DatabaseManager.Instance.exp.Count; i++)
        {
            if (curLevel == DatabaseManager.Instance.exp[i].lv)
                maxExp = DatabaseManager.Instance.exp[i].exp;
        }
    }

    private void UpLevel()
    {
        // 레벨업
        curLevel++;
        curExp -= maxExp;
        statusPoint += 5;
        LvToExp();
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
        UpdateAbility();
    }

    public void UpdateAbility()
    {
        // 능력 업데이트
        maxHp += 100 + str * 10;
        maxMp = 100 + wiz * 10;
        physicalDamage = str * 5 + equipmentController.GetEquipmentPhysicDamage() + buffPhysicalDamage;
        magicalDamage = wiz * 5 + equipmentController.GetEquipmentMagicDamage() + buffMagicalDamage;
        speed = 2 + dex * 0.1f + buffSpeed;
        atkSpeed = 1 - dex * 0.1f;
        dropProbability = luck * 0.001f;
        itemRarity = luck * 0.001f;
        defensivePower = str * 3  + equipmentController.GetEquipmentDefensivePower() + buffDefensivePower;
    }
}
