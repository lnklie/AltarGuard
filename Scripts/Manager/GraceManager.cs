using System.Collections.Generic;
using UnityEngine;

public class GraceManager : MonoBehaviour
{
    [SerializeField] private List<Grace> graceList = new List<Grace>();
    [SerializeField] private List<AllyStatus> characterStatuses = new List<AllyStatus>();


    public List<Grace> GraceList
    {
        get { return graceList; }
    }
    private void Update()
    {
        if (characterStatuses[(int)ECharacter.Player].EquipmentController.IsChangeItem)
        {

        }

    }
    public void ActiveGrace()
    {
        for(int i = 0; i< characterStatuses.Count;i++)
        {
            characterStatuses[i].InitGraceStatus();
        }

        for (int i = 0; i < graceList.Count; i++)
        {
            if (CheckGraceCondition(graceList[i]))
            {
                OperateGrace(graceList[i]);
            }
            else
            {
                Debug.Log("팔스");
            }

        }
    }
    public bool CheckGraceCondition(Grace _grace)
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Sword")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[_grace.conditionWho].EquipmentController.CheckEquipItems[8] == false)
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Spear")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Exe")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[8].weaponType == "Shield")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Bow")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Wand")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[_grace.conditionWho].EquipmentController.CheckEquipItems[8] == false)
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Exe")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[8].weaponType == "Shield")
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
                                        if (characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[_grace.conditionWho].EquipmentController.EquipItems[8].weaponType == "Shield")
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
                                        if (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Sword")
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
                                        if ((characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[1].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[2].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[3].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[4].EquipmentController.CheckEquipItems[8] == false))
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
                                        if (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Spear" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Spear" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Spear" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Spear")
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
                                        if (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Axe" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Axe" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Axe" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Axe")
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
                                        if (characterStatuses[1].EquipmentController.EquipItems[8].weaponType == "Shield" &&
                                            characterStatuses[2].EquipmentController.EquipItems[8].weaponType == "Shield" &&
                                            characterStatuses[3].EquipmentController.EquipItems[8].weaponType == "Shield" &&
                                            characterStatuses[4].EquipmentController.EquipItems[8].weaponType == "Shield")
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
                                        if (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Bow" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Bow" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Bow" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Bow")
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
                                        if (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Wand")
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
                                        if ((characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[1].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[2].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[3].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[4].EquipmentController.CheckEquipItems[8] == false))
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
                                        if ((characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Exe") &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Exe") &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Exe") &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Exe"))
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
                                        if ((characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[1].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[2].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[3].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[4].EquipmentController.EquipItems[8].weaponType == "Shield"))
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
                                        if ((characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[1].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[2].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[3].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[4].EquipmentController.EquipItems[8].weaponType == "Shield"))
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
                                        if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Sword")
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
                                        if ((characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[0].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[1].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[2].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[3].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[4].EquipmentController.CheckEquipItems[8] == false))
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
                                        if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Spear" &&
                                            characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Spear" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Spear" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Spear" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Spear")
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
                                        if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Exe" &&
                                            characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Exe" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Exe" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Exe" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Exe")
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
                                        if (characterStatuses[0].EquipmentController.EquipItems[8].weaponType == "Shield" &&
                                            characterStatuses[1].EquipmentController.EquipItems[8].weaponType == "Shield" &&
                                            characterStatuses[2].EquipmentController.EquipItems[8].weaponType == "Shield" &&
                                            characterStatuses[3].EquipmentController.EquipItems[8].weaponType == "Shield" &&
                                            characterStatuses[4].EquipmentController.EquipItems[8].weaponType == "Shield")
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
                                        if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Bow" &&
                                            characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Bow" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Bow" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Bow" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Bow")
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
                                        if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Wand")
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
                                        if ((characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[0].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[1].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[2].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[3].EquipmentController.CheckEquipItems[8] == false) &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[4].EquipmentController.CheckEquipItems[8] == false)
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
                                        if ((characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Exe") &&
                                            (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Exe") &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Exe") &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Exe") &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Spear" ||
                                            characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Exe"))
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
                                        if ((characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[0].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[1].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[2].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[3].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Sword" &&
                                            characterStatuses[4].EquipmentController.EquipItems[8].weaponType == "Shield"))
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
                                        if ((characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[0].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[1].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[1].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[2].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[2].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[3].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[3].EquipmentController.EquipItems[8].weaponType == "Shield") &&
                                            (characterStatuses[4].EquipmentController.EquipItems[7].weaponType == "Wand" &&
                                            characterStatuses[4].EquipmentController.EquipItems[8].weaponType == "Shield"))
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
    public void OperateGrace(Grace _grace)
    {
        int _value1 = 0;
        int _value2 = 0;
        if (_grace.resultWho != -1)
        {
            if(_grace.resultTarget1 != -1)
                switch ((EGraceResultTarget)_grace.resultTarget1)
                    {

                        case EGraceResultTarget.PlayerStr:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerDex:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerWiz:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerLuck:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerHpRegen:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerPhysicalDamage:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerMagicalDamage:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerDefensivePower:
                            _value1 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                            break;
                        case EGraceResultTarget.PlayerSpeed:
                            _value1 = 0;

                            break;
                        case EGraceResultTarget.PlayerAtkSpeed:
                            _value1 = 0;

                            break;
                    }
            else
                _value1 = _grace.resultValue1;
            if (_grace.resultTarget2 != -1)
                switch ((EGraceResultTarget)_grace.resultTarget2)
                    {

                        case EGraceResultTarget.PlayerStr:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerDex:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerWiz:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerLuck:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerHpRegen:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerPhysicalDamage:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerMagicalDamage:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerDefensivePower:
                            _value2 = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue2));

                            break;
                        case EGraceResultTarget.PlayerSpeed:
                            _value2 = 0;

                            break;
                        case EGraceResultTarget.PlayerAtkSpeed:
                            _value2 = 0;

                            break;
                    }
            else
                _value2 = _grace.resultValue2;

            switch ((EGraceResultWho)_grace.resultWho)
            {
                case EGraceResultWho.Player:
                case EGraceResultWho.Mercenary1:
                case EGraceResultWho.Mercenary2:
                case EGraceResultWho.Mercenary3:
                case EGraceResultWho.Mercenary4:
                    if (_grace.resultWhat1 != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat1)
                    {
                        case EGraceResultWhat.Str:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceStr -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceStr += _value1;

                            break;
                        case EGraceResultWhat.Dex:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceDex -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceDex += _value1;

                            break;
                        case EGraceResultWhat.Wiz:


                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceWiz -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceWiz += _value1;
                            break;

                        case EGraceResultWhat.Luck:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceLuck -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceLuck += _value1;
                            break;



                        case EGraceResultWhat.HpRegen:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value1;
                            break;

                        case EGraceResultWhat.PhysicalDamage:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value1;
                            else
                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value1;
                            break;

                        case EGraceResultWhat.MagicalDamage:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value1;
                            break;

                        case EGraceResultWhat.DefensivePower:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceDefensivePower -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceDefensivePower += _value1;
                            break;
                        case EGraceResultWhat.Speed:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceSpeed -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceSpeed += _value1;
                            break;
                        case EGraceResultWhat.AtkSpeed:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceAttackSpeed -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceAttackSpeed += _value1;
                            break;
                        case EGraceResultWhat.AtkRange:

                            if (_grace.resultHow1 == 0)
                                characterStatuses[_grace.resultWho].GraceAtkRange -= _value1;
                            else
                                characterStatuses[_grace.resultWho].GraceAtkRange += _value1;
                            break;
                    }

                    if(_grace.resultWhat2 != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat1)
                    {
                        case EGraceResultWhat.Str:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceStr -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceStr += _value2;

                            break;
                        case EGraceResultWhat.Dex:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceDex -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceDex += _value2;

                            break;
                        case EGraceResultWhat.Wiz:


                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceWiz -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceWiz += _value2;
                            break;

                        case EGraceResultWhat.Luck:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceLuck -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceLuck += _value2;
                            break;



                        case EGraceResultWhat.HpRegen:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value2;
                            break;

                        case EGraceResultWhat.PhysicalDamage:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value2;
                            break;

                        case EGraceResultWhat.MagicalDamage:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value2;
                            break;

                        case EGraceResultWhat.DefensivePower:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceDefensivePower -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceDefensivePower += _value2;
                            break;
                        case EGraceResultWhat.Speed:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceSpeed -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceSpeed += _value2;
                            break;
                        case EGraceResultWhat.AtkSpeed:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceAttackSpeed -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceAttackSpeed += _value2;
                            break;
                        case EGraceResultWhat.AtkRange:

                            if (_grace.resultHow2 == 0)
                                characterStatuses[_grace.resultWho].GraceAtkRange -= _value2;
                            else
                                characterStatuses[_grace.resultWho].GraceAtkRange += _value2;
                            break;
                    }
                    break;
                case EGraceResultWho.AllMercenary:
                    if (_grace.resultWhat1 != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat1)
                    {
                        case EGraceResultWhat.Str:


                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStr -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStr += _value1;

                            break;
                        case EGraceResultWhat.Dex:

                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDex -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDex += _value1;

                            break;
                        case EGraceResultWhat.Wiz:

                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceWiz -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceWiz += _value1;

                            break;
                        case EGraceResultWhat.Luck:

                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceLuck -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceLuck += _value1;

                            break;
                        case EGraceResultWhat.HpRegen:

                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceHpRegenValue -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceHpRegenValue += _value1;

                            break;
                        case EGraceResultWhat.PhysicalDamage:
                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GracePhysicalDamage -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GracePhysicalDamage += _value1;
                            break;
                        case EGraceResultWhat.MagicalDamage:
                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagicalDamage -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagicalDamage += _value1;
                            break;
                        case EGraceResultWhat.DefensivePower:
                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDefensivePower -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDefensivePower += _value1;
                            break;
                        case EGraceResultWhat.Speed:
                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceSpeed -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceSpeed += _value1;
                            break;
                        case EGraceResultWhat.AtkSpeed:
                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAttackSpeed -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAttackSpeed += _value1;
                            break;
                        case EGraceResultWhat.AtkRange:
                            if (_grace.resultHow1 == 0)
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAtkRange -= _value1;
                            else
                                for (int i = 1; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAtkRange += _value1;
                            break;
                    }

                    if (_grace.resultWhat2 != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat1)
                        {
                            case EGraceResultWhat.Str:


                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStr -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStr += _value2;

                                break;
                            case EGraceResultWhat.Dex:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDex -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDex += _value2;

                                break;
                            case EGraceResultWhat.Wiz:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceWiz -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceWiz += _value2;

                                break;
                            case EGraceResultWhat.Luck:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceLuck -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceLuck += _value2;

                                break;
                            case EGraceResultWhat.HpRegen:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceHpRegenValue -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceHpRegenValue += _value2;

                                break;
                            case EGraceResultWhat.PhysicalDamage:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GracePhysicalDamage -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GracePhysicalDamage += _value2;
                                break;
                            case EGraceResultWhat.MagicalDamage:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagicalDamage -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagicalDamage += _value2;
                                break;
                            case EGraceResultWhat.DefensivePower:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDefensivePower -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDefensivePower += _value2;
                                break;
                            case EGraceResultWhat.Speed:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceSpeed -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceSpeed += _value2;
                                break;
                            case EGraceResultWhat.AtkSpeed:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAttackSpeed -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAttackSpeed += _value2;
                                break;
                            case EGraceResultWhat.AtkRange:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAtkRange -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAtkRange += _value2;
                                break;
                        }
                    break;
                case EGraceResultWho.All:
                    if (_grace.resultWhat1 != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat1)
                    {
                        case EGraceResultWhat.Str:

                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStr -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceStr += _value1;

                            break;
                        case EGraceResultWhat.Dex:

                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDex -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDex += _value1;

                            break;
                        case EGraceResultWhat.Wiz:

                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceWiz -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceWiz += _value1;

                            break;
                        case EGraceResultWhat.Luck:

                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceLuck -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceLuck += _value1;

                            break;
                        case EGraceResultWhat.HpRegen:

                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceHpRegenValue -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceHpRegenValue += _value1;

                            break;
                        case EGraceResultWhat.PhysicalDamage:
                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GracePhysicalDamage -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GracePhysicalDamage += _value1;
                            break;
                        case EGraceResultWhat.MagicalDamage:
                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagicalDamage -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceMagicalDamage += _value1;
                            break;
                        case EGraceResultWhat.DefensivePower:
                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDefensivePower -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceDefensivePower += _value1;
                            break;
                        case EGraceResultWhat.Speed:
                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceSpeed -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceSpeed += _value1;
                            break;
                        case EGraceResultWhat.AtkSpeed:
                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAttackSpeed -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAttackSpeed += _value1;
                            break;
                        case EGraceResultWhat.AtkRange:
                            if (_grace.resultHow1 == 0)
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAtkRange -= _value1;
                            else
                                for (int i = 0; i < characterStatuses.Count; i++)
                                    characterStatuses[i].GraceAtkRange += _value1;
                            break;
                    }

                    if (_grace.resultWhat2 != -1)
                        switch ((EGraceResultWhat)_grace.resultWhat1)
                        {
                            case EGraceResultWhat.Str:


                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStr -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceStr += _value2;

                                break;
                            case EGraceResultWhat.Dex:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDex -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDex += _value2;

                                break;
                            case EGraceResultWhat.Wiz:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceWiz -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceWiz += _value2;

                                break;
                            case EGraceResultWhat.Luck:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceLuck -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceLuck += _value2;

                                break;
                            case EGraceResultWhat.HpRegen:

                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceHpRegenValue -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceHpRegenValue += _value2;

                                break;
                            case EGraceResultWhat.PhysicalDamage:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GracePhysicalDamage -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GracePhysicalDamage += _value2;
                                break;
                            case EGraceResultWhat.MagicalDamage:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagicalDamage -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceMagicalDamage += _value2;
                                break;
                            case EGraceResultWhat.DefensivePower:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDefensivePower -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceDefensivePower += _value2;
                                break;
                            case EGraceResultWhat.Speed:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceSpeed -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceSpeed += _value2;
                                break;
                            case EGraceResultWhat.AtkSpeed:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAttackSpeed -= _value2;
                                else
                                    for (int i = 1; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAttackSpeed += _value2;
                                break;
                            case EGraceResultWhat.AtkRange:
                                if (_grace.resultHow2 == 0)
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAtkRange -= _value2;
                                else
                                    for (int i = 0; i < characterStatuses.Count; i++)
                                        characterStatuses[i].GraceAtkRange += _value2;
                                break;
                        }
                    break;
            }
        }
        else
        {
        }
        Debug.Log("값은 " + _value1);
    }
    public void AquireGrace(int _key)
    {
        if (!CheckIsActive(_key))
        {
            graceList.Add(DatabaseManager.Instance.SelectGrace(_key));
            graceList[graceList.Count - 1].isActive = true;
            ActiveGrace();
        }
        else
            Debug.Log("이미 배운 은총");
    }
    
    public bool CheckIsActive(int _key)
    {
        bool isActive = false;
        Debug.Log("체크하려는 키는 " + _key);
        for(int i = 0; i < graceList.Count; i++)
        {
            if (graceList[i].graceKey == _key)
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
