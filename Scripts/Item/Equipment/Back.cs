using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Back.cs
==============================
*/
[System.Serializable]
public class Back : Equipment
{
    public Back(int _itemKey, string _itemName, int _defensivePower, int _equipLevel) : base(_itemKey,_itemName, _defensivePower, _equipLevel)
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
                    spList[i - 1] = (Sprite)sprites[i];
                }
            }
        }
        else
        {
            for (var i = 0; i < spList.Length; i++)
            {
                spList[i] = null;
            }
        }
    }
}
