using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-07
 * 작성자 : Inklie
 * 파일명 : InventoryManager.cs
==============================
*/
public class InventoryManager : SingletonManager<InventoryManager>
{
    [SerializeField]
    private List<Item> inventroyWeaponItems = new List<Item>();
    public List<Item> InventroyWeaponItems
    {
        get { return inventroyWeaponItems; }
    }

    [SerializeField]
    private List<Item> inventroyEquipmentItems = new List<Item>();
    public List<Item> InventroyEquipmentItems
    {
        get { return inventroyEquipmentItems; }
    }

    [SerializeField]
    private List<Item> inventroyConsumableItems = new List<Item>();
    public List<Item> InventroyConsumableItems
    {
        get { return inventroyConsumableItems; }
    }

    [SerializeField]
    private List<Item> inventroyMiscellaneousItems = new List<Item>();
    public List<Item> InventroyMiscellaneousItems
    {
        get { return inventroyMiscellaneousItems; }
    }

    [SerializeField]
    private List<Item> inventroyDecorationItems = new List<Item>();
    public List<Item> InventroyDecorationItems
    {
        get { return inventroyDecorationItems; }
    }

    private void Start()
    {
        AcquireItem(DatabaseManager.Instance.SelectItem(7001), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(7002), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(7003), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(11000), 3);
        AcquireItem(DatabaseManager.Instance.SelectItem(11001),3);
        AcquireItem(DatabaseManager.Instance.SelectItem(11002), 3);
        AcquireItem(DatabaseManager.Instance.SelectItem(11003), 3);
    }
    public void AddItem(List<Item> _itemList, Item _item)
    {
        // 리스트에 아이템 추가 
        _itemList.Add(_item);    
    }
    public void AcquireItem(Item _item, int _amount = 1)
    {
        // 아이템 얻기
        switch (_item.itemKey / 1000)
        {
            case 0:
                Hair _hair = new Hair(_item.itemKey, _item.itemName);
                _hair.count =+ _amount;
                inventroyDecorationItems.Add(_hair);
                break;
            case 1:
                FaceHair _faceHair = new FaceHair(_item.itemKey, _item.itemName);
                _faceHair.count =+ _amount;
                inventroyDecorationItems.Add(_faceHair);
                break;
            case 2:
                Cloth _cloth = new Cloth(_item.itemKey, _item.itemName,_item.defensivePower);
                _cloth.count =+ _amount;
                InventroyEquipmentItems.Add(_cloth);
                break;
            case 3:
                Pant _pant = new Pant(_item.itemKey, _item.itemName, _item.defensivePower);
                _pant.count =+ _amount;
                InventroyEquipmentItems.Add(_pant);
                break;
            case 4:
                Helmet _helmet = new Helmet(_item.itemKey, _item.itemName, _item.defensivePower);
                _helmet.count =+ _amount;
                InventroyEquipmentItems.Add(_helmet);
                break;
            case 5:
                Armor _armor = new Armor(_item.itemKey, _item.itemName, _item.defensivePower);
                _armor.count =+ _amount;
                InventroyEquipmentItems.Add(_armor);
                break;
            case 6:
                Back _back = new Back(_item.itemKey, _item.itemName, _item.defensivePower);
                _back.count =+ _amount;
                InventroyEquipmentItems.Add(_back);
                break;
            case 7:
                Sword _sword = new Sword(_item.itemKey, _item.itemName, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage, _item.atkRange, _item.atkDistance);
                _sword.count =+ _amount;
                inventroyWeaponItems.Add(_sword);
                break;
            case 8:
                Shield _shield = new Shield(_item.itemKey, _item.itemName, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage, _item.atkRange, _item.atkDistance,_item.defensivePower);
                _shield.count =+ _amount;
                inventroyWeaponItems.Add(_shield);
                break;
            case 9:
                Bow _bow = new Bow(_item.itemKey, _item.itemName,_item.attackType,_item.weaponType,_item.physicalDamage,_item.magicalDamage,_item.atkRange,_item.atkDistance);
                _bow.count =+ _amount;
                inventroyWeaponItems.Add(_bow);
                break;
            case 10:
                Wand _wand = new Wand(_item.itemKey, _item.itemName, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage, _item.atkRange, _item.atkDistance);
                _wand.count =+ _amount;
                inventroyWeaponItems.Add(_wand);
                break;
            case 11:
                if (IndexOfItem(inventroyConsumableItems, _item) != -1)
                {
                    Debug.Log("있던 소비품");
                    SelectItem(inventroyConsumableItems, _item).count =+ _amount;
                }
                else
                {
                    Debug.Log("없던 소비품");
                    Consumables _consumables = new Consumables(_item.itemKey, _item.itemName, _item.useEffect, _item.target, _item.durationTime, _item.value);
                    _consumables.count =+ _amount;
                    inventroyConsumableItems.Add(_consumables);
                }
                break;
            case 12:
                if (IndexOfItem(inventroyMiscellaneousItems, _item) != -1)
                {
                    Debug.Log("있던 퀘스트 아이템");
                    SelectItem(inventroyMiscellaneousItems, _item).count =+ _amount;
                }
                else
                {
                    Debug.Log("없던 퀘스트 아이템");
                    Miscellaneous _miscellaneous = new Miscellaneous(_item.itemKey, _item.itemName, _item.purpose);
                    _miscellaneous.count =+ _amount;
                    inventroyMiscellaneousItems.Add(_miscellaneous);
                }
                break;
        }
    }

