using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Weapon.cs
==============================
*/
[System.Serializable]
public class Weapon :Item 
{
    public Weapon(int _itemKey, string _itemName, int _buyPrice, int _sellPrice, string _attackType, string _weaponType, int _physicalDamage, int _magicalDamage, 
        float _atkRange, float _atkDistance,float _atkSpeed, int _skillKey1, int _skillKey2, int _equipLevel,int _disassembleItemKey, int _disassembleItemAmount) 
        : base(_itemKey, _itemName, _buyPrice, _sellPrice)
    {
        itemType = (int)ItemType.Weapon;
        physicalDamage = _physicalDamage;
        magicalDamage = _magicalDamage;
        attackType = _attackType;
        weaponType = _weaponType;
        atkRange = _atkRange;
        atkDistance = _atkDistance;
        atkSpeed = _atkSpeed;
        skillKey1 = _skillKey1;
        skillKey2 = _skillKey2;
        equipLevel = _equipLevel;
        disassembleItemKey = _disassembleItemKey;
        disassembleItemAmount = _disassembleItemAmount;
    }
}
