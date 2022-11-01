using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sword : Weapon
{
    public Sword(int _itemKey, string _itemName, string _itemKorName, int _buyPrice, int _sellPrice, string _attackType, string _weaponType,
        int _physicalDamage, int _magicalDamage, float _atkRange, float _atkDistance, float _atkSpeed,
        int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount, int _itemRank)
     : base(_itemKey, _itemName, _itemKorName, _buyPrice, _sellPrice, _attackType, _weaponType, _physicalDamage, _magicalDamage, _atkRange, _atkDistance, _atkSpeed, _equipLevel, _disassembleItemKey, _disassembleItemAmount, _itemRank)
    {
    }
}