    public int IndexOfItem(List<Item> _inventory, Item _item)
    {
        // 인벤토리에서 해당 아이템의 순서 없다면 -1
        return _inventory.IndexOf(_item);
    }

    public Item SelectItem(List<Item> _inventory, Item _selectItem)
    {
        // 인벤토리에서 해당 아이템 반환
        Item _item = null;
        if (IndexOfItem(_inventory, _selectItem) != -1)
        {
            _item = _inventory[IndexOfItem(_inventory, _selectItem)];
        }
        else
        {
            Debug.Log("아이템 없음");
        }
        return _item;
    }

    public void UseItem(CharacterStatus _character, Item _Item)
    {
        // 소모품만 가능 UI에서 소모품만 사용하기 UI나타나기
        if (IndexOfItem(inventroyConsumableItems , _Item) != -1)
        {
            SelectItem(inventroyConsumableItems, _Item).count--;
            UseEffect(_character, _Item);
            Debug.Log("아이템 사용");
            if (SelectItem(inventroyConsumableItems, _Item).count == 0)
            {
                inventroyConsumableItems.Remove(SelectItem(inventroyConsumableItems, _Item));
                Debug.Log("아이템 비워짐");
            }
        }
        else
        {
            Debug.Log("아이템 없음");
        }
    }
    public void UseEffect(CharacterStatus _character , Item _item)
    {
        // 아이템 사용
        Consumables consumables = ((Consumables)inventroyConsumableItems[IndexOfItem(inventroyConsumableItems, _item)]);
        if (consumables.useEffect == "Cure")
        {
            _character.CurHp += consumables.value;
        }
        else if (consumables.useEffect == "Buff")
        {
            Debug.Log("버프");
        }
    }
    public void DiscardItem(Item _item)
    {
        if (
            _item.itemType == (int)ItemType.Weapon || _item.itemType == (int)ItemType.SubWeapon)
        {

            // 아이템 버리기
            if (IndexOfItem(inventroyWeaponItems, _item) != -1)
            {
                if (inventroyWeaponItems[IndexOfItem(inventroyWeaponItems, _item)].isEquip != true)
                {
                    inventroyWeaponItems[IndexOfItem(inventroyWeaponItems, _item)].count--;
                    Debug.Log("아이템 버리기");
                    if (inventroyWeaponItems[IndexOfItem(inventroyWeaponItems, _item)].count <= 0)
                    {
                        inventroyWeaponItems.Remove(inventroyWeaponItems[IndexOfItem(inventroyWeaponItems, _item)]);
                        Debug.Log("아이템 비워짐");
                    }
                }
                else
                    Debug.Log("장착 중인 아이템 입니다.");
            }
            else
            {
                Debug.Log("그런 아이템 없음");
            }
        }
        else if (_item.itemType == (int)ItemType.Armor ||
            _item.itemType == (int)ItemType.Helmet || _item.itemType == (int)ItemType.Pant || _item.itemType == (int)ItemType.Back ||
            _item.itemType == (int)ItemType.Cloth)
        {
            // 아이템 버리기
            if (IndexOfItem(inventroyEquipmentItems, _item) != -1)
            {
                if (inventroyEquipmentItems[IndexOfItem(inventroyEquipmentItems, _item)].isEquip != true)
                {
                    inventroyEquipmentItems[IndexOfItem(inventroyEquipmentItems, _item)].count--;
                    Debug.Log("아이템 버리기");
                    if (inventroyEquipmentItems[IndexOfItem(inventroyEquipmentItems, _item)].count <= 0)
                    {
                        inventroyEquipmentItems.Remove(inventroyEquipmentItems[IndexOfItem(inventroyEquipmentItems, _item)]);
                        Debug.Log("아이템 비워짐");
                    }
                }
                else
                    Debug.Log("장착 중인 아이템 입니다.");
            }
            else
            {
                Debug.Log("그런 아이템 없음");
            }
        }
        else if (_item.itemType == (int)ItemType.Hair || _item.itemType == (int)ItemType.FaceHair)
        {
            // 아이템 버리기
            if (IndexOfItem(inventroyDecorationItems, _item) != -1)
            {
                if (inventroyDecorationItems[IndexOfItem(inventroyDecorationItems, _item)].isEquip != true)
                {
                    inventroyDecorationItems[IndexOfItem(inventroyDecorationItems, _item)].count--;
                    Debug.Log("아이템 버리기");
                    if (inventroyWeaponItems[IndexOfItem(inventroyDecorationItems, _item)].count <= 0)
                    {
                        inventroyDecorationItems.Remove(inventroyDecorationItems[IndexOfItem(inventroyDecorationItems, _item)]);
                        Debug.Log("아이템 비워짐");
                    }
                }
                else
                    Debug.Log("장착 중인 아이템 입니다.");
            }
            else
            {
                Debug.Log("그런 아이템 없음");
            }
        }
        else if(_item.itemType == (int)ItemType.Consumables)
        {
            if (IndexOfItem(inventroyConsumableItems, _item) != -1)
            {
                SelectItem(inventroyConsumableItems, _item).count--;
                if (SelectItem(inventroyConsumableItems, _item).count == 0)
                {
                    inventroyConsumableItems.Remove(SelectItem(inventroyConsumableItems, _item));
                    Debug.Log("아이템 비워짐");
                }
            }
            else
            {
                Debug.Log("아이템 없음");
            }
        }
        else
        {
            if (IndexOfItem(inventroyMiscellaneousItems, _item) != -1)
            {
                SelectItem(inventroyMiscellaneousItems, _item).count--;
                if (SelectItem(inventroyMiscellaneousItems, _item).count == 0)
                {
                    inventroyMiscellaneousItems.Remove(SelectItem(inventroyMiscellaneousItems, _item));
                    Debug.Log("아이템 비워짐");
                }
            }
            else
            {
                Debug.Log("아이템 없음");
            }
        }
    }

    public void SortInventory(List<Item> _inventory)
    {
        // 리스트 정렬
        _inventory.Sort(delegate (Item a, Item b)
        {
            if (a.itemKey < b.itemKey) return -1;
            else if (a.itemKey > b.itemKey) return 1;
            return 0;
        });
    }
}

