using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : Back.cs
==============================
*/
[System.Serializable]
public class Back : Equipment
{
    public Back(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount, int _itemRank) 
        : base(_itemKey,_itemName, _itemKorName, _buyPrice, _sellPrice, _defensivePower, _equipLevel,_disassembleItemKey, _disassembleItemAmount, _itemRank)
    {
        texture2D = Resources.Load("Sprites/6_Back/" + itemName, typeof(Texture2D)) as Texture2D;
        singleSprite = Resources.Load("Sprites/6_Back/" + itemName, typeof(Sprite)) as Sprite;
        itemType = (int)ItemType.Back;
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
