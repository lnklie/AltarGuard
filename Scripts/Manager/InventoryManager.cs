using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * �������� : 2022-06-09
 * �ۼ��� : Inklie
 * ���ϸ� : InventoryManager.cs
==============================
*/
public class InventoryManager : SingletonManager<InventoryManager>
{
    [SerializeField] private List<Item> inventroyWeaponItems = new List<Item>();

    [SerializeField] private List<Item> inventroyEquipmentItems = new List<Item>();

    [SerializeField] private List<Item> inventroyConsumableItems = new List<Item>();

    [SerializeField] private List<Item> inventroyMiscellaneousItems = new List<Item>();

    [SerializeField] private List<Item> inventroyDecorationItems = new List<Item>();

    [SerializeField] private bool isWeaponCoolTime = false;
    [SerializeField] private bool isEquipmentCoolTime = false;
    [SerializeField] private bool isConsumaableCoolTime = false;
    [SerializeField] private bool isDecorationCoolTime = false;

    #region Property
    public List<Item> InventroyWeaponItems { get { return inventroyWeaponItems; } }
    public List<Item> InventroyEquipmentItems { get { return inventroyEquipmentItems; } }
    public List<Item> InventroyConsumableItems { get { return inventroyConsumableItems; } }
    public List<Item> InventroyMiscellaneousItems { get { return inventroyMiscellaneousItems; } }
    public List<Item> InventroyDecorationItems { get { return inventroyDecorationItems; } }
    public bool IsWeaponCoolTime { get { return isWeaponCoolTime; } set { isWeaponCoolTime = value; } }

    public bool IsEquipmentCoolTime { get { return isEquipmentCoolTime; } set { isEquipmentCoolTime = value; } }
    public bool IsConsumaableCoolTime { get { return isConsumaableCoolTime; } set { isConsumaableCoolTime = value; } }
    public bool IsDecorationCoolTime { get { return isDecorationCoolTime; } set { isDecorationCoolTime = value; } }
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

