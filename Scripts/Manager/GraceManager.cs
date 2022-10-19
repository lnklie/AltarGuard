using System.Collections.Generic;
using UnityEngine;

public class GraceManager : SingletonManager<GraceManager>
{
    [Header("Grace")]
    [SerializeField] private List<GraceConditionWho> graceConditionWhoList = new List<GraceConditionWho>();
    [SerializeField] private List<GraceConditionWhat> graceConditionWhatList = new List<GraceConditionWhat>();
    [SerializeField] private List<GraceConditionHow> graceConditionHowList = new List<GraceConditionHow>();
    [SerializeField] private List<GraceResultWho> graceResultWhoList = new List<GraceResultWho>();
    [SerializeField] private List<GraceResultWhat> graceResultWhatList = new List<GraceResultWhat>();
    [SerializeField] private List<GraceResultIsPercent> graceResultIsPercentList = new List<GraceResultIsPercent>();
    [SerializeField] private List<GraceResultHow> graceResultHowList = new List<GraceResultHow>();

    [SerializeField] private List<CompleteGrace> graceList = new List<CompleteGrace>();
    [SerializeField] private List<BigGrace> bigGraceList = new List<BigGrace>();
    [SerializeField] private List<AllyStatus> characterStatuses = new List<AllyStatus>();
    [SerializeField] private List<EquipmentController> characterEquipmentController = new List<EquipmentController>();

    public List<CompleteGrace> GraceList { get { return graceList; } }

