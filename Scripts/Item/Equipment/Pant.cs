using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Pant.cs
==============================
*/
[System.Serializable]
public class Pant : Equipment
{
    public Pant(int _itemKey, string _itemName, int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel,int _disassembleItemKey, int _disassembleItemAmount,int _itemRank) 
        : base(_itemKey, _itemName, _buyPrice, _sellPrice, _defensivePower, _equipLevel, _disassembleItemKey, _disassembleItemAmount, _itemRank)
    {
        itemType = (int)ItemType.Pant;
        singleSprite = Resources.Load("Sprites/3_Pant/" + itemName + "_Single", typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/3_Pant/" + itemName, typeof(Texture2D)) as Texture2D;
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
