using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStatus : CharacterStatus
{

    [Header("GraceStatus")]
    [SerializeField] private float[] graceStatuses = new float[16];


    [Header("GraceMagnificationStatus")]
    [SerializeField] private int[] graceMagniStatuses = new int[16];


    [SerializeField] private bool isAlterBuff = false;
    [SerializeField] private int allyNum = 0;

    private int statusPoint = 10;
    [SerializeField] private float maxRevivalTime = 10f;
    [SerializeField] private float curRevivalTime = 0f;
    private float knuckBackPower = 1;


    #region Properties
    public float[] GraceStatuses { get { return graceStatuses; } set { graceStatuses = value; } }
    public int[] GraceMagniStatuses { get { return graceMagniStatuses; } set { graceMagniStatuses = value; } }
    public int AllyNum { get{ return allyNum; } set{ allyNum = value; } }
    public float KnuckBackPower { get { return knuckBackPower; } set { knuckBackPower = value; } }
    public float MaxRevivalTime { get { return maxRevivalTime; } set { maxRevivalTime = value; } }
    public float CurRevivalTime { get { return curRevivalTime; } set { curRevivalTime = value; } }
    public int StatusPoint { get { return statusPoint; } set { statusPoint = value; } }
    public bool IsAlterBuff {get { return isAlterBuff; } set { isAlterBuff = value; } }

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

        if (isDied)
        {
            CurRevivalTime -= Time.deltaTime;
        }
    }

    public void UpStatus(int _index)
    {
        // 스텟 상승
        if (statusPoint > 0)
        {
            switch (_index)
            {
                case 0:
                    basicStatus[(int)EStatus.Str]++;
                    UpdateTotalAbility(EStatus.Str);
                    break;
                case 1:
                    basicStatus[(int)EStatus.Dex]++;
                    UpdateTotalAbility(EStatus.Dex);
                    break;
                case 2:
                    basicStatus[(int)EStatus.Wiz]++;
                    UpdateTotalAbility(EStatus.Wiz);
                    break;
                case 3:
                    basicStatus[(int)EStatus.Luck]++;
                    UpdateTotalAbility(EStatus.Luck);
                    break;
            }
            statusPoint--;
        }
        else
            Debug.Log("스테이터스 포인트가 없습니다.");
    }

    private void UpLevel()
    {
        // 레벨업
        curLevel++;
        curExp = 0;
        statusPoint += 5;
        LvToExp();
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
    public override void UpdateBasicStatus(EStatus _eStatus)
    {
        switch (_eStatus)
        {
            case EStatus.Str:
                basicStatus[(int)EStatus.MaxHp] = totalStatus[(int)EStatus.Str] * 10;
                basicStatus[(int)EStatus.PhysicalDamage] = totalStatus[(int)EStatus.Str] * 5;
                UpdateTotalAbility(EStatus.MaxHp);
                UpdateTotalAbility(EStatus.PhysicalDamage);
                break;
            case EStatus.Dex:
                basicStatus[(int)EStatus.Speed] = totalStatus[(int)EStatus.Dex] * 0.01f;
                basicStatus[(int)EStatus.AttackSpeed] = totalStatus[(int)EStatus.Dex] * 0.01f;
                UpdateTotalAbility(EStatus.Speed);
                UpdateTotalAbility(EStatus.AttackSpeed);
                break;
            case EStatus.Wiz:
                basicStatus[(int)EStatus.MaxMp] = totalStatus[(int)EStatus.Wiz] * 10f;
                basicStatus[(int)EStatus.MagicalDamage] = totalStatus[(int)EStatus.Wiz] * 5;
                UpdateTotalAbility(EStatus.MaxMp);
                UpdateTotalAbility(EStatus.MagicalDamage);
                break;
            case EStatus.Luck:
                basicStatus[(int)EStatus.DropProbability] = totalStatus[(int)EStatus.Luck] * 0.001f;
                basicStatus[(int)EStatus.ItemRarity] = totalStatus[(int)EStatus.Luck] * 0.001f;
                UpdateTotalAbility(EStatus.DropProbability);
                UpdateTotalAbility(EStatus.ItemRarity);
                break;
        }
        
    }
    public override void UpdateEquipAbility(Item[] _items)
    {
        base.UpdateEquipAbility(_items);
        for (int i = 0; i < _items.Length; i++)
        {
            //equipStatus[(int)EStatus.DropProbability] += _items[i].;
            //equipStatus[(int)EStatus.ItemRarity] += _items[i].magicalDamage;
        }
    }
  
    public override void UpdateTotalAbility(EStatus _eStatus)
    {
        // 능력 업데이트

        int _index = CastTo<int>.From(_eStatus);
        totalStatus[_index] = basicStatus[_index] + equipStatus[_index] + graceStatuses[_index];
        totalStatus[_index] += totalStatus[_index] * (graceMagniStatuses[_index] / 100f);
        totalStatus[_index] += totalStatus[_index] * (buffStatus[_index] - debuffStatus[_index]);

        if(_eStatus == EStatus.AttackSpeed)
            maxDelayTime = 1f / totalStatus[(int)EStatus.AttackSpeed];
    }

    public void InitGraceStatus()
    {
        for(int i = 0; i < 16; i++)
        {
            graceStatuses[i] = 0f;
            graceMagniStatuses[i] = 0;
        }
    }
}
