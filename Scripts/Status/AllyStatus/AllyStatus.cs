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
    private float revivalTime = 5f;
    private float knuckBackPower = 1;


    #region Properties
    public float[] GraceStatuses { get { return graceStatuses; } set { graceStatuses = value; } }
    public int[] GraceMagniStatuses { get { return graceMagniStatuses; } set { graceMagniStatuses = value; } }
    public int AllyNum { get{ return allyNum; } set{ allyNum = value; } }
    public float KnuckBackPower { get { return knuckBackPower; } set { knuckBackPower = value; } }
    public float RevivalTime { get { return revivalTime; } set { revivalTime = value; } }
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
                UpdateTotalAbility(EStatus.MaxHp);
                break;
            case EStatus.Dex:
                basicStatus[(int)EStatus.Speed] = totalStatus[(int)EStatus.Dex] * 0.1f;
                UpdateTotalAbility(EStatus.Speed);
                break;
            case EStatus.Wiz:
                basicStatus[(int)EStatus.MaxMp] = totalStatus[(int)EStatus.Wiz] * 10f;
                UpdateTotalAbility(EStatus.MaxMp);
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
        //Debug.Log("오브젝트 이름: " + ObjectName + " 해당 베이직 스테이터스 "+ _eStatus + " "+ basicStatus[(int)_eStatus] + " 해당 장착스테이터스 " + equipStatus[(int)_eStatus]);
        totalStatus[(int)_eStatus] = basicStatus[(int)_eStatus] + equipStatus[(int)_eStatus] + graceStatuses[(int)_eStatus]
            + ((basicStatus[(int)_eStatus] + equipStatus[(int)_eStatus] + graceStatuses[(int)_eStatus]) * (graceMagniStatuses[(int)_eStatus] / 100f))
            + buffStatus[(int)_eStatus];

        delayTime = totalStatus[(int)EStatus.AttackSpeed];
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
