using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Wand.cs
==============================
*/
[System.Serializable]
public class Wand : Weapon
{
    public Wand(int _itemKey, string _itemName, int _buyPrice, int _sellPrice, string _attackType,
        string _weaponType, int _physicalDamage, int _magicalDamage, float _atkRange, float _atkDistance, float _atkSpeed, 
        int _skillKey1, int _skillKey2, int _equipLevel, int _disassembleItemKey, int _disassembleItemAmount, int _itemRank)
         : base(_itemKey, _itemName, _buyPrice, _sellPrice, _attackType, _weaponType, _physicalDamage, _magicalDamage, _atkRange, _atkDistance, _atkSpeed, _skillKey1, _skillKey2, _equipLevel, _disassembleItemKey, _disassembleItemAmount, _itemRank)
    {
        singleSprite = Resources.Load("Sprites/10_Wand/" + itemName, typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/10_Wand/" + itemName, typeof(Texture2D)) as Texture2D;
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
