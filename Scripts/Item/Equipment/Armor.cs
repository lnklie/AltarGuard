using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Armor.cs
==============================
*/
[System.Serializable]
public class Armor : Equipment
{
    public Armor(int _itemKey, string _itemName, int _defensivePower) : base(_itemKey,_itemName,_defensivePower)
    {
        itemType = (int)ItemType.Armor;
        texture2D = Resources.Load("Sprites/5_Armor/" + itemName, typeof(Texture2D)) as Texture2D;
        singleSprite = Resources.Load("Sprites/5_Armor/" + itemName + "_Single", typeof(Sprite)) as Sprite;
        string path = AssetDatabase.GetAssetPath(texture2D);
        Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(path);
        if (texture2D != null)
        {
            for (var i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].GetType() == typeof(Sprite))
                {
                    switch (sprites[i].name)
                    {
                        case "Left":
                            spList[i - 1] = (Sprite)sprites[i];
                            break;
                        case "Body":
                            spList[i - 1] = (Sprite)sprites[i];
                            break;
                        case "Right":
                            spList[i - 1] = (Sprite)sprites[i];
                            break;
                    }
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
