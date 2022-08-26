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
            SetGraceAbility();
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
        if (_grace.resultWho != -1)
        {
            int _value = 0;
            switch ((EGraceResultWho)_grace.resultWho)
            {
                case EGraceResultWho.Player:
                    if (_grace.resultWho != -1)
                    {
                        switch ((EGraceResultWhat)_grace.resultWhat1)
                        {
                            case EGraceResultWhat.Str:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.Dex:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDex -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDex += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.Wiz:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceWiz -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceWiz += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.Luck:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceLuck -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceLuck += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.HpRegen:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceHpRegenValue += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.PhysicalDamage:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GracePhysicalDamage += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.MagicalDamage:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceMagicalDamage += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.DefensivePower:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceDefensivePower -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceDefensivePower += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.Speed:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.AtkSpeed:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                    }
                                }
                                break;
                            case EGraceResultWhat.AtkRange:
                                if (_grace.resultTarget1 != -1)
                                {
                                    switch ((EGraceResultTarget)_grace.resultTarget1)
                                    {
                                        case EGraceResultTarget.PlayerStr:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Str * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDex:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Dex * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerWiz:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Wiz * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerLuck:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].Luck * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerHpRegen:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].HpRegenValue * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerPhysicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].PhysicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerMagicalDamage:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].MagicalDamage * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerDefensivePower:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = Mathf.CeilToInt(characterStatuses[(int)ECharacter.Player].DefensivePower * (1f / _grace.resultValue1));

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                        case EGraceResultTarget.PlayerAtkSpeed:
                                            if (_grace.resultValue1IsPercent == 0)
                                                _value = _grace.resultValue1;
                                            else
                                                _value = 0;

                                            if (_grace.resultHow1 == 0)
                                                characterStatuses[_grace.resultWho].GraceStr -= _value;
                                            else
                                                characterStatuses[_grace.resultWho].GraceStr += _value;
                                            break;
                                    }
                                }
                                break;
                        }

                    }
                    break;
            }
        }
        else
        {
        }
    }
    public void AquireGrace(int _key)
    {
        if (!CheckIsActive(_key))
        {
            graceList.Add(DatabaseManager.Instance.SelectGrace(_key));
            graceList[graceList.Count - 1].isActive = true;
            SetGraceAbility();
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

    public void SetGraceAbility()
    {
        characterStatuses[0].InitGraceStatus();
        for (int i = 0; i < graceList.Count; i++)
        {
            switch(graceList[i].graceKey)
            {
                case 0:
                    if (characterStatuses[0].EquipmentController.EquipItems[7].attackType == "Melee")
                        characterStatuses[0].GracePhysicalDamage += 0.1f;
                    break;
                case 1:
                    if(characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword")
                        characterStatuses[0].GracePhysicalDamage += 0.1f;
                    break;
                case 2:
                    if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Axe" ||
                        characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Spear")
                        characterStatuses[0].GracePhysicalDamage += 0.1f;
                    break;
                case 3:
                    if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword")
                        characterStatuses[0].GraceAttackSpeed += 0.1f;
                    break;
                case 4:
                    if (characterStatuses[0].EquipmentController.EquipItems[7].weaponType == "Sword")
                        characterStatuses[0].GracePhysicalDamage += 0.2f;
                    break;
            }
        }
    }
}
