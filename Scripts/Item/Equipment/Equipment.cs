using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Equipment.cs
==============================
*/
public class Equipment : Item
{
    public Equipment(int _itemKey, string _itemName, string _itemKorName, int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel,int _disassembleItemKey, int _disassembleItemAmount, int _itemRank) 
        :base(_itemKey,_itemName, _itemKorName, _buyPrice, _sellPrice, _itemRank)
    {
        defensivePower = _defensivePower;
        equipLevel = _equipLevel;
        disassembleItemKey = _disassembleItemKey;
        disassembleItemAmount = _disassembleItemAmount;
    }
}
