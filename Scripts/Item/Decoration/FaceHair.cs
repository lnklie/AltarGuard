using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FaceHair : Item
{
    
    public FaceHair(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, int _itemRank) : base(_itemKey, _itemName, _itemKorName, _buyPrice, _sellPrice, _itemRank)
    {
        itemType = (int)ItemType.FaceHair;
        
    }
}
