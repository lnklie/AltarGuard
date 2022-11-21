using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Back : Equipment
{
    public Back(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount, int _itemRank) 
        : base(_itemKey,_itemName, _itemKorName, _buyPrice, _sellPrice, _defensivePower, _equipLevel,_disassembleItemKey, _disassembleItemAmount, _itemRank)
    {
        itemType = EItemType.Back;
    }
}
