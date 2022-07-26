using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Consumables.cs
==============================
*/
[System.Serializable]
public class Consumables : Item
{
    public Consumables(int _itemKey, string _itemName, string _useEffect, string _target, float _durationTime, int _value) : base(_itemKey,_itemName)
    {
        useEffect = _useEffect;
        target = _target;
        durationTime = _durationTime;
        value = _value;
        itemType = (int)ItemType.Consumables;
        singleSprite = Resources.Load("Sprites/11_Consumable/" + itemName, typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/11_Consumable/" + itemName, typeof(Texture2D)) as Texture2D;
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
