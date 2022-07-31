using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : Equipment.cs
==============================
*/
public class Equipment : Item
{
    public Equipment(int _itemKey, string _itemName, int _defensivePower, int _equipLevel) :base(_itemKey,_itemName)
    {
        defensivePower = _defensivePower;
        equipLevel = _equipLevel;
    }
}