        //AcquireItem(DatabaseManager.Instance.SelectItem(1003));
        //AcquireItem(DatabaseManager.Instance.SelectItem(2001));
        //AcquireItem(DatabaseManager.Instance.SelectItem(3002));
        AcquireItem(DatabaseManager.Instance.SelectItem(4003));
        //AcquireItem(DatabaseManager.Instance.SelectItem(5002));
        //AcquireItem(DatabaseManager.Instance.SelectItem(6003));
        //AcquireItem(DatabaseManager.Instance.SelectItem(7002));
        //AcquireItem(DatabaseManager.Instance.SelectItem(8003));
        //AcquireItem(DatabaseManager.Instance.SelectItem(11001,5));

    }
    private void Update()
    {

        for (int i = 0; i < InventroyWeaponItems.Count; i++)
        {
            if (InventroyWeaponItems[i].isCoolTime)
            {
                InventroyWeaponItems[i].coolTime -= Time.deltaTime;
                if (InventroyWeaponItems[i].coolTime <= 0f)
                {
                    InventroyWeaponItems[i].isCoolTime = false;
                    InventroyWeaponItems[i].coolTime = 0;
                }
            }
        }

        for (int i = 0; i < inventroyEquipmentItems.Count; i++)
        {
            if (inventroyEquipmentItems[i].isCoolTime)
            {
                inventroyEquipmentItems[i].coolTime -= Time.deltaTime;
                if (inventroyEquipmentItems[i].coolTime <= 0f)
                {
                    inventroyEquipmentItems[i].isCoolTime = false;
                    inventroyEquipmentItems[i].coolTime = 0;
                }
            }
        }

        for (int i = 0; i < InventroyConsumableItems.Count; i++)
        {
            if (InventroyConsumableItems[i].isCoolTime)
            {
                InventroyConsumableItems[i].coolTime -= Time.deltaTime;
                if (InventroyConsumableItems[i].coolTime <= 0f)
                {
                    InventroyConsumableItems[i].coolTime = 0;
                    InventroyConsumableItems[i].isCoolTime = false;
                }
            }
        }
        for (int i = 0; i < InventroyDecorationItems.Count; i++)
        {
            if (InventroyDecorationItems[i].isCoolTime)
            {
                InventroyDecorationItems[i].coolTime -= Time.deltaTime;
                if (InventroyDecorationItems[i].coolTime <= 0f)
                {
                    InventroyDecorationItems[i].isCoolTime = false;
                    InventroyDecorationItems[i].coolTime = 0;
                }
            }
        }

    }
    public void AddItem(List<Item> _itemList, Item _item)
    {
        // ����Ʈ�� ������ �߰� 
        _itemList.Add(_item);    
    }
    public Item AcquireItem(Item _item,int _count = 1)
    {
        _item.dateTime = System.DateTime.Now;
        if (_item != null)
        { 
            switch (_item.itemKey / 1000)
            {
                case 0:
                case 1:
                    inventroyDecorationItems.Add(_item);
                    _item.inventoryIndex = inventroyDecorationItems.IndexOf(_item);
                    _item.count = _count;
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    inventroyEquipmentItems.Add(_item);
                    _item.inventoryIndex = inventroyEquipmentItems.IndexOf(_item);
                    _item.count = _count;
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    inventroyWeaponItems.Add(_item);
                    _item.inventoryIndex = inventroyWeaponItems.IndexOf(_item);
                    _item.count = _count;
                    break;
                case 13:
                    if (IndexOfItem(_item) != -1)
                        SelectItem(_item).count += _item.count;
                    else
                    {
                        inventroyConsumableItems.Add(_item);
                    }
                    break;
                case 14:
                    if (IndexOfItem(_item) != -1)
                        SelectItem(_item).count += _item.count;
                    else
                    {
                        inventroyMiscellaneousItems.Add(_item);
                    }
                    break;
            }
            _item.inventoryIndex = IndexOfItem(_item);
            UIManager.Instance.SetLog(_item.itemKorName +  " Aquired !!");
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
            case 7:
                index = inventroyEquipmentItems.IndexOf(_item);
                break;
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
        // �κ��丮���� �ش� ������ ��ȯ
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
                case 7:
                    _item = inventroyEquipmentItems[IndexOfItem(_selectItem)];
                    break;
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
        }
        return _item;
    }
    public List<Item> KeyToItems(int _selectItemKey)
    {
        // �κ��丮���� �ش� ������ ��ȯ
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
            case 7:
                for (int i = 0; i < inventroyEquipmentItems.Count; i++)
                {
                    if (_selectItemKey == inventroyEquipmentItems[i].itemKey)
                    _items.Add(inventroyEquipmentItems[i]);
                }
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                for (int i = 0; i < inventroyWeaponItems.Count; i++)
                {
                    if (_selectItemKey == inventroyWeaponItems[i].itemKey)
                    _items.Add(inventroyWeaponItems[i]);
                }
                break;
            case 13:
                for (int i = 0; i < inventroyConsumableItems.Count; i++)
                {
                    if (_selectItemKey == inventroyConsumableItems[i].itemKey)
                    _items.Add(inventroyConsumableItems[i]);
                }
                break;
            case 14:
                for (int i = 0; i < inventroyMiscellaneousItems.Count; i++)
                {
                    if (_selectItemKey == inventroyMiscellaneousItems[i].itemKey)
                    _items.Add(inventroyMiscellaneousItems[i]);
                }
                break;

        }

        return _items;
    }
    public void SetInventoryIndex(List<Item> _inventory)
    {
        for(int i = 0; i < _inventory.Count; i++)
        {
            _inventory[i].inventoryIndex = i;
        }
    }
    public void UseItem(CharacterStatus _character, Item _item)
    {
        if (IndexOfItem(_item) != -1)
        {
            SelectItem(_item).count--;
            UseEffect(_character, _item);
            if (SelectItem(_item).count == 0)
            {
                inventroyConsumableItems.Remove(SelectItem(_item));
                SetInventoryIndex(inventroyConsumableItems);
            }
        }
        else
        {
            Debug.Log("������ ���");
        }
    }
    public void UseEffect(CharacterStatus _character , Item _item)
    {
        // ������ ���
        Consumables consumables = ((Consumables)inventroyConsumableItems[IndexOfItem( _item)]);
        if (consumables.useEffect == "Cure")
        {
            _character.CurHp += consumables.value;
        }
        else if (consumables.useEffect == "Buff")
        {
            Debug.Log("����");
        }
    }
    public void DiscardItem(Item _item,int _amount = 1)
    {
        if (_item.itemType == (int)ItemType.Weapon || _item.itemType == (int)ItemType.SubWeapon)
        {

            // ������ ���
            if (IndexOfItem(_item) != -1)
            {
                if (!inventroyWeaponItems[IndexOfItem(_item)].isEquip)
                {
                    inventroyWeaponItems[IndexOfItem( _item)].count--;
                    Debug.Log("������ ���");
                    if (inventroyWeaponItems[IndexOfItem(_item)].count <= 0)
                    {
                        inventroyWeaponItems.Remove(inventroyWeaponItems[IndexOfItem(_item)]);
                        SetInventoryIndex(inventroyWeaponItems);
                    }
                }
                else
                    Debug.Log("���� ���� ������ �Դϴ�.");
            }
            else
            {
                //Debug.Log("�׷� ������ ���");
            }
        }
        else if (_item.itemType == (int)ItemType.Armor ||
            _item.itemType == (int)ItemType.Helmet || _item.itemType == (int)ItemType.Pant || _item.itemType == (int)ItemType.Back ||
            _item.itemType == (int)ItemType.Cloth)
        {
            // ������ ���
            if (IndexOfItem(_item) != -1)
            {
                if (inventroyEquipmentItems[IndexOfItem(_item)].isEquip != true)
                {
                    inventroyEquipmentItems[IndexOfItem( _item)].count--;
                    Debug.Log("������ ���");
                    if (inventroyEquipmentItems[IndexOfItem(_item)].count <= 0)
                    {
                        inventroyEquipmentItems.Remove(inventroyEquipmentItems[IndexOfItem(_item)]);
                        SetInventoryIndex(inventroyEquipmentItems);
                    }
                }
                else
                    Debug.Log("���� ���� ������ �Դϴ�.");
            }
            else
            {
                //Debug.Log("�׷� ������ ���");
            }
        }
        else if (_item.itemType == (int)ItemType.Hair || _item.itemType == (int)ItemType.FaceHair)
        {
            // ������ ���
            if (IndexOfItem(_item) != -1)
            {
                if (inventroyDecorationItems[IndexOfItem(_item)].isEquip != true)
                {
                    inventroyDecorationItems[IndexOfItem(_item)].count--;
                    Debug.Log("������ ���");
                    if (inventroyDecorationItems[IndexOfItem(_item)].count <= 0)
                    {
                        inventroyDecorationItems.Remove(inventroyDecorationItems[IndexOfItem(_item)]);
                        SetInventoryIndex(inventroyDecorationItems);
                    }
                }
                else
                    Debug.Log("���� ���� ������ �Դϴ�.");
            }
            else
            {
                //Debug.Log("�׷� ������ ���");
            }
        }
        else if(_item.itemType == (int)ItemType.Consumables)
        {
            if (IndexOfItem(_item) != -1)
            {
                Debug.Log("�ķ�� �������� ��� " + _item.count);
                _item.count -= _amount;
                Debug.Log("��� �������� ��� " + _item.count);
                if ( _item.count <= 0)
                {
                    inventroyConsumableItems.Remove(_item);
                    SetInventoryIndex(inventroyConsumableItems);
                }
            }
            else
            {
                //Debug.Log("������ ���");
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
                    SetInventoryIndex(inventroyMiscellaneousItems);
                }
            }
            else
            {
                //Debug.Log("������ ���");
            }
        }
    }
    public void SortInventoryByItemKey(List<Item> _inventory)
    {
        _inventory.Sort(delegate (Item a, Item b)
        {
            if (a.itemKey < b.itemKey) return -1;
            else if (a.itemKey > b.itemKey) return 1;
            else return 0;
        });
    }
    public void SortInventoryByKeyAndInventoryIndex(List<Item> _inventory)
    {
        // ����Ʈ ���
        _inventory.Sort(delegate (Item a, Item b)
        {
            if (a.itemKey < b.itemKey) return -1;
            else if (a.itemKey > b.itemKey) return 1;
            else return a.inventoryIndex.CompareTo(b.inventoryIndex);
        });
    }
}

