using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : Hair.cs
==============================
*/
[System.Serializable]
public class Hair : Item
{
    public Hair(int _itemKey, string _itemName, int _buyPrice, int _sellPrice, int _itemRank) : base(_itemKey, _itemName, _buyPrice, _sellPrice,_itemRank)
    {
        itemType = (int)ItemType.Hair;
        singleSprite = Resources.Load("Sprites/0_Hair/" + itemName, typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/0_Hair/" + itemName, typeof(Texture2D)) as Texture2D;
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
