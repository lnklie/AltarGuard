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

        AcquireItem(DatabaseManager.Instance.SelectItem(1003));
        AcquireItem(DatabaseManager.Instance.SelectItem(2001));
        AcquireItem(DatabaseManager.Instance.SelectItem(3002));
        AcquireItem(DatabaseManager.Instance.SelectItem(4003));
        AcquireItem(DatabaseManager.Instance.SelectItem(5002));
        AcquireItem(DatabaseManager.Instance.SelectItem(6003));
        AcquireItem(DatabaseManager.Instance.SelectItem(7002));
        AcquireItem(DatabaseManager.Instance.SelectItem(8003));
        AcquireItem(DatabaseManager.Instance.SelectItem(11001,5));
    }
    public void AddItem(List<Item> _itemList, Item _item)
    {
        // 리스트에 아이템 추가 
        _itemList.Add(_item);    
    }
    public Item AcquireItem(Item _item)
    {
        if (_item != null)
        { 
            switch (_item.itemKey / 1000)
            {
                case 0:
                case 1:
                    inventroyDecorationItems.Add(_item);
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    inventroyWeaponItems.Add(_item);
                    break;
                case 11:
                    if (IndexOfItem(_item) != -1)
                        SelectItem(_item).count += _item.count;
                    else
                        inventroyConsumableItems.Add(_item);
                    break;
                case 12:
                    if (IndexOfItem(_item) != -1)
                        SelectItem(_item).count += _item.count;
                    else
                    {

                        inventroyMiscellaneousItems.Add(_item);
                    }
                    break;
            }
            UIManager.Instance.SetLog(_item.itemName +  " Aquired !!");
        }
        return _item;
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
        // 인벤토리에서 해당 아이템 반환
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
                if (!inventroyWeaponItems[IndexOfItem(_item)].isEquip)
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

