using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Helmet.cs
==============================
*/
[System.Serializable]
public class Helmet : Equipment
{

    public Helmet(int _itemKey, string _itemName, int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount) : base(_itemKey, _itemName, _buyPrice, _sellPrice, _defensivePower, _equipLevel, _disassembleItemKey, _disassembleItemAmount)
    {
        itemType = (int)ItemType.Helmet;
        singleSprite = Resources.Load("Sprites/4_Helmet/" + itemName, typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/4_Helmet/" + itemName, typeof(Texture2D)) as Texture2D;
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