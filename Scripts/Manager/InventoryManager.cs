using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
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
    public void DiscardItem(List<Item> _inventory,Item _item)
    {
        // 아이템 버리기
        Debug.Log("버리기 " + IndexOfItem(_inventory, _item) + "버릴려는 키는 " + _item);
        if (IndexOfItem(_inventory, _item) != -1)
        {
            if (_inventory[IndexOfItem(_inventory, _item)].isEquip != true)
            {
                _inventory[IndexOfItem(_inventory, _item)].count--;
                Debug.Log("아이템 버리기");
                if (_inventory[IndexOfItem(_inventory, _item)].count <= 0)
                {
                    _inventory.Remove(_inventory[IndexOfItem(_inventory, _item)]);
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

