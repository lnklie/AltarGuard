using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentController : MonoBehaviour
{
    protected CharacterStatus status = null;

    protected HairSpace hairSpace = null;
    protected FaceHairSpace faceHairSpace = null;
    protected ClothSpace[] clothSpaces = null;
    protected PantSpace[] pantSpaces = null;
    protected ArmorSpace[] armorSpaces = null;
    protected BackSpace backSpace = null;
    protected HelmetSpace helmetSpace = null;
    protected WeaponSpace weaponSpace = null;
    protected SubWeaponSpace subWeaponSpace = null;

    [SerializeField] private SkillController skillController = null;
    [SerializeField] protected Item[] equipItems = new Item[9];
    [SerializeField] protected bool[] checkEquipItems = new bool[9] { false, false, false, false, false, false, false, false, false};
    [SerializeField] protected bool[] triggerEquipmentChange = new bool[9];
    #region Property
    public Item[] EquipItems { get { return equipItems; } set { equipItems = value; } }
    public CharacterStatus Status { get { return status; } }
    public bool[] CheckEquipItems { get { return checkEquipItems; } set { checkEquipItems = value; } }
    public bool[] TriggerEquipmentChange { get { return triggerEquipmentChange; } set { triggerEquipmentChange = value; } }
    #endregion
    public virtual void Awake()
    {
        status = GetComponent<CharacterStatus>();
        hairSpace = this.GetComponentInChildren<HairSpace>();
        armorSpaces = this.GetComponentsInChildren<ArmorSpace>();
        backSpace = this.GetComponentInChildren<BackSpace>();
        clothSpaces = this.GetComponentsInChildren<ClothSpace>();
        faceHairSpace = this.GetComponentInChildren<FaceHairSpace>();
        helmetSpace = this.GetComponentInChildren<HelmetSpace>();
        pantSpaces = this.GetComponentsInChildren<PantSpace>();
        subWeaponSpace = this.GetComponentInChildren<SubWeaponSpace>();
        weaponSpace = this.GetComponentInChildren<WeaponSpace>();
    }

    public int GetEquipmentDefensivePower()
    {
        // 장비에 방어력 출력
        int equipmentDefensivePower = 0;

        for (int i = 0; i < equipItems.Length; i++)
            equipmentDefensivePower += (equipItems[i] != null) ? (equipItems[i]).defensivePower : 0;

        return equipmentDefensivePower;
    }
    public int GetEquipmentPhysicDamage()
    {
        // 장비에 물리데미지 출력
        int physicDamage = 0;

        for (int i = 0; i < equipItems.Length; i++)
        {
            if (equipItems[i] != null)
                physicDamage += equipItems[i].physicalDamage;
        }
        return physicDamage;
    }
    public int GetEquipmentMagicDamage()
    {
        // 장비에 마법데미지 출력
        int magicDamage = 0;

        for (int i = 0; i < equipItems.Length; i++)
        {
            if (equipItems[i] != null)
                magicDamage += equipItems[i].magicalDamage;
        }
        return magicDamage;
    }
    public void ChangeAttackType()
    {
        // 무기 아이템에 따른 공격 타입 변경
        if (checkEquipItems[8])
        {
            status.EquipStatus[(int)EStatus.AtkRange] = equipItems[8].atkRange;
            int num = equipItems[8].itemKey / 1000;

            if (equipItems[8].attackType == "Melee")
                status.AttackType = 0f;
            else if (equipItems[8].attackType == "Ranged")
                status.AttackType = 0.5f;
            else if (equipItems[8].attackType == "Magic")
                status.AttackType = 1f;
        }
        else
            status.AttackType = 0f;
    }

    public virtual void ChangeEquipment(Item _item)
    {
        if(!_item.isEquip)
        {
            int _index = CastTo<int>.From(_item.itemType);
            status.IsSkillChange = true;
            TriggerEquipmentChange[_index] = true;

            status.TriggerStatusUpdate = true;

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
                    TakeOffWeaponBySubWeapon(equipItems[8]);
                    break;
                case EItemType.Weapon:
                    weaponSpace.ChangeItemSprite(equipItems[8].spList[0]);
                    ChangeAttackType();
                    TakeOffSubWeaponByTwoHandedWeapon(_item);
                    SkillChange();
                    break;
            }
            status.UpdateEquipAbility(equipItems);
            status.UpdateAllStatus();
            //GraceManager.Instance.UpdateGrace();
            TriggerEquipmentChange[_index] = false;
        }
        else
        {
            Debug.Log("장착 중입니다.");
        }
    }
    public void TakeOffWeaponBySubWeapon(Item _item)
    {
        if (checkEquipItems[8])
        {
            switch (_item.weaponType)
            {
                case "Spear":
                case "Axe":
                case "Bow":
                    TakeOffEquipment(equipItems[8]);
                    break;
            }
        }
    }
    public void TakeOffSubWeaponByTwoHandedWeapon(Item _item)
    {
        if (checkEquipItems[7])
        {
            switch (_item.weaponType)
            {
                case "Spear":
                case "Exe":
                case "Bow":
                    TakeOffEquipment(equipItems[7]);
                    break;
            }
        }

    }
    public void SkillChange()
    {
        for(int i = 0; i < equipItems[8].skills.Count; i++)
        {
            if (equipItems[8].skills[i] != null)
            {
                skillController.AquireSkill(equipItems[8].skills[i]);
            }
        }
    }

    public virtual void TakeOffEquipment(Item _item)
    {
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
                }
                break;
        }
        TriggerEquipmentChange[CastTo<int>.From(_item.itemType)] = true;
        status.UpdateEquipAbility(equipItems);
        status.UpdateAllStatus();
    }
    public void RemoveEquipment(int _index)
    {
        // 장착 제거
        switch (_index)
        {
            case 0:
                equipItems[0] = null;
                checkEquipItems[0] = false;
                hairSpace.ChangeItemSprite(null);
                break;
            case 1:
                equipItems[1] = null;
                checkEquipItems[1] = false;
                faceHairSpace.ChangeItemSprite(null);
                break;
            case 2:
                equipItems[2] = null;
                checkEquipItems[2] = false;
                for (int i = 0; i < clothSpaces.Length; i++)
                {
                    clothSpaces[i].ChangeItemSprite(null);
                }
                break;
            case 3:
                equipItems[3] = null;
                checkEquipItems[3] = false;
                for (int i = 0; i < pantSpaces.Length; i++)
                {
                    pantSpaces[i].ChangeItemSprite(null);
                }
                break;
            case 4:
                equipItems[4] = null;
                checkEquipItems[4] = false;
                helmetSpace.ChangeItemSprite(null);
                break;
            case 5:
                equipItems[5] = null;
                checkEquipItems[5] = false;
                for (int i = 0; i < armorSpaces.Length; i++)
                {
                    armorSpaces[i].ChangeItemSprite(null);
                }
                break;
            case 6:
                equipItems[6] = null;
                checkEquipItems[6] = false;
                backSpace.ChangeItemSprite(null);
                break;
            case 7:
                equipItems[7] = null;
                checkEquipItems[7] = false;
                subWeaponSpace.ChangeItemSprite(null);
                break;
            case 8:
                equipItems[8] = null;
                checkEquipItems[8] = false;
                weaponSpace.ChangeItemSprite(null);
                ChangeAttackType();
                break;
        }
        TriggerEquipmentChange[_index] = true;
    }
}
