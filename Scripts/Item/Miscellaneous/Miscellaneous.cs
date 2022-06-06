using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Miscellaneous.cs
==============================
*/
[System.Serializable]
public class Miscellaneous: Item
{
    public Miscellaneous(int _itemKey, string _itemName, string _purpose) : base(_itemKey,_itemName)
    {
        purpose = _purpose;
        itemType = (int)ItemType.Miscellaneous;
        singleSprite = Resources.Load("Sprites/12_Miscellaneous/" + itemName, typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/12_Miscellaneous/" + itemName, typeof(Texture2D)) as Texture2D;
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
