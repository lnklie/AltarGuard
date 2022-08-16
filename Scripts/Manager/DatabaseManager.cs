using System.Collections.Generic;
using System.IO;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-09
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
    public List<Exp> expList = new List<Exp>();

    [Header("Enemy")]
    public List<Enemy> rushEnemyList = new List<Enemy>();

    [Header("Stage")]
    public List<Stage> stageList = new List<Stage>();

    [Header("Skill")]
    public List<ActiveSkill> activeSkillList = new List<ActiveSkill>();
    public List<PassiveSkill> passiveSkillList = new List<PassiveSkill>();

    [Header("Grace")]
    public List<Grace> graceList = new List<Grace>();

    [Header("CraftRecipe")]
    public List<CraftRecipe> craftRecipeList = new List<CraftRecipe>();
    private void Awake()
    {
        ExcelToJsonConverter.ConvertExcelFilesToJson(Application.dataPath + "/ExcelFile", Application.dataPath + "/JsonFile");
        JsonLoad();
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
    private string CombinePath(string _excelName)
    {
        return Application.dataPath + "/JsonFile/" + _excelName +".json";
    }

    public void JsonLoad()
    {
        if (!File.Exists(CombinePath("0_Hair")))
        {
            Debug.Log("경로에 머리 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("0_Hair")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                hairList.Add(new Hair(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("1_FaceHair")))
        {
            Debug.Log("경로에 얼굴 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("1_FaceHair")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                faceHairList.Add(new FaceHair(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("2_Cloth")))
        {
            Debug.Log("경로에 옷 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("2_Cloth")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                clothList.Add(new Cloth(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("3_Pant")))
        {
            Debug.Log("경로에 바지 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("3_Pant")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                pantList.Add(new Pant(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("4_Helmet")))
        {
            Debug.Log("경로에 머리 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("4_Helmet")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                helmetList.Add(new Helmet(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("5_Armor")))
        {
            Debug.Log("경로에 갑옷 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("5_Armor")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                armorList.Add(new Armor(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("6_Back")))
        {
            Debug.Log("경로에 망토 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("6_Back")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                backList.Add(new Back(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("7_Sword")))
        {
            Debug.Log("경로에 검 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("7_Sword")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                swordList.Add(new Sword(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].attackType,items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("8_Shield")))
        {
            Debug.Log("경로에 방패 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("8_Shield")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                shieldList.Add(new Shield(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].attackType, items[i].weaponType, items[i].physicalDamage, 
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance,items[i].defensivePower, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("9_Bow")))
        {
            Debug.Log("경로에 활 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("9_Bow")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                bowList.Add(new Bow(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].attackType, items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("10_Wand")))
        {
            Debug.Log("경로에 지팡이 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("10_Wand")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                wandList.Add(new Wand(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].attackType, items[i].weaponType, items[i].physicalDamage, 
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("11_Consumables")))
        {
            Debug.Log("경로에 소비품 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("11_Consumables")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                consumablesList.Add(new Consumables(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].useEffect, items[i].target, items[i].durationTime, items[i].value, items[i].coolTime, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("12_Miscellaneous")))
        {
            Debug.Log("경로에 기타템 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("12_Miscellaneous")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                miscellaneousList.Add(new Miscellaneous(items[i].itemKey, items[i].itemName, items[i].buyPrice, items[i].sellPrice, items[i].purpose, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("Enemy")))
        {
            Debug.Log("경로에 적 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("Enemy")));
            Enemy[] rushEnemies = JsonHelper.FromJson<Enemy>(loadJson);
            for (var i = 0; i < rushEnemies.Length; i++)
            {
                rushEnemyList.Add(new Enemy(rushEnemies[i].objectName, rushEnemies[i].enemyKey, rushEnemies[i].enemyType, rushEnemies[i].hp, rushEnemies[i].mp,
                    rushEnemies[i].str, rushEnemies[i].dex, rushEnemies[i].wiz, rushEnemies[i].seeRange,
                   rushEnemies[i].speed, rushEnemies[i].defeatExp,
                 rushEnemies[i].itemDropKey1, rushEnemies[i].itemDropKey2, rushEnemies[i].itemDropKey3,rushEnemies[i].itemDropKey4, rushEnemies[i].itemDropKey5, 
                 rushEnemies[i].itemDropProb1, rushEnemies[i].itemDropProb2, rushEnemies[i].itemDropProb3, rushEnemies[i].itemDropProb4, rushEnemies[i].itemDropProb5,
                 rushEnemies[i].helmetKey, rushEnemies[i].armorKey, rushEnemies[i].pantKey, rushEnemies[i].weaponKey));
            }

        }
        if (!File.Exists(CombinePath("Exp")))
        {
            Debug.Log("경로에 경험치 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("Exp")));
            Exp[] exp = JsonHelper.FromJson<Exp>(loadJson);
            for (var i = 0; i < exp.Length; i++)
            {
                expList.Add(new Exp(exp[i].lv, exp[i].exp));
            }
        }
        if (!File.Exists(CombinePath("Stage")))
        {
            Debug.Log("경로에 스테이지 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("Stage")));
            Stage[] stage = JsonHelper.FromJson<Stage>(loadJson);
            for (var i = 0; i < stage.Length; i++)
            {
                stageList.Add(new Stage(stage[i].stage, stage[i].enemyKey1,stage[i].enemyKey2,stage[i].bossKey,stage[i].enemyNum1,stage[i].enemyNum2));
            }
        }
        if (!File.Exists(CombinePath("ActiveSkill")))
        {
            Debug.Log("경로에 액티브 스킬 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ActiveSkill")));
            ActiveSkill[] skill = JsonHelper.FromJson<ActiveSkill>(loadJson);
            for (var i = 0; i < skill.Length; i++)
            {
                activeSkillList.Add(new ActiveSkill(skill[i].skillKey, skill[i].skillName, skill[i].skillLevel,skill[i].skillType, skill[i].skillVariable,
                    skill[i].skillValue1, skill[i].skillValue2, skill[i].skillValue3, skill[i].skillValue4,
                    skill[i].skillValue5, skill[i].skillValue6, skill[i].skillValue7, skill[i].skillValue8, skill[i].skillValue9, skill[i].skillValue10,
                    skill[i].skillFigures1, skill[i].skillFigures2, skill[i].skillFigures3, skill[i].skillFigures4, skill[i].skillFigures5,
                    skill[i].skillFigures6, skill[i].skillFigures7, skill[i].skillFigures8, skill[i].skillFigures9, skill[i].skillFigures10, skill[i].coolTime, skill[i].skillHitCount));
            }
        }
        if (!File.Exists(CombinePath("PassiveSkill")))
        {
            Debug.Log("경로에 패시브 스킬 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("PassiveSkill")));
            PassiveSkill[] skill = JsonHelper.FromJson<PassiveSkill>(loadJson);
            for (var i = 0; i < skill.Length; i++)
            {
                passiveSkillList.Add(new PassiveSkill(skill[i].skillKey, skill[i].skillName, skill[i].skillLevel, skill[i].skillVariable, skill[i].targetStatus,
                    skill[i].skillValue1, skill[i].skillValue2, skill[i].skillValue3, skill[i].skillValue4,
                    skill[i].skillValue5, skill[i].skillValue6, skill[i].skillValue7, skill[i].skillValue8, skill[i].skillValue9, skill[i].skillValue10,
                    skill[i].skillFigures1, skill[i].skillFigures2, skill[i].skillFigures3, skill[i].skillFigures4, skill[i].skillFigures5,
                    skill[i].skillFigures6, skill[i].skillFigures7, skill[i].skillFigures8, skill[i].skillFigures9, skill[i].skillFigures10)) ;
            }
        }
        if (!File.Exists(CombinePath("Grace")))
        {
            Debug.Log("경로에 은총 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("Grace")));
            Grace[] grace = JsonHelper.FromJson<Grace>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceList.Add(new Grace(grace[i].graceKey, grace[i].graceName, grace[i].explain, grace[i].necessaryGraceKey));
            }
        }
        if (!File.Exists(CombinePath("Grace")))
        {
            Debug.Log("경로에 은총 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("Grace")));
            Grace[] grace = JsonHelper.FromJson<Grace>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceList.Add(new Grace(grace[i].graceKey, grace[i].graceName, grace[i].explain, grace[i].necessaryGraceKey));
            }
        }
        if (!File.Exists(CombinePath("CraftRecipe")))
        {
            Debug.Log("경로에 제작 레시피 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("CraftRecipe")));
            CraftRecipe[] craftRecipes = JsonHelper.FromJson<CraftRecipe>(loadJson);
            for (var i = 0; i < craftRecipes.Length; i++)
            {
                craftRecipeList.Add(new CraftRecipe(craftRecipes[i].recipeKey, craftRecipes[i].recipeName, craftRecipes[i].completeItemKey, 
                    craftRecipes[i].necessaryItemKey1, craftRecipes[i].necessaryItemKey2, craftRecipes[i].necessaryItemKey3, craftRecipes[i].necessaryItemKey4,
                    craftRecipes[i].necessaryItemCount1, craftRecipes[i].necessaryItemCount2, craftRecipes[i].necessaryItemCount3, craftRecipes[i].necessaryItemCount4));
            }
        }
    }
    public Item SelectItem(int _key, int _amount = 1)
    {
        if(_key == -1)
            return null;
        else
        {
            Item _item = new Item(_key, null, 0 , 0, -1);
            switch(_key / 1000)
            {
                case 0:
                    for(int i = 0; i < hairList.Count; i++)
                    {
                        if (_key == hairList[i].itemKey)
                        {
                            Hair _hair = new Hair(hairList[i].itemKey, hairList[i].itemName, hairList[i].buyPrice, hairList[i].sellPrice, hairList[i].itemRank);
                            _item = _hair;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < faceHairList.Count; i++)
                    {
                        if (_key == faceHairList[i].itemKey)
                        {
                            FaceHair _faceHair = new FaceHair(faceHairList[i].itemKey, faceHairList[i].itemName, faceHairList[i].buyPrice, faceHairList[i].sellPrice, faceHairList[i].itemRank);
                            _item = _faceHair;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < clothList.Count; i++)
                    {
                        if (_key == clothList[i].itemKey)
                        {
                            Cloth _cloth = new Cloth(clothList[i].itemKey, clothList[i].itemName, clothList[i].buyPrice, clothList[i].sellPrice, clothList[i].defensivePower, clothList[i].equipLevel, clothList[i].disassembleItemKey, clothList[i].disassembleItemAmount, clothList[i].itemRank);
                            _item = _cloth;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < pantList.Count; i++)
                    {
                        if (_key == pantList[i].itemKey)
                        {
                            Pant _pant = new Pant(pantList[i].itemKey, pantList[i].itemName, pantList[i].buyPrice, pantList[i].sellPrice, pantList[i].defensivePower, pantList[i].equipLevel, pantList[i].disassembleItemKey, pantList[i].disassembleItemAmount, pantList[i].itemRank);
                            _item = _pant;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < helmetList.Count; i++)
                    {
                        if (_key == helmetList[i].itemKey)
                        {
                            Helmet _helmet = new Helmet(helmetList[i].itemKey, helmetList[i].itemName, helmetList[i].buyPrice, helmetList[i].sellPrice, helmetList[i].defensivePower, helmetList[i].equipLevel, helmetList[i].disassembleItemKey, helmetList[i].disassembleItemAmount, helmetList[i].itemRank);
                            _item = _helmet;
                        }
                    }
                    break;
                case 5:
                    for (int i = 0; i < armorList.Count; i++)
                    {
                        if (_key == armorList[i].itemKey)
                        {
                            Armor _armor = new Armor(armorList[i].itemKey, armorList[i].itemName, armorList[i].buyPrice, armorList[i].sellPrice, armorList[i].defensivePower, armorList[i].equipLevel, armorList[i].disassembleItemKey, armorList[i].disassembleItemAmount, armorList[i].itemRank);
                            _item = _armor;
                        }
                    }
                    break;
                case 6:
                    for (int i = 0; i < backList.Count; i++)
                    {
                        if (_key == backList[i].itemKey)
                        {
                            Back _back = new Back(backList[i].itemKey, backList[i].itemName, backList[i].buyPrice, backList[i].sellPrice, backList[i].defensivePower, backList[i].equipLevel, backList[i].disassembleItemKey, backList[i].disassembleItemAmount, backList[i].itemRank);
                            _item = _back;
                        }
                    }
                    break;
                case 7:
                    for (int i = 0; i < swordList.Count; i++)
                    {
                        if (_key == swordList[i].itemKey)
                        {
                            Sword _sword = new Sword(swordList[i].itemKey, swordList[i].itemName, swordList[i].buyPrice, swordList[i].sellPrice, swordList[i].attackType, swordList[i].weaponType, swordList[i].physicalDamage, swordList[i].magicalDamage,
                        swordList[i].atkRange, swordList[i].atkDistance, swordList[i].atkSpeed, swordList[i].skillKey1, swordList[i].skillKey2, swordList[i].equipLevel, swordList[i].disassembleItemKey, swordList[i].disassembleItemAmount, swordList[i].itemRank);
                            _item = _sword;
                        }
                    }
                    break;
                case 8:
                    for (int i = 0; i < shieldList.Count; i++)
                    {
                        if (_key == shieldList[i].itemKey)
                        {
                            Shield _shield = new Shield(shieldList[i].itemKey, shieldList[i].itemName, shieldList[i].buyPrice, shieldList[i].sellPrice, shieldList[i].attackType, shieldList[i].weaponType, shieldList[i].physicalDamage, shieldList[i].magicalDamage,
                        shieldList[i].atkRange, shieldList[i].atkDistance, shieldList[i].defensivePower, shieldList[i].atkSpeed, shieldList[i].skillKey1, shieldList[i].skillKey2, shieldList[i].equipLevel, shieldList[i].disassembleItemKey, shieldList[i].disassembleItemAmount, shieldList[i].itemRank);
                            _item = _shield;
                        }
                    }
                    break;
                case 9:
                    for (int i = 0; i < bowList.Count; i++)
                    {
                        if (_key == bowList[i].itemKey)
                        {
                            Bow _bow = new Bow(
                                bowList[i].itemKey, bowList[i].itemName, bowList[i].buyPrice, bowList[i].sellPrice, bowList[i].attackType, bowList[i].weaponType, bowList[i].physicalDamage, bowList[i].magicalDamage,
                                bowList[i].atkRange, bowList[i].atkDistance, bowList[i].atkSpeed, bowList[i].skillKey1, bowList[i].skillKey2, bowList[i].equipLevel, bowList[i].disassembleItemKey, bowList[i].disassembleItemAmount, bowList[i].itemRank);
                            _item = _bow;
                        }
                    }
                    break;
                case 10:
                    for (int i = 0; i < wandList.Count; i++)
                    {
                        if (_key == wandList[i].itemKey)
                        {
                            Wand _wand = new Wand(
                                wandList[i].itemKey, wandList[i].itemName, wandList[i].buyPrice, wandList[i].sellPrice, wandList[i].attackType, wandList[i].weaponType, wandList[i].physicalDamage, wandList[i].magicalDamage,
                                wandList[i].atkRange, wandList[i].atkDistance, wandList[i].atkSpeed, wandList[i].skillKey1, wandList[i].skillKey2, wandList[i].equipLevel, wandList[i].disassembleItemKey, wandList[i].disassembleItemAmount, wandList[i].itemRank);
                            _item = _wand;
                        }
                    }
                    break;
                case 11:
                    for (int i = 0; i < consumablesList.Count; i++)
                    {
                        if (_key == consumablesList[i].itemKey)
                        {
                            Consumables _consumables = new Consumables(
                                consumablesList[i].itemKey, consumablesList[i].itemName, consumablesList[i].buyPrice, consumablesList[i].sellPrice,
                                consumablesList[i].useEffect, consumablesList[i].target, consumablesList[i].durationTime, consumablesList[i].value, consumablesList[i].coolTime, consumablesList[i].itemRank);
                            _consumables.count = _amount;
                            _item = _consumables;
                        }
                    }
                    break;
                case 12:
                    for (int i = 0; i < miscellaneousList.Count; i++)
                    {
                        if (_key == miscellaneousList[i].itemKey)
                        {
                            Miscellaneous _miscellaneous = new Miscellaneous(
                                miscellaneousList[i].itemKey, miscellaneousList[i].itemName, miscellaneousList[i].buyPrice, miscellaneousList[i].sellPrice, miscellaneousList[i].purpose, miscellaneousList[i].itemRank);
                            _miscellaneous.count = _amount;
                            _item = _miscellaneous;
                        }
                    }
                    break;
            }
            return _item;
        }
    }

    public Enemy SelectRushEnemy(int _key)
    {
        Enemy _enemy = null;

        for (int i = 0; i < rushEnemyList.Count; i++)
        {
            if (_key == rushEnemyList[i].enemyKey)
                _enemy = rushEnemyList[i];
        }
        return _enemy;
    }

    public Skill SelectSkill(int _key)
    {
        Skill _skill = null;
        if(_key < 1000)
        {
            for(int i =0; i< activeSkillList.Count; i++)
            {
                if(activeSkillList[i].skillKey == _key)
                    _skill = activeSkillList[i];
            }
        }
        else
        {
            for (int i = 0; i < passiveSkillList.Count; i++)
            {
                if (passiveSkillList[i].skillKey == _key)
                    _skill = passiveSkillList[i];
            }
        }
        return _skill;
    }

    public Grace SelectGrace(int _key)
    {
        Grace _grace = null;
        for (int i = 0; i < graceList.Count; i++)
        {
            if (graceList[i].graceKey == _key)
                _grace = graceList[i];
            else
                Debug.Log("해당 은총이 없습니다.");
        }
        return _grace;
    }
    public CraftRecipe SelectCraftRecipe(int _key)
    {
        CraftRecipe _craftRecipe = null;
        for (int i = 0; i < graceList.Count; i++)
        {
            if (craftRecipeList[i].recipeKey == _key)
                _craftRecipe = craftRecipeList[i];
            else
                Debug.Log("해당 제작 레시피가 없습니다.");
        }
        return _craftRecipe;
    }
}