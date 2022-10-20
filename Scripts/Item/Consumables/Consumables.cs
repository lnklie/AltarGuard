using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Consumables : Item
{
    public Consumables(int _itemKey, string _itemName, string _itemKorName,int _buyPrice, int _sellPrice, string _useEffect, string _target, float _durationTime, int _value, int _coolTime,int _itemRank) : base(_itemKey, _itemName, _itemKorName,_buyPrice, _sellPrice, _itemRank)
    {
        useEffect = _useEffect;
        target = _target;
        durationTime = _durationTime;
        value = _value;
        maxCoolTime = _coolTime;
        itemType = (int)ItemType.Consumables;
        singleSprite = Resources.Load("Sprites/13_Consumable/" + itemName, typeof(Sprite)) as Sprite;
        texture2D = Resources.Load("Sprites/13_Consumable/" + itemName, typeof(Texture2D)) as Texture2D;
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
