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
                if(characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace1 != null)
                    AquireGrace(characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace1);
                else
                    
                if (characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace2 != null)
                    AquireGrace(characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace2);
                if (characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace3 != null)
                    AquireGrace(characterEquipmentController[(int)ECharacter.Player].EquipItems[i].grace3);

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
                Debug.Log("작동");
                bigGraceList[i].isActive = true;
            }
            else
            {
                bigGraceList[i].isActive = false;
                Debug.Log("팔스");
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
