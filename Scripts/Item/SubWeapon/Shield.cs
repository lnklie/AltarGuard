using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : Shield.cs
==============================
*/
[System.Serializable]
public class Shield : SubWeapon
{
    public Shield(int _itemKey, string _itemName, string _itemKorName,int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount, int _itemRank) 
        : base(_itemKey,_itemName, _itemKorName, _buyPrice, _sellPrice, _equipLevel, _disassembleItemKey, _disassembleItemAmount, _itemRank) 
    {

        defensivePower = _defensivePower;
        itemType = (int)ItemType.SubWeapon;
        singleSprite = Resources.Load("Sprites/7_Shield/" + itemName, typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/7_Shield/" + itemName, typeof(Texture2D)) as Texture2D;
        string path = AssetDatabase.GetAssetPath(texture2D);
        Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(path);
        if (texture2D != null)
        {
            for (var i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].GetType() == typeof(Sprite))
                {
                    spList.Add((Sprite)sprites[i]);
                }
            }
        }
        else
        {
            for (var i = 0; i < spList.Count; i++)
            {
                spList[i] = null;
            }
        }
    }
}
