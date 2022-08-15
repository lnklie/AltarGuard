using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SingletonManager<ShopManager>
{
    [SerializeField]
    private Player player = null;

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
    public List<Item> shopInventroyWeaponItems
    {
        get { return inventroyWeaponItems; }
    }
    public List<Item> shopInventroyEquipmentItems
    {
        get { return inventroyEquipmentItems; }
    }
    public List<Item> shopInventroyConsumableItems
    {
        get { return inventroyConsumableItems; }
    }
    public List<Item> shopInventroyMiscellaneousItems
    {
        get { return inventroyMiscellaneousItems; }
    }
    public List<Item> shopInventroyDecorationItems
    {
        get { return inventroyDecorationItems; }
    }
    #endregion
    private void Start()
    {
        AddProduct(DatabaseManager.Instance.SelectItem(8001));
        AddProduct(DatabaseManager.Instance.SelectItem(9001));
        AddProduct(DatabaseManager.Instance.SelectItem(10001));
        AddProduct(DatabaseManager.Instance.SelectItem(11001));
    }
    public Item AddProduct(Item _item)
    {
        Item __item = null;
        // ¾ÆÀÌÅÛ ¾ò±â
        switch (_item.itemKey / 1000)
        {
            case 0:
                Hair _hair = new Hair(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice);
                inventroyDecorationItems.Add(_hair);
                __item = _hair;
                break;
            case 1:
                FaceHair _faceHair = new FaceHair(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice);
                inventroyDecorationItems.Add(_faceHair);
                __item = _faceHair;
                break;
            case 2:
                Cloth _cloth = new Cloth(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                shopInventroyEquipmentItems.Add(_cloth);
                __item = _cloth;
                break;
            case 3:
                Pant _pant = new Pant(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                shopInventroyEquipmentItems.Add(_pant);
                __item = _pant;
                break;
            case 4:
                Helmet _helmet = new Helmet(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                shopInventroyEquipmentItems.Add(_helmet);
                __item = _helmet;
                break;
            case 5:
                Armor _armor = new Armor(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                shopInventroyEquipmentItems.Add(_armor);
                __item = _armor;
                break;
            case 6:
                Back _back = new Back(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.defensivePower, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                shopInventroyEquipmentItems.Add(_back);
                __item = _back;
                break;
            case 7:
                Sword _sword = new Sword(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage,
                    _item.atkRange, _item.atkDistance, _item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                inventroyWeaponItems.Add(_sword);
                __item = _sword;
                break;
            case 8:
                Shield _shield = new Shield(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage,
                    _item.atkRange, _item.atkDistance, _item.defensivePower, _item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                inventroyWeaponItems.Add(_shield);
                __item = _shield;
                break;
            case 9:
                Bow _bow = new Bow(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage,
                    _item.atkRange, _item.atkDistance, _item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                inventroyWeaponItems.Add(_bow);
                __item = _bow;
                break;
            case 10:
                Wand _wand = new Wand(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.attackType, _item.weaponType, _item.physicalDamage, _item.magicalDamage,
                    _item.atkRange, _item.atkDistance, _item.atkSpeed, _item.skillKey1, _item.skillKey2, _item.equipLevel, _item.disassembleItemKey, _item.disassembleItemAmount);
                inventroyWeaponItems.Add(_wand);
                __item = _wand;
                break;
            case 11:
                Consumables _consumables = new Consumables(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.useEffect, _item.target, _item.durationTime, _item.value,_item.coolTime);
                inventroyConsumableItems.Add(_consumables);
                __item = _consumables;

                break;
            case 12:
                Miscellaneous _miscellaneous = new Miscellaneous(_item.itemKey, _item.itemName, _item.buyPrice, _item.sellPrice, _item.purpose);
                inventroyMiscellaneousItems.Add(_miscellaneous);
                __item = _miscellaneous;

                break;
        }
        //UIManager.Instance.SetLog(__item.itemName + " Aquired !!");
        return __item;
    }



    public void SortItemKeyInventory(List<Item> _shopInventory)
    {
        // ¸®½ºÆ® Á¤·Ä
        _shopInventory.Sort(delegate (Item a, Item b)
        {
            if (a.itemKey < b.itemKey) return -1;
            else if (a.itemKey > b.itemKey) return 1;
            else return 0;

        });
    }
}
