using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : DatabaseManager.cs
==============================
*/

public class DatabaseManager : SingletonManager<DatabaseManager>
{
    [Header("Items")]
    public List<Hair> hairList = new List<Hair>();
    public List<FaceHair> faceHairList = new List<FaceHair>();
    public List<Cloth> clothList = new List<Cloth>();
    public List<Pant> pantList = new List<Pant>();
    public List<Helmet> helmetList = new List<Helmet>();
    public List<Armor> armorList = new List<Armor>();
    public List<Back> backList = new List<Back>();
    public List<Sword> swordList = new List<Sword>();
    public List<Shield> shieldList = new List<Shield>();
    public List<Bow> bowList = new List<Bow>();
    public List<Wand> wandList = new List<Wand>();
    public List<Consumables> consumablesList = new List<Consumables>();
    public List<Miscellaneous> miscellaneousList = new List<Miscellaneous>();
    [Header("Exp")]
    public List<Exp> exp = new List<Exp>();

    private void Awake()
    {
        List<Dictionary<string, object>> data_Hair = CSVReader.Read("0_Hair");
        List<Dictionary<string, object>> data_FaceHair = CSVReader.Read("1_FaceHair");
        List<Dictionary<string, object>> data_Cloth = CSVReader.Read("2_Cloth");
        List<Dictionary<string, object>> data_Pant = CSVReader.Read("3_Pant");
        List<Dictionary<string, object>> data_Helmet = CSVReader.Read("4_Helmet");
        List<Dictionary<string, object>> data_Armor = CSVReader.Read("5_Armor");
        List<Dictionary<string, object>> data_Back = CSVReader.Read("6_Back");
        List<Dictionary<string, object>> data_Sword = CSVReader.Read("7_Sword");
        List<Dictionary<string, object>> data_Shield = CSVReader.Read("8_Shield");
        List<Dictionary<string, object>> data_Bow = CSVReader.Read("9_Bow");
        List<Dictionary<string, object>> data_Wand = CSVReader.Read("10_Wand");
        List<Dictionary<string, object>> data_Consumables = CSVReader.Read("11_Consumables");
        List<Dictionary<string, object>> data_Miscellaneous = CSVReader.Read("12_Miscellaneous");
        List<Dictionary<string, object>> data_Exp = CSVReader.Read("Exp");

        for (var i = 0; i < data_Hair.Count; i++)
        {
            hairList.Add(new Hair((int)data_Hair[i]["KEY"], (string)data_Hair[i]["NAME"]));
        }
        for (var i = 0; i < data_FaceHair.Count; i++)
        {
            faceHairList.Add(new FaceHair((int)data_FaceHair[i]["KEY"], (string)data_FaceHair[i]["NAME"]));
        }
        for (var i = 0; i < data_Cloth.Count; i++)
        {
            clothList.Add(new Cloth((int)data_Cloth[i]["KEY"], (string)data_Cloth[i]["NAME"], (int)data_Cloth[i]["DEFENSIVEPOWER"]));
        }
        for (var i = 0; i < data_Pant.Count; i++)
        {
            pantList.Add(new Pant((int)data_Pant[i]["KEY"], (string)data_Pant[i]["NAME"], (int)data_Pant[i]["DEFENSIVEPOWER"]));
        }
        for (var i = 0; i < data_Helmet.Count; i++)
        {
            helmetList.Add(new Helmet((int)data_Helmet[i]["KEY"], (string)data_Helmet[i]["NAME"], (int)data_Helmet[i]["DEFENSIVEPOWER"]));
        }
        for (var i = 0; i < data_Armor.Count; i++)
        {
            armorList.Add(new Armor((int)data_Armor[i]["KEY"], (string)data_Armor[i]["NAME"], (int)data_Armor[i]["DEFENSIVEPOWER"]));
        }
        for (var i = 0; i < data_Back.Count; i++)
        {
            backList.Add(new Back((int)data_Back[i]["KEY"], (string)data_Back[i]["NAME"], (int)data_Back[i]["DEFENSIVEPOWER"]));
        }
        for (var i = 0; i < data_Sword.Count; i++)
        {
            swordList.Add(new Sword((int)data_Sword[i]["KEY"], (string)data_Sword[i]["NAME"], (string)data_Sword[i]["ATTACKTYPE"], (string)data_Sword[i]["WEAPONTYPE"], (int)data_Sword[i]["PYSICALDAMAGE"], (int)data_Sword[i]["MAGICALDAMAGE"], (float)data_Sword[i]["ATKRANGE"], (float)data_Sword[i]["ATKDISTANCE"]));
        }
        for (var i = 0; i < data_Shield.Count; i++)
        {
            shieldList.Add(new Shield((int)data_Shield[i]["KEY"], (string)data_Shield[i]["NAME"], (string)data_Shield[i]["ATTACKTYPE"], (string)data_Shield[i]["WEAPONTYPE"], (int)data_Sword[i]["PYSICALDAMAGE"], (int)data_Sword[i]["MAGICALDAMAGE"], (float)data_Shield[i]["ATKRANGE"], (float)data_Shield[i]["ATKDISTANCE"], (int)data_Shield[i]["DEFENSIVEPOWER"]));
        }
        for (var i = 0; i < data_Bow.Count; i++)
        {
            bowList.Add(new Bow((int)data_Bow[i]["KEY"], (string)data_Bow[i]["NAME"], (string)data_Bow[i]["ATTACKTYPE"], (string)data_Bow[i]["WEAPONTYPE"], (int)data_Sword[i]["PYSICALDAMAGE"], (int)data_Sword[i]["MAGICALDAMAGE"], (float)data_Bow[i]["ATKRANGE"], (float)data_Bow[i]["ATKDISTANCE"]));
        }
        for (var i = 0; i < data_Wand.Count; i++)
        {
            wandList.Add(new Wand((int)data_Wand[i]["KEY"], (string)data_Wand[i]["NAME"], (string)data_Wand[i]["ATTACKTYPE"], (string)data_Wand[i]["WEAPONTYPE"], (int)data_Sword[i]["PYSICALDAMAGE"], (int)data_Sword[i]["MAGICALDAMAGE"], (float)data_Wand[i]["ATKRANGE"], (float)data_Wand[i]["ATKDISTANCE"]));
        }
        for (var i = 0; i < data_Consumables.Count; i++)
        {
            consumablesList.Add(new Consumables((int)data_Consumables[i]["KEY"], (string)data_Consumables[i]["NAME"], (string)data_Consumables[i]["USEEFFECT"], (string)data_Consumables[i]["TARGET"], (float)data_Consumables[i]["DURATIONTIME"],(int)data_Consumables[i]["VALUE"]));
        }
        for (var i = 0; i < data_Miscellaneous.Count; i++)
        {
            miscellaneousList.Add(new Miscellaneous((int)data_Miscellaneous[i]["KEY"], (string)data_Miscellaneous[i]["NAME"], (string)data_Miscellaneous[i]["PURPOSE"]));
        }
        for(var i = 0; i < data_Exp.Count; i++)
        {
            exp.Add(new Exp(data_Exp[i]["LV"], data_Exp[i]["EXP"]));
        }
    }
    public Item SelectItem(int _key)
    {
        Item _item = new Item(_key, null);
        switch(_key / 1000)
        {
            case 0:
                for(int i = 0; i < hairList.Count; i++)
                {
                    if (_key == hairList[i].itemKey)
                        _item = hairList[i];
                }
                break;
            case 1:
                for (int i = 0; i < faceHairList.Count; i++)
                {
                    if (_key == faceHairList[i].itemKey)
                        _item = faceHairList[i];
                }
                break;
            case 2:
                for (int i = 0; i < clothList.Count; i++)
                {
                    if (_key == clothList[i].itemKey)
                        _item = clothList[i];
                }
                break;
            case 3:
                for (int i = 0; i < pantList.Count; i++)
                {
                    if (_key == pantList[i].itemKey)
                        _item = pantList[i];
                }
                break;
            case 4:
                for (int i = 0; i < helmetList.Count; i++)
                {
                    if (_key == helmetList[i].itemKey)
                        _item = helmetList[i];
                }
                break;
            case 5:
                for (int i = 0; i < armorList.Count; i++)
                {
                    if (_key == armorList[i].itemKey)
                        _item = armorList[i];
                }
                break;
            case 6:
                for (int i = 0; i < backList.Count; i++)
                {
                    if (_key == backList[i].itemKey)
                        _item = backList[i];
                }
                break;
            case 7:
                for (int i = 0; i < swordList.Count; i++)
                {
                    if (_key == swordList[i].itemKey)
                        _item = swordList[i];
                }
                break;
            case 8:
                for (int i = 0; i < shieldList.Count; i++)
                {
                    if (_key == shieldList[i].itemKey)
                        _item = shieldList[i];
                }
                break;
            case 9:
                for (int i = 0; i < bowList.Count; i++)
                {
                    if (_key == bowList[i].itemKey)
                        _item = bowList[i];
                }
                break;
            case 10:
                for (int i = 0; i < wandList.Count; i++)
                {
                    if (_key == wandList[i].itemKey)
                        _item = wandList[i];
                }
                break;
            case 11:
                for (int i = 0; i < consumablesList.Count; i++)
                {
                    if (_key == consumablesList[i].itemKey)
                        _item = consumablesList[i];
                }
                break;
            case 12:
                for (int i = 0; i < miscellaneousList.Count; i++)
                {
                    if (_key == miscellaneousList[i].itemKey)
                        _item = miscellaneousList[i];
                }
                break;
        }
        return _item;
    }
}