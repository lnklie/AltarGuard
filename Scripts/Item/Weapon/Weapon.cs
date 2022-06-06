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
    public Weapon(int _itemKey, string _itemName, string _attackType, string _weaponType, int _physicalDamage, int _magicalDamage, float _atkRange, float _atkDistance) : base(_itemKey, _itemName)
    {
        physicalDamage = _physicalDamage;
        magicalDamage = _magicalDamage;
        attackType = _attackType;
        weaponType = _weaponType;
        atkRange = _atkRange;
        atkDistance = _atkDistance;
        itemType = (int)ItemType.Weapon;
    }
}
