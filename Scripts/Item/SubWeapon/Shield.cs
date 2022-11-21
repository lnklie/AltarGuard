using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Shield : SubWeapon
{
    public Shield(int _itemKey, string _itemName, string _itemKorName,int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount, int _itemRank) 
        : base(_itemKey,_itemName, _itemKorName, _buyPrice, _sellPrice, _equipLevel, _disassembleItemKey, _disassembleItemAmount, _itemRank) 
    {

        defensivePower = _defensivePower;
        itemType = EItemType.SubWeapon;
    }
}