    private void Update()
    {
        if (characterStatuses[(int)ECharacter.Player].TriggerEquipmentChange)
        {
            ActiveGrace();
        }

    }
    public void SetGraceList()
    {
        graceConditionWhoList = DatabaseManager.Instance.graceConditionWhoList;
        graceConditionWhatList = DatabaseManager.Instance.graceConditionWhatList;
        graceConditionHowList = DatabaseManager.Instance.graceConditionHowList;
        graceResultWhoList = DatabaseManager.Instance.graceResultWhoList;
        graceResultWhatList = DatabaseManager.Instance.graceResultWhatList;
        graceResultIsPercentList = DatabaseManager.Instance.graceResultIsPercentList;
        graceResultHowList = DatabaseManager.Instance.graceResultHowList;
    }
    public BigGrace SelectBigGrace(int _key)
    {
        BigGrace bigGrace = null;
        return bigGrace;
    }
    public Grace SelectGrace(int _key)
    {
        Grace _grace = null;
        switch (_key / 1000)
        {
            case 0:
                for (int i = 0; i < graceConditionWhoList.Count; i++)
                {
                    if (graceConditionWhoList[i].graceKey == _key)
                        _grace = graceConditionWhoList[i];
                    //else
                        //Debug.Log("�ش� ��� �����ΰ� ���ϴ�.");
                }
                break;
            case 1:
                for (int i = 0; i < graceConditionWhatList.Count; i++)
                {
                    if (graceConditionWhatList[i].graceKey == _key)
                        _grace = graceConditionWhatList[i];
                    //else
                        //Debug.Log("�ش� ��� �����ΰ� ���ϴ�.");
                }
                break;
            case 2:
                for (int i = 0; i < graceConditionHowList.Count; i++)
                {
                    if (graceConditionHowList[i].graceKey == _key)
                        _grace = graceConditionHowList[i];
                    //else
                        //Debug.Log("�ش� ��� �����ΰ� ���ϴ�.");
                }
                break;
            case 3:
                for (int i = 0; i < graceResultWhoList.Count; i++)
                {
                    if (graceResultWhoList[i].graceKey == _key)
                        _grace = graceResultWhoList[i];
                    //else
                        //Debug.Log("�ش� ��� �����ΰ� ���ϴ�.");
                }
                break;
            case 4:
                for (int i = 0; i < graceResultWhatList.Count; i++)
                {
                    if (graceResultWhatList[i].graceKey == _key)
                        _grace = graceResultWhatList[i];
                    //else
                        //Debug.Log("�ش� ��� �����ΰ� ���ϴ�.");
                }
                break;
            case 5:
                for (int i = 0; i < graceResultIsPercentList.Count; i++)
                {
                    if (graceResultIsPercentList[i].graceKey == _key)
                        _grace = graceResultIsPercentList[i];
                    //else
                        //Debug.Log("�ش� ��� �����ΰ� ���ϴ�.");
                }
                break;
            case 6:
                for (int i = 0; i < graceResultHowList.Count; i++)
                {
                    if (graceResultHowList[i].graceKey == _key)
                        _grace = graceResultHowList[i];
                    //else
                       //Debug.Log("�ش� ��� �����ΰ� ���ϴ�.");
                }
                break;
        }
        return _grace;
    }
    public void ActiveGrace()
    {
        for (int i = 0; i < characterStatuses.Count; i++)
        {
            characterStatuses[i].InitGraceStatus(); 
        }
        for (int i = 0; i < graceList.Count; i++)
        {

            if (CheckGraceCondition(graceList[i]))
            {
                OperateGrace(graceList[i]);
                Debug.Log("�۵�");
            }
            else
            {
                Debug.Log("�Ƚ�");
            }

        }
    }
    public CompleteGrace CreateRandomGrace() 
    {
        int conditionWho = Random.Range(-1, graceConditionWhoList.Count);
        int conditionWhat = Random.Range(0, graceConditionWhatList.Count);
        int conditionValue = -1;
        int conditionHow = -1;
        if (conditionWhat > 13)
        {
            conditionHow = 3; 
            conditionValue = Random.Range(0, 10);
        }
        else if (conditionWhat > 12)
            conditionHow = 2;
        else if(conditionWhat > 10)
            conditionHow = 1;
        else
            conditionHow = 0;
        int resultWho = Random.Range(0, graceResultWhoList.Count);
        int resultValue = Random.Range(0, 10);
        int resultWhat = Random.Range(0, graceResultWhatList.Count);
        int resultValueIsPercent = Random.Range(0, 2);
        //int resultHow = Random.Range(0, DatabaseManager.Instance.graceResultHowList.Count);
        float weightedValue = 0;
        if(conditionWho != -1)
        {
            weightedValue =
                SelectGrace(conditionWho).weightedValue * SelectGrace(conditionWhat + 1000).weightedValue
             * SelectGrace(resultWho + 3000).weightedValue * SelectGrace(resultWhat + 4000).weightedValue * SelectGrace(resultValueIsPercent + 5000).weightedValue;
        }
        else
        {
            weightedValue = SelectGrace(resultWho + 3000).weightedValue * SelectGrace(resultWhat + 4000).weightedValue * SelectGrace(resultValueIsPercent + 5000).weightedValue;
        }
        CompleteGrace _completeGrace = new CompleteGrace(
            "", conditionWho, conditionWhat + 1000, conditionValue, conditionHow + 2000, resultWho + 3000, resultWhat + 4000,
            Mathf.CeilToInt(resultValue * weightedValue), resultValueIsPercent ,6000);
        _completeGrace.explain = AddGraceExplain(_completeGrace); 

        return _completeGrace;
    }
    public string AddGraceExplain(CompleteGrace _completeGrace)
    {
        string _explain = null;
        if(_completeGrace.conditionWho != -1)
        {
            _explain = SelectGrace(_completeGrace.conditionWho).graceKorName + "��(��) ";
            if (_completeGrace.conditionWhat < 1014)
            {
                _explain += SelectGrace(_completeGrace.conditionWhat).graceKorName + "�(��) ";
            }
            else
            {
                _explain += SelectGrace(_completeGrace.conditionWhat).graceKorName + "�� ";
                _explain += _completeGrace.conditionValue;
            }
            _explain += SelectGrace(_completeGrace.conditionHow).graceKorName + ", ";
        }
        _explain += SelectGrace(_completeGrace.resultWho).graceKorName + "�� ";
        _explain += SelectGrace(_completeGrace.resultWhat).graceKorName + "�(��) ";
        if(_completeGrace.resultValueIsPercent == 0)
            _explain += _completeGrace.resultValue + " ��ŭ ";
        else
            _explain += _completeGrace.resultValue + "% ��ŭ ";
        _explain += SelectGrace(_completeGrace.resultHow).graceKorName;
        return _explain;
    }
    public bool CheckGraceCondition(CompleteGrace _grace)
    {
        bool _bool = false;
        if(_grace.conditionWho != -1)
        {
            switch((EGraceConditionWho)_grace.conditionWho)
            {
                case EGraceConditionWho.Player:
                case EGraceConditionWho.Mercenary1:
                case EGraceConditionWho.Mercenary2:
                case EGraceConditionWho.Mercenary3:
                case EGraceConditionWho.Mercenary4:
                    if (_grace.conditionWhat != -1)
                    {
                        switch ((EGraceConditionWhat)_grace.conditionWhat)
                        {
                            case EGraceConditionWhat.Sword:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Sword")
                                        {
                                            _bool = true;
                                        }
                                        else
                                        {
                                            _bool = false;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.OnlySword:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[_grace.conditionWho].CheckEquipItems[8] == false)
                                        {
                                            _bool = true;
                                        }
                                        else
                                        {
                                            _bool = false;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Spear:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Spear")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Exe:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Exe")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Shield:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Shield")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Bow:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Bow")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Wand:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Wand")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.OnlyWand:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[_grace.conditionWho].CheckEquipItems[8] == false)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.SpearOrExe:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Exe")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.SwordAndShield:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Shield")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.WandAndShiled:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Shield")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                        }

                    }
                    break;
                case EGraceConditionWho.AllMercenary:
                    if (_grace.conditionWhat != -1)
                    {
                        switch ((EGraceConditionWhat)_grace.conditionWhat)
                        {
                            case EGraceConditionWhat.Sword:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[1].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Sword")
                                        {
                                            _bool = true;
                                        }
                                        else
                                        {
                                            _bool = false;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.OnlySword:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[1].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[1].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[2].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[3].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[4].CheckEquipItems[8] == false))
                                        {
                                            _bool = true;
                                        }
                                        else
                                        {
                                            _bool = false;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Spear:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[1].EquipItems[7].weaponType == "Spear" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Spear" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Spear" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Spear")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Exe:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[1].EquipItems[7].weaponType == "Axe" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Axe" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Axe" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Axe")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Shield:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[1].EquipItems[8].weaponType == "Shield" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Shield" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Shield" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Shield")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Bow:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[1].EquipItems[7].weaponType == "Bow" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Bow" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Bow" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Bow")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Wand:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[1].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Wand")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.OnlyWand:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[1].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[1].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[2].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[3].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[4].CheckEquipItems[8] == false))
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.SpearOrExe:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[1].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Exe") &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Exe") &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Exe") &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Exe"))
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.SwordAndShield:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[1].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Shield"))
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.WandAndShiled:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[1].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Shield"))
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                        }

                    }
                    break;
                case EGraceConditionWho.All:
                    if (_grace.conditionWhat != -1)
                    {
                        switch ((EGraceConditionWhat)_grace.conditionWhat)
                        {
                            case EGraceConditionWhat.Sword:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[0].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Sword")
                                        {
                                            _bool = true;
                                        }
                                        else
                                        {
                                            _bool = false;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.OnlySword:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[0].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[0].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[1].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[1].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[2].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[3].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[4].CheckEquipItems[8] == false))
                                        {
                                            _bool = true;
                                        }
                                        else
                                        {
                                            _bool = false;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Spear:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[0].EquipItems[7].weaponType == "Spear" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Spear" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Spear" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Spear" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Spear")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Exe:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[0].EquipItems[7].weaponType == "Exe" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Exe" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Exe" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Exe" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Exe")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Shield:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[0].EquipItems[8].weaponType == "Shield" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Shield" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Shield" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Shield" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Shield")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Bow:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[0].EquipItems[7].weaponType == "Bow" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Bow" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Bow" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Bow" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Bow")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Wand:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if (characterEquipmentController[0].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Wand")
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.OnlyWand:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[0].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[0].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[1].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[1].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[2].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[3].CheckEquipItems[8] == false) &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[4].CheckEquipItems[8] == false)
                                            )
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.SpearOrExe:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[0].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[0].EquipItems[7].weaponType == "Exe") &&
                                            (characterEquipmentController[1].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Exe") &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Exe") &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Exe") &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Spear" ||
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Exe"))
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.SwordAndShield:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[0].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[0].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[1].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Shield"))
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.WandAndShiled:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.Equip:
                                        if ((characterEquipmentController[0].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[0].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[1].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[7].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Shield"))
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                        }

                    }
                    break;
            }
        }
        else
        {
            _bool = true;
        }
        return _bool;
    }
    public void OperateGrace(CompleteGrace _grace)
    {
        if (_grace.resultWho != -1)
        {
            switch ((EGraceResultWho)_grace.resultWho)
            {
                case EGraceResultWho.Player:
                case EGraceResultWho.Mercenary1:
                case EGraceResultWho.Mercenary2:
                case EGraceResultWho.Mercenary3:
                case EGraceResultWho.Mercenary4:
                    if (_grace.resultWhat != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat)
                        {
                        case EGraceResultWhat.Str:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceStr += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceStr -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniStr += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniStr -= _grace.resultValue;
                                }
                            break;
                        case EGraceResultWhat.Dex:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceDex += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceDex -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniDex += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniDex -= _grace.resultValue;
                                }

                                break;
                        case EGraceResultWhat.Wiz:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceWiz += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceWiz -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniWiz += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniWiz -= _grace.resultValue;
                                }
                                break;

                        case EGraceResultWhat.Luck:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceLuck += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceLuck -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniLuck += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniLuck -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.HpRegen:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceHpRegenValue += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceHpRegenValue -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniHpRegenValue += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniHpRegenValue -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.PhysicalDamage:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GracePhysicalDamage += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GracePhysicalDamage -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniPhysicalDamage += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniPhysicalDamage -= _grace.resultValue;
                                }
                                break;

                        case EGraceResultWhat.MagicalDamage:

                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagicalDamage += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagicalDamage -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniMagicalDamage += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniMagicalDamage -= _grace.resultValue;
                                }
                                break;

                        case EGraceResultWhat.DefensivePower:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceDefensivePower += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceDefensivePower -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniDefensivePower += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniDefensivePower -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.Speed:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceSpeed += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceSpeed -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniSpeed += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniSpeed -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.AtkSpeed:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceAttackSpeed += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceAttackSpeed -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniAtkRange += _grace.resultValue;
                                    else
                                        characterStatuses[_grace.resultWho - 3000].GraceMagniAtkRange -= _grace.resultValue;
                                }
                                break;
                        }
                    break;
                case EGraceResultWho.AllMercenary:
                    if (_grace.resultWhat != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat)
                    {
                        case EGraceResultWhat.Str:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceStr += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceStr -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniStr +=  _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniStr -= _grace.resultValue;
                                }
                            break;
                        case EGraceResultWhat.Dex:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDex += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDex -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDex += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDex -=  _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.Wiz:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceWiz += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceWiz -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniWiz += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniWiz -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.Luck:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceLuck += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceLuck -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniLuck += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniLuck -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.HpRegen:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceHpRegenValue += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceHpRegenValue -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniHpRegenValue += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniHpRegenValue -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.PhysicalDamage:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GracePhysicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GracePhysicalDamage -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniPhysicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniPhysicalDamage -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.MagicalDamage:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagicalDamage -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniMagicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniMagicalDamage -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.DefensivePower:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDefensivePower += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDefensivePower -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDefensivePower += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDefensivePower -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.Speed:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceSpeed += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceSpeed -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniSpeed += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniSpeed -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.AtkSpeed:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceAttackSpeed += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceAttackSpeed -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniAttackSpeed += _grace.resultValue;
                                    else
                                        for (int i = 1; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniAttackSpeed -= _grace.resultValue;
                                }
                                break;
                    }
                    break;
                case EGraceResultWho.All:
                    if (_grace.resultWhat != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat)
                        {
                        case EGraceResultWhat.Str:

                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceStr += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceStr -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniStr += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniStr -= _grace.resultValue;
                                }

                                break;
                        case EGraceResultWhat.Dex:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDex += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDex -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDex += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDex -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.Wiz:

                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceWiz += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceWiz -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniWiz += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniWiz -= _grace.resultValue;
                                }

                                break;
                        case EGraceResultWhat.Luck:

                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceLuck += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceLuck -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniLuck += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniLuck -= _grace.resultValue;
                                }

                                break;
                        case EGraceResultWhat.HpRegen:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceHpRegenValue += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceHpRegenValue -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniHpRegenValue += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniHpRegenValue -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.PhysicalDamage:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GracePhysicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GracePhysicalDamage -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniPhysicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniPhysicalDamage -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.MagicalDamage:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagicalDamage -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniMagicalDamage += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniMagicalDamage -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.DefensivePower:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDefensivePower += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceDefensivePower -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDefensivePower += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniDefensivePower -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.Speed:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceSpeed += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceSpeed -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniSpeed += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniSpeed -= _grace.resultValue;
                                }
                                break;
                        case EGraceResultWhat.AtkSpeed:
                                if (_grace.resultValueIsPercent == 0)
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceAttackSpeed += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceAttackSpeed -= _grace.resultValue;
                                }
                                else
                                {
                                    if (_grace.resultHow == (int)EGraceResultHow.Increase)
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniAttackSpeed += _grace.resultValue;
                                    else
                                        for (int i = 0; i < characterStatuses.Count; i++)
                                            characterStatuses[i].GraceMagniAttackSpeed -= _grace.resultValue;
                                }
                                break;
                    }
                    break;
            }
        }
    }
    public void AquireGrace(CompleteGrace _grace)
    {
        graceList.Add(_grace);
        graceList[graceList.Count - 1].isActive = true;
        ActiveGrace();
    }
    public void AquireBigGrace(int _key)
    {
        if (!CheckIsActive(_key))
        {
            bigGraceList.Add(SelectBigGrace(_key));
            bigGraceList[graceList.Count - 1].isActive = true;
            ActiveGrace();
        }
        else
            Debug.Log("�̹� ��� ���");
    }
    public bool CheckIsActive(int _key)
    {
        bool isActive = false;
        Debug.Log("üũ�Ϸ�� Ű�� " + _key);
        for(int i = 0; i < graceList.Count; i++)
        {
            //if (graceList[i].graceKey == _key)
            {
                if (graceList[i].isActive)
                    isActive = true;
                else
                    isActive = false;
            }
        }
        return isActive;
    }

    //public void SetGraceAbility()
    //{
    //    characterStatuses[0].InitGraceStatus();
    //    for (int i = 0; i < graceList.Count; i++)
    //    {
    //        switch(graceList[i].graceKey)
    //        {
    //            case 0:
    //                if (characterStatuses[0].EquipmentController.EquipItems[7].attackType == "Melee")
    //                    characterStatuses[0].GracePhysicalDamage += 0.1f;
    //                break;
    //            case 1:
    //                if(characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword")
    //                    characterStatuses[0].GracePhysicalDamage += 0.1f;
    //                break;
    //            case 2:
    //                if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Axe" ||
    //                    characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Spear")
    //                    characterStatuses[0].GracePhysicalDamage += 0.1f;
    //                break;
    //            case 3:
    //                if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword")
    //                    characterStatuses[0].GraceAttackSpeed += 0.1f;
    //                break;
    //            case 4:
    //                if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword")
    //                    characterStatuses[0].GracePhysicalDamage += 0.2f;
    //                break;
    //        }
    //    }
    //}
}
