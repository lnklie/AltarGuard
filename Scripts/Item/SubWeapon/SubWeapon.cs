using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapon : Item
{
    public SubWeapon(int _itemKey, string _itemName, string _itemKorName, int _buyPrice, int _sellPrice, int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount, int _itemRank)
        : base(_itemKey, _itemName, _itemKorName, _buyPrice, _sellPrice, _itemRank)
    {
        itemType = EItemType.SubWeapon;
        equipLevel = _equipLevel;
        disassembleItemKey = _disassembleItemKey;
        disassembleItemAmount = _disassembleItemAmount;
    }
}
