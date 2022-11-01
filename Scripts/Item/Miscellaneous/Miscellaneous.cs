using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Miscellaneous: Item
{
    public Miscellaneous(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, string _purpose,int _itemRank)
        : base(_itemKey,_itemName, _itemKorName, _buyPrice, _sellPrice, _itemRank)
    {
        purpose = _purpose;
        itemType = (int)ItemType.Miscellaneous;

    }
}
