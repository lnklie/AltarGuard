using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-09
 * 작성자 : Inklie
 * 파일명 : InventoryManager.cs
==============================
*/
public class InventoryManager : SingletonManager<InventoryManager>
{
    [SerializeField]
    private List<Item> inventroyWeaponItems = new List<Item>();

    [SerializeField]
    private List<Item> inventroyEquipmentItems = new List<Item>();

    [SerializeField]
    private List<Item> inventroyConsumableItems = new List<Item>();

    [SerializeField]
    private List<Item> inventroyMiscellaneousItems = new List<Item>();
    [SerializeField]
    private List<Item> inventroyDecorationItems = new List<Item>();

    #region Property
    public List<Item> InventroyWeaponItems
    {
        get { return inventroyWeaponItems; }
    }
    public List<Item> InventroyEquipmentItems
    {
        get { return inventroyEquipmentItems; }
    }
    public List<Item> InventroyConsumableItems
    {
        get { return inventroyConsumableItems; }
    }
    public List<Item> InventroyMiscellaneousItems
    {
        get { return inventroyMiscellaneousItems; }
    }
    public List<Item> InventroyDecorationItems
    {
        get { return inventroyDecorationItems; }
    }
    #endregion

    private void Start()
    {
        //AcquireItem(DatabaseManager.Instance.SelectItem(3), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(4), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(5), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(6), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(7), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(8), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(9), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(10), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(11), 1);
        //AcquireItem(DatabaseManager.Instance.SelectItem(12), 1);

        AcquireItem(DatabaseManager.Instance.SelectItem(1003), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(2001), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(3002), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(4003), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(5002), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(6003),1);
        AcquireItem(DatabaseManager.Instance.SelectItem(7002), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(8003), 1);
        AcquireItem(DatabaseManager.Instance.SelectItem(11001), 5);
    }
    public void AddItem(List<Item> _itemList, Item _item)
    {
        // 리스트에 아이템 추가 
        _itemList.Add(_item);    
    }
    public Item AcquireItem(Item _item, int _amount = 1)
    {
        Item __item = null;
        // 아이템 얻기
        switch (_item.itemKey / 1000)
        {
            case 0:
                Hair _hair = new Hair(_item.itemKey, _item.itemName,_item.buyPrice,_item.sellPrice);
                _hair.count =+ _amount;
                inventroyDecorationItems.Add(_hair);
                __item  = _hair;
                break;
            case 1:
                FaceHair _faceHair = new FaceHair(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice);
                _faceHair.count =+ _amount;
                inventroyDecorationItems.Add(_faceHair);
                __item = _faceHair;
                break;
            case 2:
                Cloth _cloth = new Cloth(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel,_item.disassembleItemKey, _item.disassembleItemAmount);
                _cloth.count =+ _amount;
                InventroyEquipmentItems.Add(_cloth);
                __item = _cloth;
                break;
            case 3:
                Pant _pant = new Pant(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _pant.count =+ _amount;
                InventroyEquipmentItems.Add(_pant);
                __item = _pant;
                break;
            case 4:
                Helmet _helmet = new Helmet(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _helmet.count =+ _amount;
                InventroyEquipmentItems.Add(_helmet);
                __item = _helmet;
                break;
            case 5:
                Armor _armor = new Armor(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _armor.count =+ _amount;
                InventroyEquipmentItems.Add(_armor);
                __item = _armor;
                break;
            case 6:
                Back _back = new Back(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower,_item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _back.count =+ _amount;
                InventroyEquipmentItems.Add(_back);
                __item = _back;
                break;
            case 7:
                Sword _sword = new Sword(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage,
                    _item.atkRange, _item.atkDistance,_item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _sword.count =+ _amount;
                inventroyWeaponItems.Add(_sword);
                __item = _sword;
                break;
            case 8:
                Shield _shield = new Shield(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage,
                    _item.atkRange, _item.atkDistance,_item.defensivePower,_item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _shield.count =+ _amount;
                inventroyWeaponItems.Add(_shield);
                __item = _shield;
                break;
            case 9:
                Bow _bow = new Bow(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType,_item.weaponType,_item.physicalDamage,_item.magicalDamage, 
                    _item.atkRange,_item.atkDistance,_item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _bow.count =+ _amount;
                inventroyWeaponItems.Add(_bow);
                __item = _bow;
                break;
            case 10:
                Wand _wand = new Wand(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage, 
                    _item.atkRange, _item.atkDistance, _item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                _wand.count =+ _amount;
                inventroyWeaponItems.Add(_wand);
                __item = _wand;
                break;
            case 11:
                if (IndexOfItem(_item) != -1)
                {
                    __item = SelectItem(_item);
                    Debug.Log("있던 소비품 " + __item.count);
                    __item.count += _amount;
                    Debug.Log("채워 진후 " + __item.count);
                }
                else
                {
                    Debug.Log("없던 소비품");
                    Consumables _consumables = new Consumables(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.useEffect, _item.target, _item.durationTime, _item.value);
                    _consumables.count = _amount;
                    inventroyConsumableItems.Add(_consumables);
                    __item = _consumables;
                }
                break;
            case 12:
                if (IndexOfItem(_item) != -1)
                {
                    Debug.Log("있던 퀘스트 아이템");
                    __item = SelectItem(_item);
                    __item.count += _amount;
                }
                else
                {
                    Debug.Log("없던 퀘스트 아이템");
                    Miscellaneous _miscellaneous = new Miscellaneous(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.purpose);
                    _miscellaneous.count = _amount;
                    inventroyMiscellaneousItems.Add(_miscellaneous);
                    __item = _miscellaneous;
                }
                break;
        }
        UIManager.Instance.SetLog(__item.itemName +  " Aquired !!");
        return __item;
    }

    public int IndexOfItem(Item _item)
    {
        int index = 0;
        switch (_item.itemType)
        {
            case 0:
            case 1:
                index = inventroyDecorationItems.IndexOf(_item);
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                index = inventroyEquipmentItems.IndexOf(_item);
                break;
            case 7:
            case 8:
                index = inventroyWeaponItems.IndexOf(_item);
                break;
            case 9:
                index = CheckStackedItemIndex(_item, inventroyConsumableItems);
                break;
            case 10:
                index = CheckStackedItemIndex(_item, inventroyMiscellaneousItems);
                break;
        }
        return index;
    }
    public int CheckStackedItemIndex(Item _stackedItem, List<Item> _inventory)
    {
        int _index = -1;
        for(int i = 0; i< _inventory.Count; i++)
        {
            if (_stackedItem.itemKey == _inventory[i].itemKey)
                _index = i;
            else
                _index = -1;
        }
        return _index;
    }
    public Item SelectItem(Item _selectItem)
    {
        // 인벤토리에서 해당 아이템 반환
        Item _item = null;

        if (IndexOfItem(_selectItem) != -1)
        {

            switch (_selectItem.itemType)
            {
                case 0:
                case 1:
                    _item = inventroyDecorationItems[IndexOfItem(_selectItem)];
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    _item = inventroyEquipmentItems[IndexOfItem(_selectItem)];
                    break;
                case 7:
                case 8:
                    _item = inventroyWeaponItems[IndexOfItem(_selectItem)];
                    break;
                case 9:
                    _item = inventroyConsumableItems[IndexOfItem(_selectItem)];

                    break;
                case 10:
                    _item = inventroyMiscellaneousItems[IndexOfItem(_selectItem)];
                    break;
            }
        } 
        else
        {
            Debug.Log("아이템 없음");
        }
        return _item;
    }
    public List<Item> KeyToItems(int _selectItemKey)
    {
        List<Item> _items = new List<Item>();

        switch (_selectItemKey / 1000)
        {
            case 0:
            case 1:
                for(int i = 0; i< inventroyDecorationItems.Count; i++)
                {
                    if(_selectItemKey == inventroyDecorationItems[i].itemKey)
                    {
                        _items.Add(inventroyDecorationItems[i]);
                    }
                }
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                for (int i = 0; i < inventroyEquipmentItems.Count; i++)
                {
                    if (_selectItemKey == inventroyEquipmentItems[i].itemKey)
                    _items.Add(inventroyEquipmentItems[i]);
                }
                break;
            case 7:
            case 8:
                break;
            case 9:
                break;
            case 10:
                for (int i = 0; i < inventroyWeaponItems.Count; i++)
                {
                    if (_selectItemKey == inventroyWeaponItems[i].itemKey)
                    _items.Add(inventroyWeaponItems[i]);
                }
                break;
            case 11:
                for (int i = 0; i < inventroyConsumableItems.Count; i++)
                {
                    if (_selectItemKey == inventroyConsumableItems[i].itemKey)
                    _items.Add(inventroyConsumableItems[i]);
                }
                break;
            case 12:
                for (int i = 0; i < inventroyMiscellaneousItems.Count; i++)
                {
                    if (_selectItemKey == inventroyMiscellaneousItems[i].itemKey)
                    _items.Add(inventroyMiscellaneousItems[i]);
                }
                break;

        }

        return _items;
    }
    public void UseItem(CharacterStatus _character, Item _Item)
    {
        // 소모품만 가능 UI에서 소모품만 사용하기 UI나타나기
        if (IndexOfItem(_Item) != -1)
        {
            SelectItem(_Item).count--;
            UseEffect(_character, _Item);
            Debug.Log("아이템 사용");
            if (SelectItem(_Item).count == 0)
            {
                inventroyConsumableItems.Remove(SelectItem(_Item));
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
        Consumables consumables = ((Consumables)inventroyConsumableItems[IndexOfItem( _item)]);
        if (consumables.useEffect == "Cure")
        {
            _character.CurHp += consumables.value;
        }
        else if (consumables.useEffect == "Buff")
        {
            Debug.Log("버프");
        }
    }
    public void DiscardItem(Item _item,int _amount = 1)
    {
        if (_item.itemType == (int)ItemType.Weapon || _item.itemType == (int)ItemType.SubWeapon)
        {

            // 아이템 버리기
            if (IndexOfItem(_item) != -1)
            {
                if (inventroyWeaponItems[IndexOfItem(_item)].isEquip != true)
                {
                    inventroyWeaponItems[IndexOfItem( _item)].count--;
                    Debug.Log("아이템 버리기");
                    if (inventroyWeaponItems[IndexOfItem(_item)].count <= 0)
                    {
                        inventroyWeaponItems.Remove(inventroyWeaponItems[IndexOfItem(_item)]);
                        Debug.Log("아이템 비워짐");
                    }
                }
                else
                    Debug.Log("장착 중인 아이템 입니다.");
            }
            else
            {
                //Debug.Log("그런 아이템 없음");
            }
        }
        else if (_item.itemType == (int)ItemType.Armor ||
            _item.itemType == (int)ItemType.Helmet || _item.itemType == (int)ItemType.Pant || _item.itemType == (int)ItemType.Back ||
            _item.itemType == (int)ItemType.Cloth)
        {
            // 아이템 버리기
            if (IndexOfItem(_item) != -1)
            {
                if (inventroyEquipmentItems[IndexOfItem(_item)].isEquip != true)
                {
                    inventroyEquipmentItems[IndexOfItem( _item)].count--;
                    Debug.Log("아이템 버리기");
                    if (inventroyEquipmentItems[IndexOfItem(_item)].count <= 0)
                    {
                        inventroyEquipmentItems.Remove(inventroyEquipmentItems[IndexOfItem(_item)]);
                        Debug.Log("아이템 비워짐");
                    }
                }
                else
                    Debug.Log("장착 중인 아이템 입니다.");
            }
            else
            {
                //Debug.Log("그런 아이템 없음");
            }
        }
        else if (_item.itemType == (int)ItemType.Hair || _item.itemType == (int)ItemType.FaceHair)
        {
            // 아이템 버리기
            if (IndexOfItem(_item) != -1)
            {
                if (inventroyDecorationItems[IndexOfItem(_item)].isEquip != true)
                {
                    inventroyDecorationItems[IndexOfItem(_item)].count--;
                    Debug.Log("아이템 버리기");
                    if (inventroyDecorationItems[IndexOfItem(_item)].count <= 0)
                    {
                        inventroyDecorationItems.Remove(inventroyDecorationItems[IndexOfItem(_item)]);
                        Debug.Log("아이템 비워짐");
                    }
                }
                else
                    Debug.Log("장착 중인 아이템 입니다.");
            }
            else
            {
                //Debug.Log("그런 아이템 없음");
            }
        }
        else if(_item.itemType == (int)ItemType.Consumables)
        {
            if (IndexOfItem(_item) != -1)
            {
                Debug.Log("파려는 아이템의 수량은 " + _item.count);
                _item.count -= _amount;
                Debug.Log("남은 아이템의 수량은 " + _item.count);
                if ( _item.count <= 0)
                {
                    inventroyConsumableItems.Remove(_item);
                    Debug.Log("아이템 비워짐");
                }
            }
            else
            {
                //Debug.Log("아이템 없음");
            }
        }
        else
        {
            if (IndexOfItem(_item) != -1)
            {
                _item.count -= _amount;
                if (_item.count == 0)
                {
                    inventroyMiscellaneousItems.Remove(_item);
                    Debug.Log("아이템 비워짐");
                }
            }
            else
            {
                //Debug.Log("아이템 없음");
            }
        }
    }

    public void SortItemKeyInventory(List<Item> _inventory)
    {
        // 리스트 정렬
        _inventory.Sort(delegate (Item a, Item b)
        {
            if (a.itemKey < b.itemKey) return -1;
            else if (a.itemKey > b.itemKey) return 1;
            else return 0;

        });
    }
}

