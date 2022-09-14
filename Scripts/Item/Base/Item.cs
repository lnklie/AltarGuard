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
    public List<Sprite> spList = new List<Sprite>();

    public int itemKey;
    public int count = 0;
    public int equipCharNum = -1;
    public string itemName = null;
    public string itemKorName = null;
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
    public int[] skillkeies = new int[3];
    public int skillKey1 = -1;
    public int skillKey2 = -1;
    public int skillKey3 = -1;
    public int equipLevel = 0;
    public int buyPrice = 0;
    public int sellPrice = 0;
    public int disassembleItemKey = -1;
    public int disassembleItemAmount = -1;
    public int maxCoolTime = 1;
    public float coolTime = 0;
    public bool isCoolTime = false;
    public int itemRank = -1;
    public int inventoryIndex = -1; 
    public System.DateTime dateTime = default; 
    public Item(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, int _itemRank)
    {
        itemKey = _itemKey;
        itemName = _itemName;
        itemKorName = _itemKorName;
        buyPrice = _buyPrice;
        sellPrice = _sellPrice;
        count = 0;
        itemRank = _itemRank;
    }
}
