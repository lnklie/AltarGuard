using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon :Item 
{
    public Weapon(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, string _attackType, string _weaponType, int _physicalDamage, int _magicalDamage, 
        float _atkRange, float _atkDistance,float _atkSpeed, int _equipLevel,int _disassembleItemKey, int _disassembleItemAmount, int _itemRank) 
        : base(_itemKey, _itemName, _itemKorName, _buyPrice, _sellPrice,_itemRank)
    {
        itemType = (int)ItemType.Weapon;
        physicalDamage = _physicalDamage;
        magicalDamage = _magicalDamage;
        attackType = _attackType;
        weaponType = _weaponType;
        atkRange = _atkRange;
        atkDistance = _atkDistance;
        atkSpeed = _atkSpeed;
        equipLevel = _equipLevel;
        disassembleItemKey = _disassembleItemKey;
        disassembleItemAmount = _disassembleItemAmount;
    }
}
