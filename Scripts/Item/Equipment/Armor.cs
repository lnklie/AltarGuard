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
    public Armor(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, int _defensivePower, int _equipLevel, int _disassembleItemKey,int _disassembleItemAmount, int _itemRank)
        : base(_itemKey,_itemName, _itemKorName,_buyPrice, _sellPrice, _defensivePower,_equipLevel, _disassembleItemKey, _disassembleItemAmount,_itemRank)
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
                    spList.Add((Sprite)sprites[i]);
                    //switch (sprites[i].name)
                    //{
                    //    case "Left":
                    //        spList[i] = (Sprite)sprites[i];
                    //        break;
                    //    case "Body":
                    //        spList[i] = (Sprite)sprites[i];
                    //        break;
                    //    case "Right":
                    //        spList[i] = (Sprite)sprites[i];
                    //        break;
                    //}
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
