using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    public Sprite singleSprite = null;
    public List<Sprite> spList = new List<Sprite>();
    public string spPath = null;

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
    public Skill[] skills = new Skill[3];

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
    public DateTime dateTime = default;
    public CompleteGrace grace1 = null;
    public CompleteGrace grace2 = null;
    public CompleteGrace grace3 = null;

    public Item(int _itemKey, string _itemName,string _itemKorName, int _buyPrice, int _sellPrice, int _itemRank)
    {
        itemKey = _itemKey;
        itemName = _itemName;
        itemKorName = _itemKorName;
        buyPrice = _buyPrice;
        sellPrice = _sellPrice;
        count = 0;
        itemRank = _itemRank;

        switch(itemKey / 1000)
        {
            case 0:
                spPath = "Sprites/0_Hair/";
                break;
            case 1:
                spPath = "Sprites/1_FaceHair/";
                break;
            case 2:
                spPath = "Sprites/2_Cloth/";
                break;
            case 3:
                spPath = "Sprites/3_Pant/";
                break;
            case 4:
                spPath = "Sprites/4_Helmet/";
                break;
            case 5:
                spPath = "Sprites/5_Armor/";
                break;
            case 6:
                spPath = "Sprites/6_Back/";
                break;
            case 7:
                spPath = "Sprites/7_Shield/";
                break;
            case 8:
                spPath = "Sprites/8_Sword/";
                break;
            case 9:
                spPath = "Sprites/9_Exe/";
                break;
            case 10:
                spPath = "Sprites/10_Spear/";
                break;

            case 11:
                spPath = "Sprites/11_Bow/";
                break;
            case 12:
                spPath = "Sprites/12_Wand/";
                break;
            case 13:
                spPath = "Sprites/13_Consumable/";
                break;
            case 14:
                spPath = "Sprites/14_Miscellaneous/";
                break;
        }

        singleSprite = Resources.Load(spPath + itemName, typeof(Sprite)) as Sprite;
        UnityEngine.Object[] sprites = Resources.LoadAll<Sprite>(spPath + itemName);
        if (sprites.Length > 0)
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
