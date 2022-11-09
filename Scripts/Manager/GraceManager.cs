using System.Collections.Generic;
using UnityEngine;

public class GraceManager : MonoBehaviour
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

    public static GraceManager Instance = null;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetGraceList();
    }


    private void Update()
    {
        for(int i = 0; i < 9; i++)
        {
            if (characterEquipmentController[(int)ECharacter.Player].TriggerEquipmentChange[i])
            {
                for(int j = 0; j < characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace.Count; j++)
                {
                    AquireGrace(characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace[j]);
                }

                ActiveGrace();
                characterEquipmentController[(int)ECharacter.Player].TriggerEquipmentChange[i] = false;
            }
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
                    //Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 1:
                for (int i = 0; i < graceConditionWhatList.Count; i++)
                {
                    if (graceConditionWhatList[i].graceKey == _key)
                        _grace = graceConditionWhatList[i];
                    //else
                    //Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 2:
                for (int i = 0; i < graceConditionHowList.Count; i++)
                {
                    if (graceConditionHowList[i].graceKey == _key)
                        _grace = graceConditionHowList[i];
                    //else
                    //Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 3:
                for (int i = 0; i < graceResultWhoList.Count; i++)
                {
                    if (graceResultWhoList[i].graceKey == _key)
                        _grace = graceResultWhoList[i];
                    //else
                    //Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 4:
                for (int i = 0; i < graceResultWhatList.Count; i++)
                {
                    if (graceResultWhatList[i].graceKey == _key)
                        _grace = graceResultWhatList[i];
                    //else
                    //Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 5:
                for (int i = 0; i < graceResultIsPercentList.Count; i++)
                {
                    if (graceResultIsPercentList[i].graceKey == _key)
                        _grace = graceResultIsPercentList[i];
                    //else
                    //Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 6:
                for (int i = 0; i < graceResultHowList.Count; i++)
                {
                    if (graceResultHowList[i].graceKey == _key)
                        _grace = graceResultHowList[i];
                    //else
                    //Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
        }
        return _grace;
    }
    public string AddGraceExplain(CompleteGrace _completeGrace)
    {
        string _explain = null;
        if (_completeGrace.conditionWho != -1)
        {
            _explain = SelectGrace(_completeGrace.conditionWho).graceKorName + "가(이) ";
            if (_completeGrace.conditionWhat < 1014)
            {
                _explain += SelectGrace(_completeGrace.conditionWhat).graceKorName + "을(를) ";
            }
            else
            {
                _explain += SelectGrace(_completeGrace.conditionWhat).graceKorName + "이 ";
                _explain += _completeGrace.conditionValue;
            }
            _explain += SelectGrace(_completeGrace.conditionHow).graceKorName + ", ";
        }
        if (_completeGrace.relationOfVariables == 7000)
        {
            _explain += SelectGrace(_completeGrace.resultWho1).graceKorName + "의 ";
            _explain += SelectGrace(_completeGrace.resultWhat1).graceKorName + "을(를) ";
            if (_completeGrace.resultValueIsPercent1 == 0)
                _explain += _completeGrace.resultValue1 + " 만큼 ";
            else
                _explain += _completeGrace.resultValue1 + "% 만큼 ";
            if (_completeGrace.resultWho2 == -1)
                _explain += SelectGrace(_completeGrace.resultHow1).graceKorName + "시킨다";
            else
            {
                _explain += SelectGrace(_completeGrace.resultHow1).graceKorName + "시키고 ";
                _explain += SelectGrace(_completeGrace.resultWho2).graceKorName + "의 ";
                _explain += SelectGrace(_completeGrace.resultWhat2).graceKorName + "을(를) ";
                if (_completeGrace.resultValueIsPercent2 == 0)
                    _explain += _completeGrace.resultValue2 + " 만큼 ";
                else
                    _explain += _completeGrace.resultValue2 + "% 만큼 ";
                _explain += SelectGrace(_completeGrace.resultHow2).graceKorName + "시킨다";
            }
        }
        else
        {
            _explain += SelectGrace(_completeGrace.resultWho1).graceKorName + "의 ";
            _explain += SelectGrace(_completeGrace.resultWhat1).graceKorName + "을(를) ";
            if (_completeGrace.resultValueIsPercent1 == 0)
                _explain += _completeGrace.resultValue1 + " 만큼 ";
            else
                _explain += _completeGrace.resultValue1 + "% 만큼 ";
            if (_completeGrace.resultHow1 == 6001)
                _explain += SelectGrace(_completeGrace.resultHow1).graceKorName + "시키고 그 만큼 ";

            _explain += SelectGrace(_completeGrace.resultWho2).graceKorName + "의 ";
            _explain += SelectGrace(_completeGrace.resultWhat2).graceKorName + "을(를) ";
            _explain += SelectGrace(_completeGrace.resultHow2).graceKorName + "시킨다";
        }
        return _explain;
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
        else if (conditionWhat > 10)
            conditionHow = 1;
        else
            conditionHow = 0;
        int resultWho1 = Random.Range(0, graceResultWhoList.Count);
        int resultWho2 = Random.Range(0, graceResultWhoList.Count);
        int resultValue1 = Random.Range(0, 10);
        int resultValue2 = Random.Range(0, 10);
        int resultWhat1 = Random.Range(0, graceResultWhatList.Count);
        int resultWhat2 = Random.Range(0, graceResultWhatList.Count);
        int resultValueIsPercent1 = Random.Range(0, 2);
        int resultValueIsPercent2 = Random.Range(0, 2);
        int relationOfGraces = Random.Range(0, 2);
        float weightedValue1 = 0;
        float weightedValue2 = 0;
        if (conditionWho != -1)
        {
            weightedValue1 =
                SelectGrace(conditionWho).weightedValue * SelectGrace(conditionWhat + 1000).weightedValue
             * SelectGrace(resultWho1 + 3000).weightedValue * SelectGrace(resultWhat1 + 4000).weightedValue * SelectGrace(resultValueIsPercent1 + 5000).weightedValue;
            weightedValue2 =
                SelectGrace(conditionWho).weightedValue * SelectGrace(conditionWhat + 1000).weightedValue
             * SelectGrace(resultWho2 + 3000).weightedValue * SelectGrace(resultWhat2 + 4000).weightedValue * SelectGrace(resultValueIsPercent2 + 5000).weightedValue;
        }
        else
        {
            weightedValue1 = SelectGrace(resultWho1 + 3000).weightedValue * SelectGrace(resultWhat1 + 4000).weightedValue * SelectGrace(resultValueIsPercent1 + 5000).weightedValue;
            weightedValue2 = SelectGrace(resultWho2 + 3000).weightedValue * SelectGrace(resultWhat2 + 4000).weightedValue * SelectGrace(resultValueIsPercent2 + 5000).weightedValue;
        }
        CompleteGrace _completeGrace = new CompleteGrace(
            conditionWho, conditionWhat + 1000, conditionValue, conditionHow + 2000, resultWho1 + 3000, resultWho2 + 3000, resultWhat1 + 4000, resultWhat2 + 4000,
            Mathf.CeilToInt(resultValue1 * weightedValue1), Mathf.CeilToInt(resultValue2 * weightedValue2), resultValueIsPercent1 + 5000, resultValueIsPercent2 + 5000, 6000, 6000, relationOfGraces + 7000);
        _completeGrace.explain = AddGraceExplain(_completeGrace);

        return _completeGrace;
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
                graceList[i].isActive = true;
            }
            else
            {
                graceList[i].isActive = false;
            }

        }

        for(int i = 0; i < bigGraceList.Count; i++)
        {
            if (CheckGraceCondition(bigGraceList[i]))
            {
                OperateGrace(bigGraceList[i]);
                bigGraceList[i].isActive = true;
            }
            else
            {
                bigGraceList[i].isActive = false;
            }
        }
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Sword")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[_grace.conditionWho].CheckEquipItems[7] == false)
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Spear")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Exe")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Shield")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Bow")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Wand")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[_grace.conditionWho].CheckEquipItems[7] == false)
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Exe")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Shield")
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
                                        if (characterEquipmentController[_grace.conditionWho].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[_grace.conditionWho].EquipItems[7].weaponType == "Shield")
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Str:
                                switch((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Str] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Str] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Str] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Dex:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Wiz:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Luck:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[_grace.conditionWho].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue)
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
                                        if (characterEquipmentController[1].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Sword")
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
                                        if ((characterEquipmentController[1].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[1].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[2].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[3].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[4].CheckEquipItems[7] == false))
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
                                        if (characterEquipmentController[1].EquipItems[8].weaponType == "Spear" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Spear" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Spear" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Spear")
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
                                        if (characterEquipmentController[1].EquipItems[8].weaponType == "Axe" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Axe" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Axe" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Axe")
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
                                        if (characterEquipmentController[1].EquipItems[7].weaponType == "Shield" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Shield" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Shield" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Shield")
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
                                        if (characterEquipmentController[1].EquipItems[8].weaponType == "Bow" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Bow" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Bow" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Bow")
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
                                        if (characterEquipmentController[1].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Wand")
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
                                        if ((characterEquipmentController[1].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[1].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[2].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[3].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[4].CheckEquipItems[7] == false))
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
                                        if ((characterEquipmentController[1].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Exe") &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Exe") &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Exe") &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Exe"))
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
                                        if ((characterEquipmentController[1].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Shield"))
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
                                        if ((characterEquipmentController[1].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Shield"))
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Str:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Str] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Str] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Str] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Str] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Str] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Str] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Str] ==  _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Str] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Str] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Str] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Str] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Str] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Dex:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Wiz:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Luck:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[1].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue)
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
                                        if (characterEquipmentController[0].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Sword")
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
                                        if ((characterEquipmentController[0].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[0].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[1].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[1].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[2].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[3].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[4].CheckEquipItems[7] == false))
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
                                        if (characterEquipmentController[0].EquipItems[8].weaponType == "Spear" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Spear" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Spear" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Spear" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Spear")
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
                                        if (characterEquipmentController[0].EquipItems[8].weaponType == "Exe" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Exe" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Exe" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Exe" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Exe")
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
                                        if (characterEquipmentController[0].EquipItems[7].weaponType == "Shield" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Shield" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Shield" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Shield" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Shield")
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
                                        if (characterEquipmentController[0].EquipItems[8].weaponType == "Bow" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Bow" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Bow" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Bow" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Bow")
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
                                        if (characterEquipmentController[0].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Wand")
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
                                        if ((characterEquipmentController[0].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[0].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[1].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[1].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[2].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[3].CheckEquipItems[7] == false) &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[4].CheckEquipItems[7] == false)
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
                                        if ((characterEquipmentController[0].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[0].EquipItems[8].weaponType == "Exe") &&
                                            (characterEquipmentController[1].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[1].EquipItems[8].weaponType == "Exe") &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[2].EquipItems[8].weaponType == "Exe") &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[3].EquipItems[8].weaponType == "Exe") &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Spear" ||
                                            characterEquipmentController[4].EquipItems[8].weaponType == "Exe"))
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
                                        if ((characterEquipmentController[0].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[0].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[1].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Sword" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Shield"))
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
                                        if ((characterEquipmentController[0].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[0].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[1].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[1].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[2].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[2].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[3].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[3].EquipItems[7].weaponType == "Shield") &&
                                            (characterEquipmentController[4].EquipItems[8].weaponType == "Wand" &&
                                            characterEquipmentController[4].EquipItems[7].weaponType == "Shield"))
                                        {
                                            _bool = true;
                                        }
                                        break;

                                }
                                break;
                            case EGraceConditionWhat.Str:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Str] > _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Str] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Str] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Str] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Str] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Str] == _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Str] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Str] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Str] == _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Str] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Str] < _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Str] < _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Str] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Str] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Str] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Dex:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Dex] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Dex] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Dex] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Wiz:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Wiz] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Wiz] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Wiz] < _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                }
                                break;
                            case EGraceConditionWhat.Luck:
                                switch ((EGraceConditionHow)_grace.conditionHow)
                                {
                                    case EGraceConditionHow.More:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Luck] > _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Same:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Luck] == _grace.conditionValue)
                                        {
                                            _bool = true;
                                        }
                                        break;
                                    case EGraceConditionHow.Less:
                                        if (characterStatuses[0].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue &&
                                            characterStatuses[1].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue &&
                                            characterStatuses[2].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue &&
                                            characterStatuses[3].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue &&
                                            characterStatuses[4].TotalStatus[(int)EStatus.Luck] < _grace.conditionValue)
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
        if (_grace.relationOfVariables == 7000)
        {

            if (_grace.resultWho1 != -1)
            {
                switch ((EGraceResultWho)_grace.resultWho1)
                {
                    case EGraceResultWho.Player:
                    case EGraceResultWho.Mercenary1:
                    case EGraceResultWho.Mercenary2:
                    case EGraceResultWho.Mercenary3:
                    case EGraceResultWho.Mercenary4:

                        if (_grace.resultWhat1 != -1)
                            if (_grace.resultValueIsPercent1 == 5000)
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                            else
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho1 - 3000].GraceMagniStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    characterStatuses[_grace.resultWho1 - 3000].GraceMagniStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                        break;
                    case EGraceResultWho.AllMercenary:
                        if (_grace.resultWhat1 != -1)
                            if (_grace.resultValueIsPercent1 == 5000)
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                            else
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                        break;

                    case EGraceResultWho.All:
                        if (_grace.resultWhat1 != -1)
                            if (_grace.resultValueIsPercent1 == 5000)
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                            else
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                        break;
                }
            }
        }
        if (_grace.resultWho2 != -1)
        {
            switch ((EGraceResultWho)_grace.resultWho2)
            {
                case EGraceResultWho.Player:
                case EGraceResultWho.Mercenary1:
                case EGraceResultWho.Mercenary2:
                case EGraceResultWho.Mercenary3:
                case EGraceResultWho.Mercenary4:

                    if (_grace.resultWhat2 != -1)
                        if (_grace.resultValueIsPercent2 == 5000)
                        {
                            if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                            else
                                characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                        }
                        else
                        {
                            if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                            else
                                characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                        }
                    break;
                case EGraceResultWho.AllMercenary:
                    if (_grace.resultWhat2 != -1)
                        if (_grace.resultValueIsPercent2 == 5000)
                        {
                            if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                        }
                        else
                        {
                            if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                        }
                    break;

                case EGraceResultWho.All:
                    if (_grace.resultWhat2 != -1)
                        if (_grace.resultValueIsPercent2 == 5000)
                        {
                            if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                        }
                        else
                        {
                            if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                        }
                    break;
            }
        }
        else
        {
            if (_grace.resultWho2 != -1)
            {
                switch ((EGraceResultWho)_grace.resultWho1)
                {
                    case EGraceResultWho.Player:
                    case EGraceResultWho.Mercenary1:
                    case EGraceResultWho.Mercenary2:
                    case EGraceResultWho.Mercenary3:
                    case EGraceResultWho.Mercenary4:
                        if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                        {
                            _grace.resultValue2 = (int)(characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                        }
                        else
                        {
                            characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] -=
                                (int)(characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                            _grace.resultValue2 = (int)(characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                        }
                        break;
                    case EGraceResultWho.AllMercenary:
                        if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                        {
                            for (int i = 1; i < characterStatuses.Count; i++)
                                _grace.resultValue2 = (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                        }
                        else
                        {
                            for (int i = 1; i < characterStatuses.Count; i++)
                            {
                                characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -=
                                (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                _grace.resultValue2 += (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                            }
                        }
                        break;
                    case EGraceResultWho.All:
                        if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                        {
                            for (int i = 0; i < characterStatuses.Count; i++)
                                _grace.resultValue2 = (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                        }
                        else
                        {
                            for (int i = 0; i < characterStatuses.Count; i++)
                            {
                                characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -=
                                (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                _grace.resultValue2 += (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                            }
                        }
                        break;

                }

                switch ((EGraceResultWho)_grace.resultWho2)
                {
                    case EGraceResultWho.Player:
                    case EGraceResultWho.Mercenary1:
                    case EGraceResultWho.Mercenary2:
                    case EGraceResultWho.Mercenary3:
                    case EGraceResultWho.Mercenary4:

                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;
                    case EGraceResultWho.AllMercenary:
                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;

                    case EGraceResultWho.All:
                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;
                }
            }
        }
    }
    public void OperateGrace(BigGrace _grace)
    {
        if (_grace.relationOfVariables == 7000)
        {
            if (_grace.resultWho1 != -1)
            {
                switch ((EGraceResultWho)_grace.resultWho1)
                {
                    case EGraceResultWho.Player:
                    case EGraceResultWho.Mercenary1:
                    case EGraceResultWho.Mercenary2:
                    case EGraceResultWho.Mercenary3:
                    case EGraceResultWho.Mercenary4:
                    
                        if (_grace.resultWhat1 != -1)
                            if (_grace.resultValueIsPercent1 == 5000)
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                            else
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho1 - 3000].GraceMagniStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    characterStatuses[_grace.resultWho1 - 3000].GraceMagniStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                        break;
                    case EGraceResultWho.AllMercenary:
                        if (_grace.resultWhat1 != -1)
                            if (_grace.resultValueIsPercent1 == 5000)
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                            else
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                        break;

                    case EGraceResultWho.All:
                        if (_grace.resultWhat1 != -1)
                            if (_grace.resultValueIsPercent1 == 5000)
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                            else
                            {
                                if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] += _grace.resultValue1;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat1 - 4000] -= _grace.resultValue1;
                            }
                        break;
                }
            }
            if (_grace.resultWho2 != -1)
            {
                switch ((EGraceResultWho)_grace.resultWho2)
                {
                    case EGraceResultWho.Player:
                    case EGraceResultWho.Mercenary1:
                    case EGraceResultWho.Mercenary2:
                    case EGraceResultWho.Mercenary3:
                    case EGraceResultWho.Mercenary4:

                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;
                    case EGraceResultWho.AllMercenary:
                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;

                    case EGraceResultWho.All:
                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;
                }
            }
        }
        else
        {
            if (_grace.resultWho2 != -1)
            {
                switch ((EGraceResultWho)_grace.resultWho1)
                                    {
                                        case EGraceResultWho.Player:
                                        case EGraceResultWho.Mercenary1:
                                        case EGraceResultWho.Mercenary2:
                                        case EGraceResultWho.Mercenary3:
                                        case EGraceResultWho.Mercenary4:
                                            if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                            {
                                                _grace.resultValue2 = (int)(characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                            }
                                            else
                                            {
                                                characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] -=
                                                    (int)(characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                                _grace.resultValue2 = (int)(characterStatuses[_grace.resultWho1 - 3000].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                            }
                                            break;
                                        case EGraceResultWho.AllMercenary:
                                            if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                            {
                                                for (int i = 1; i < characterStatuses.Count; i++)
                                                    _grace.resultValue2 = (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                            }
                                            else
                                            {
                                                for (int i = 1; i < characterStatuses.Count; i++)
                                                {
                                                    characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -=
                                                    (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                                   _grace.resultValue2 += (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                                }
                                            }
                                            break;
                                        case EGraceResultWho.All:
                                            if (_grace.resultHow1 == (int)EGraceResultHow.Increase)
                                            {
                                                for (int i = 0; i < characterStatuses.Count; i++)
                                                    _grace.resultValue2 = (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                            }
                                            else
                                            {
                                                for (int i = 0; i < characterStatuses.Count; i++)
                                                {
                                                    characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] -=
                                                    (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                                    _grace.resultValue2 += (int)(characterStatuses[i].GraceStatuses[_grace.resultWhat1 - 4000] * (_grace.resultValue1 / 100f));
                                                }
                                            }
                                            break;

                                    }

                switch ((EGraceResultWho)_grace.resultWho2)
                {
                    case EGraceResultWho.Player:
                    case EGraceResultWho.Mercenary1:
                    case EGraceResultWho.Mercenary2:
                    case EGraceResultWho.Mercenary3:
                    case EGraceResultWho.Mercenary4:

                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    characterStatuses[_grace.resultWho2 - 3000].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    characterStatuses[_grace.resultWho2 - 3000].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;
                    case EGraceResultWho.AllMercenary:
                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;

                    case EGraceResultWho.All:
                        if (_grace.resultWhat2 != -1)
                            if (_grace.resultValueIsPercent2 == 5000)
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                            else
                            {
                                if (_grace.resultHow2 == (int)EGraceResultHow.Increase)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] += _grace.resultValue2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagniStatuses[_grace.resultWhat2 - 4000] -= _grace.resultValue2;
                            }
                        break;
                }
            }
        }
    }
    public void AquireGrace(CompleteGrace _grace)
    {
        graceList.Add(_grace);
        ActiveGrace();
    }
    public void AquireBigGrace(int _key)
    {
        if (!CheckIsReceive(_key))
        {
            bigGraceList.Add(DatabaseManager.Instance.SelectBigGrace(_key));
            ActiveGrace();
        }
        else
            Debug.Log("이미 배운 은총");
    }
    public bool CheckIsReceive(int _key)
    {
        bool isReceive = false;
        if(bigGraceList.Count > 0)
            for(int i = 0; i < bigGraceList.Count; i++)
            {
                if (bigGraceList[i].bigGraceKey == _key)
                {
                    isReceive = true;
                }
            }
        return isReceive;
    }

        
}
