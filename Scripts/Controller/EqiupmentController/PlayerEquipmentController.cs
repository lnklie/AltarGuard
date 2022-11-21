using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentController : AllyEquipmentController
{


    public override void ChangeEquipment(Item _item)
    {
        if (!_item.isEquip)
        {
            int _index = CastTo<int>.From(_item.itemType);
            ally.IsSkillChange = true;
            ally.TriggerStatusUpdate = true;
            TriggerEquipmentChange[_index] = true;
            equipItems[_index] = _item;
            equipItems[_index].isEquip = true;
            checkEquipItems[_index] = true;
            switch (_item.itemType)
            {
                case EItemType.Hair:
                    hairSpace.ChangeItemSprite(equipItems[0].spList[0]);
                    break;
                case EItemType.FaceHair:
                    faceHairSpace.ChangeItemSprite(equipItems[1].spList[0]);
                    break;
                case EItemType.Cloth:
                    //for (int i = 0; i < clothSpaces.Length; i++)
                    //{
                    //    if(equipItems[2].spList[i] != null)
                    //        clothSpaces[i].ChangeItemSprite(equipItems[2].spList[i]);
                    //}
                    break;
                case EItemType.Pant:
                    for (int i = 0; i < pantSpaces.Length; i++)
                    {
                        pantSpaces[i].ChangeItemSprite(equipItems[3].spList[i]);
                    }
                    break;
                case EItemType.Helmet:
                    helmetSpace.ChangeItemSprite(equipItems[4].spList[0]);
                    break;
                case EItemType.Armor:
                    for (int i = 0; i < armorSpaces.Length; i++)
                    {
                        armorSpaces[i].ChangeItemSprite(equipItems[5].spList[i]);
                    }
                    break;
                case EItemType.Back:
                    backSpace.ChangeItemSprite(equipItems[6].spList[0]);
                    break;
                case EItemType.SubWeapon:
                    subWeaponSpace.ChangeItemSprite(equipItems[7].spList[0]);
                    break;
                case EItemType.Weapon:
                    weaponSpace.ChangeItemSprite(equipItems[8].spList[0]);
                    ChangeAttackType();
                    SkillChange();
                    UIManager.Instance.UpdateSkillSlot();
                    break;
            }
            ally.UpdateEquipAbility(equipItems);
            ally.UpdateAllStatus();
            GraceManager.Instance.AquireEquipmentGrace(ally.AllyNum, _index);
            TriggerEquipmentChange[_index] = false;
        }
        else
        {
            Debug.Log("장착 중입니다.");
        }
    }
    public override void TakeOffEquipment(Item _item)
    {
        int _index = CastTo<int>.From(_item.itemType);
        GraceManager.Instance.RemoveEquipmentGrace(ally.AllyNum, _index);
        
        switch (_item.itemType)
        {
            case EItemType.Hair:
                if (checkEquipItems[0])
                {
                    equipItems[0].isEquip = false;
                    equipItems[0].equipCharNum = -1;
                    equipItems[0] = null;
                    checkEquipItems[0] = false;
                    hairSpace.ChangeItemSprite(null);
                }
                break;
            case EItemType.FaceHair:
                if (checkEquipItems[1])
                {
                    equipItems[1].isEquip = false;
                    equipItems[1].equipCharNum = -1;
                    equipItems[1] = null;
                    checkEquipItems[1] = false;
                    faceHairSpace.ChangeItemSprite(null);
                }
                break;
            case EItemType.Cloth:
                if (checkEquipItems[2])
                {
                    equipItems[2].isEquip = false;
                    equipItems[2].equipCharNum = -1;
                    equipItems[2] = null;
                    checkEquipItems[2] = false;
                    for (int i = 0; i < clothSpaces.Length; i++)
                    {
                        clothSpaces[i].ChangeItemSprite(null);
                    }
                }
                break;
            case EItemType.Pant:
                if (checkEquipItems[3])
                {
                    equipItems[3].isEquip = false;
                    equipItems[3].equipCharNum = -1;
                    equipItems[3] = null;
                    checkEquipItems[3] = false;
                    for (int i = 0; i < pantSpaces.Length; i++)
                    {
                        pantSpaces[i].ChangeItemSprite(null);
                    }
                }
                break;
            case EItemType.Helmet:
                if (checkEquipItems[4])
                {
                    equipItems[4].isEquip = false;
                    equipItems[4].equipCharNum = -1;
                    equipItems[4] = null;
                    checkEquipItems[4] = false;
                    helmetSpace.ChangeItemSprite(null);
                }
                break;
            case EItemType.Armor:
                if (checkEquipItems[5])
                {
                    equipItems[5].isEquip = false;
                    equipItems[5].equipCharNum = -1;
                    equipItems[5] = null;
                    checkEquipItems[5] = false;
                    for (int i = 0; i < armorSpaces.Length; i++)
                    {
                        armorSpaces[i].ChangeItemSprite(null);
                    }
                }
                break;
            case EItemType.Back:
                if (checkEquipItems[6])
                {
                    equipItems[6].isEquip = false;
                    equipItems[6].equipCharNum = -1;
                    equipItems[6] = null;
                    checkEquipItems[6] = false;
                    backSpace.ChangeItemSprite(null);
                }
                break;
            case EItemType.SubWeapon:
                if (checkEquipItems[7])
                {
                    equipItems[7].isEquip = false;
                    equipItems[7].equipCharNum = -1;
                    equipItems[7] = null;
                    checkEquipItems[7] = false;
                    subWeaponSpace.ChangeItemSprite(null);
                }
                break;
            case EItemType.Weapon:
                if (checkEquipItems[8])
                {
                    equipItems[8].isEquip = false;
                    equipItems[8].equipCharNum = -1;
                    equipItems[8] = null;
                    checkEquipItems[8] = false;
                    weaponSpace.ChangeItemSprite(null);
                    ChangeAttackType();
                    Debug.Log("응냐냐냐");
                    UIManager.Instance.InitSkillSlot();
                }
                break;
        }
        TriggerEquipmentChange[_index] = true;
        status.UpdateEquipAbility(equipItems);
        status.UpdateAllStatus();
    }
}
