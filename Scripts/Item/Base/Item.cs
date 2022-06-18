using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Item.cs
==============================
*/
[System.Serializable]
public class Item
{
    public Texture2D texture2D = null;
    public Sprite singleSprite = null;
    public Sprite[] spList = new Sprite[3];

    public int itemKey;
    public int count = 0;
    public int equipCharNum = -1;
    public string itemName = null;
    public bool isEquip = false;
    public int itemType = -1;
    public int physicalDamage = 0;
    public int magicalDamage = 0;
    public int defensivePower = 0;
    public float atkRange = 0f;
    public float atkDistance = 0f;
    public float atkSpeed = 0f;
    public string attackType = null;
    public string weaponType = null;
    public string purpose = null;
    public string useEffect = null;
    public string target = null;
    public float durationTime = 0f;
    public int value = 0;
    public Item(int _itemKey, string _itemName)
    {
        itemKey = _itemKey;
        itemName = _itemName;
        count = 0;
    }
}
