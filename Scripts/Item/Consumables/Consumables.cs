using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Consumables : Item
{
    public Consumables(int _itemKey, string _itemName, string _itemKorName,int _buyPrice, int _sellPrice, string _useEffect, string _target, float _durationTime, int _value, int _coolTime,int _itemRank) : base(_itemKey, _itemName, _itemKorName,_buyPrice, _sellPrice, _itemRank)
    {
        useEffect = _useEffect;
        target = _target;
        durationTime = _durationTime;
        value = _value;
        maxCoolTime = _coolTime;
        itemType = (int)ItemType.Consumables;
    }
}
