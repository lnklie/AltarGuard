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
    public Weapon(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, string _attackType, string _weaponType, int _physicalDamage, int _magicalDamage, 
        float _atkRange, float _atkDistance,float _atkSpeed, int _skillKey1, int _skillKey2, int _skillKey3, int _equipLevel,int _disassembleItemKey, int _disassembleItemAmount, int _itemRank) 
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
        skillKey1 = _skillKey1;
        skillKey2 = _skillKey2;
        skillKey3 = _skillKey3;
        skillkeies[0] = skillKey1;
        skillkeies[1] = skillKey2;
        skillkeies[2] = skillKey3;
        equipLevel = _equipLevel;
        disassembleItemKey = _disassembleItemKey;
        disassembleItemAmount = _disassembleItemAmount;
    }
}
