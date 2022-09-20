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
    public List<Shield> shieldList = new List<Shield>();
    public List<Sword> swordList = new List<Sword>();
    public List<Exe> exeList = new List<Exe>();
    public List<Spear> spearList = new List<Spear>();
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
    public List<Skill> skillList = new List<Skill>();

    [Header("Grace")]
    public List<GraceConditionWho> graceConditionWhoList = new List<GraceConditionWho>();
    public List<GraceConditionWhat> graceConditionWhatList = new List<GraceConditionWhat>();
    public List<GraceConditionHow> graceConditionHowList = new List<GraceConditionHow>();
    public List<GraceResultWho> graceResultWhoList = new List<GraceResultWho>();
    public List<GraceResultTarget> graceResultTargetList = new List<GraceResultTarget>();
    public List<GraceResultWhat> graceResultWhatList = new List<GraceResultWhat>();
    public List<GraceResultHow> graceResultHowList = new List<GraceResultHow>();


    [Header("BigGrace")]
    public List<BigGrace> warriorGraceList = new List<BigGrace>();
    public List<BigGrace> rangedGraceList = new List<BigGrace>();
    public List<BigGrace> magicGraceList = new List<BigGrace>();
    public List<BigGrace> commanderGraceList = new List<BigGrace>();

    [Header("CraftRecipe")]
    public List<CraftRecipe> craftRecipeList = new List<CraftRecipe>();

    [Header("AltarProperty")]
    public List<AltarProperty> altarPropertyList = new List<AltarProperty>();

    [Header("GameScript")]
    public List<GameScript> gameScriptList = new List<GameScript>();
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
                hairList.Add(new Hair(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].itemRank));
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
                faceHairList.Add(new FaceHair(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].itemRank));
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
                clothList.Add(new Cloth(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
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
                pantList.Add(new Pant(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
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
                helmetList.Add(new Helmet(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
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
                armorList.Add(new Armor(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank)); ;
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
                backList.Add(new Back(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("7_Shield")))
        {
            Debug.Log("경로에 방패 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("7_Shield")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                shieldList.Add(new Shield(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].defensivePower, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("8_Sword")))
        {
            Debug.Log("경로에 검 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("8_Sword")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                swordList.Add(new Sword(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].attackType,items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].skillKey3, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("9_Exe")))
        {
            Debug.Log("경로에 도끼 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("9_Exe")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                exeList.Add(new Exe(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].attackType, items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].skillKey3, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("10_Spear")))
        {
            Debug.Log("경로에 창 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("10_Spear")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                spearList.Add(new Spear(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].attackType, items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].skillKey3, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("11_Bow")))
        {
            Debug.Log("경로에 활 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("11_Bow")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                bowList.Add(new Bow(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].attackType, items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].skillKey3, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("12_Wand")))
        {
            Debug.Log("경로에 지팡이 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("12_Wand")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                wandList.Add(new Wand(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].attackType, items[i].weaponType, items[i].physicalDamage, 
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].skillKey3, items[i].equipLevel, items[i].disassembleItemKey, items[i].disassembleItemAmount, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("13_Consumables")))
        {
            Debug.Log("경로에 소비품 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("13_Consumables")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                consumablesList.Add(new Consumables(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].useEffect, items[i].target, items[i].durationTime, items[i].value, items[i].maxCoolTime, items[i].itemRank));
            }

        }
        if (!File.Exists(CombinePath("14_Miscellaneous")))
        {
            Debug.Log("경로에 기타템 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("14_Miscellaneous")));
            Item[] items = JsonHelper.FromJson<Item>(loadJson);
            for (var i = 0; i < items.Length; i++)
            {
                miscellaneousList.Add(new Miscellaneous(items[i].itemKey, items[i].itemName, items[i].itemKorName, items[i].buyPrice, items[i].sellPrice, items[i].purpose, items[i].itemRank));
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
        if (!File.Exists(CombinePath("Skill")))
        {
            Debug.Log("경로에 스킬 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("Skill")));
            Skill[] skill = JsonHelper.FromJson<Skill>(loadJson);
            for (var i = 0; i < skill.Length; i++)
            {
                skillList.Add(new Skill(skill[i].skillKey, skill[i].skillName, skill[i].skillKorName,skill[i].skillExplain, skill[i].skillLevel, skill[i].skillVariable,skill[i].skillType, skill[i].skillRange, skill[i].maxCoolTime, skill[i].skillHitCount,
                    skill[i].skillValue1, skill[i].skillValue2, skill[i].skillValue3, skill[i].skillValue4,
                    skill[i].skillValue5, skill[i].skillValue6, skill[i].skillValue7, skill[i].skillValue8, skill[i].skillValue9, skill[i].skillValue10,
                    skill[i].skillFigures1, skill[i].skillFigures2, skill[i].skillFigures3, skill[i].skillFigures4, skill[i].skillFigures5,
                    skill[i].skillFigures6, skill[i].skillFigures7, skill[i].skillFigures8, skill[i].skillFigures9, skill[i].skillFigures10));
            }
        }
        if (!File.Exists(CombinePath("ConditionWho")))
        {
            Debug.Log("경로에 조건부 누구의 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ConditionWho")));
            GraceConditionWho[] grace = JsonHelper.FromJson<GraceConditionWho>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceConditionWhoList.Add(new GraceConditionWho(grace[i].graceKey, grace[i].graceKorName, grace[i].weightedValue));
            }
        }
        if (!File.Exists(CombinePath("ConditionWhat")))
        {
            Debug.Log("경로에 조건부 무엇의 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ConditionWhat")));
            GraceConditionWhat[] grace = JsonHelper.FromJson<GraceConditionWhat>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceConditionWhatList.Add(new GraceConditionWhat(grace[i].graceKey, grace[i].graceKorName, grace[i].weightedValue));
            }
        }
        if (!File.Exists(CombinePath("ConditionHow")))
        {
            Debug.Log("경로에 조건부 어떻게의 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ConditionHow")));
            GraceConditionHow[] grace = JsonHelper.FromJson<GraceConditionHow>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceConditionHowList.Add(new GraceConditionHow(grace[i].graceKey, grace[i].graceKorName, grace[i].weightedValue));
            }
        }
        if (!File.Exists(CombinePath("ResultWho")))
        {
            Debug.Log("경로에 결과부 누구의 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ResultWho")));
            GraceResultWho[] grace = JsonHelper.FromJson<GraceResultWho>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceResultWhoList.Add(new GraceResultWho(grace[i].graceKey, grace[i].graceKorName, grace[i].weightedValue));
            }
        }
        if (!File.Exists(CombinePath("ResultTarget")))
        {
            Debug.Log("경로에 결과부 타겟의 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ResultTarget")));
            GraceResultTarget[] grace = JsonHelper.FromJson<GraceResultTarget>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceResultTargetList.Add(new GraceResultTarget(grace[i].graceKey, grace[i].graceKorName, grace[i].weightedValue));
            }
        }
        if (!File.Exists(CombinePath("ResultWhat")))
        {
            Debug.Log("경로에 결과부 무엇의 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ResultWhat")));
            GraceResultWhat[] grace = JsonHelper.FromJson<GraceResultWhat>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceResultWhatList.Add(new GraceResultWhat(grace[i].graceKey, grace[i].graceKorName, grace[i].weightedValue));
            }
        }
        if (!File.Exists(CombinePath("ResultHow")))
        {
            Debug.Log("경로에 결과부 어떻게의 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("ResultHow")));
            GraceResultHow[] grace = JsonHelper.FromJson<GraceResultHow>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                graceResultHowList.Add(new GraceResultHow(grace[i].graceKey, grace[i].graceKorName, grace[i].weightedValue));
            }
        }
        if (!File.Exists(CombinePath("WarriorGrace")))
        {
            Debug.Log("경로에 밀리 은총 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("WarriorGrace")));
            BigGrace[] grace = JsonHelper.FromJson<BigGrace>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                warriorGraceList.Add(new BigGrace(grace[i].graceKey, grace[i].graceName, grace[i].explain, grace[i].necessaryGraceKey, grace[i].conditionWho, grace[i].conditionWhat, grace[i].conditionValue, grace[i].conditionHow,
                    grace[i].resultWho, grace[i].resultTarget1, grace[i].resultTarget2, grace[i].resultWhat1, grace[i].resultWhat2, grace[i].resultValue1, grace[i].resultValue2, grace[i].resultHow1, grace[i].resultHow2));
            }
        }
        if (!File.Exists(CombinePath("RangedGrace")))
        {
            Debug.Log("경로에 궁수 은총 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("RangedGrace")));
            BigGrace[] grace = JsonHelper.FromJson<BigGrace>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                           
                rangedGraceList.Add(new BigGrace(grace[i].graceKey, grace[i].graceName, grace[i].explain, grace[i].necessaryGraceKey, grace[i].conditionWho, grace[i].conditionWhat, grace[i].conditionValue, grace[i].conditionHow,
                    grace[i].resultWho, grace[i].resultTarget1, grace[i].resultTarget2, grace[i].resultWhat1, grace[i].resultWhat2, grace[i].resultValue1, grace[i].resultValue2, grace[i].resultHow1, grace[i].resultHow2));
            }
        }
        if (!File.Exists(CombinePath("MagicGrace")))
        {
            Debug.Log("경로에 은총 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("MagicGrace")));
            BigGrace[] grace = JsonHelper.FromJson<BigGrace>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                magicGraceList.Add(new BigGrace(grace[i].graceKey, grace[i].graceName, grace[i].explain, grace[i].necessaryGraceKey, grace[i].conditionWho, grace[i].conditionWhat, grace[i].conditionValue, grace[i].conditionHow,
                    grace[i].resultWho, grace[i].resultTarget1, grace[i].resultTarget2, grace[i].resultWhat1, grace[i].resultWhat2, grace[i].resultValue1, grace[i].resultValue2, grace[i].resultHow1, grace[i].resultHow2));
            }
        }

        if (!File.Exists(CombinePath("CommanderGrace")))
        {
            Debug.Log("경로에 은총 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("CommanderGrace")));
            BigGrace[] grace = JsonHelper.FromJson<BigGrace>(loadJson);
            for (var i = 0; i < grace.Length; i++)
            {
                commanderGraceList.Add(new BigGrace(grace[i].graceKey, grace[i].graceName, grace[i].explain, grace[i].necessaryGraceKey, grace[i].conditionWho, grace[i].conditionWhat, grace[i].conditionValue, grace[i].conditionHow,
                   grace[i].resultWho, grace[i].resultTarget1, grace[i].resultTarget2, grace[i].resultWhat1, grace[i].resultWhat2, grace[i].resultValue1, grace[i].resultValue2, grace[i].resultHow1, grace[i].resultHow2));
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
        if (!File.Exists(CombinePath("AltarProperty")))
        {
            Debug.Log("경로에 제단 특성 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("AltarProperty")));
            AltarProperty[] altarProperties = JsonHelper.FromJson<AltarProperty>(loadJson);
            for (var i = 0; i < altarProperties.Length; i++)
            {
                altarPropertyList.Add(new AltarProperty(altarProperties[i].propertyKey, altarProperties[i].propertyName,
                    altarProperties[i].rateOfMoneyIncreaseBySection_1, altarProperties[i].rateOfMoneyIncreaseBySection_2, altarProperties[i].rateOfMoneyIncreaseBySection_3,
                    altarProperties[i].rateOfMoneyIncreaseBySection_4, altarProperties[i].rateOfMoneyIncreaseBySection_5, altarProperties[i].rateOfMoneyIncreaseBySection_6, altarProperties[i].rateOfMoneyIncreaseBySection_7
                    , altarProperties[i].rateOfMoneyIncreaseBySection_8, altarProperties[i].rateOfMoneyIncreaseBySection_9, altarProperties[i].rateOfMoneyIncreaseBySection_10, altarProperties[i].rateOfValueIncreaseBySection_1, altarProperties[i].rateOfValueIncreaseBySection_2
                    , altarProperties[i].rateOfValueIncreaseBySection_3, altarProperties[i].rateOfValueIncreaseBySection_4, altarProperties[i].rateOfValueIncreaseBySection_5
                    , altarProperties[i].rateOfValueIncreaseBySection_6, altarProperties[i].rateOfValueIncreaseBySection_7, altarProperties[i].rateOfValueIncreaseBySection_8, altarProperties[i].rateOfValueIncreaseBySection_9, altarProperties[i].rateOfValueIncreaseBySection_10));
            }
        }
        if (!File.Exists(CombinePath("GameScript")))
        {
            Debug.Log("경로에 대본 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("GameScript")));
            GameScript[] gameScripts = JsonHelper.FromJson<GameScript>(loadJson);
            for (var i = 0; i < gameScripts.Length; i++)
            {
                gameScriptList.Add(new GameScript(gameScripts[i].scriptKey, gameScripts[i].actorNum0, gameScripts[i].actorNum1, gameScripts[i].actorNum2, gameScripts[i].actorNum3,gameScripts[i].actorNum4, gameScripts[i].actorNum5, gameScripts[i].actorNum6, gameScripts[i].actorNum7, gameScripts[i].actorNum8, gameScripts[i].actorNum9,
                    gameScripts[i].script0, gameScripts[i].script1, gameScripts[i].script2, gameScripts[i].script3, gameScripts[i].script4, gameScripts[i].script5, gameScripts[i].script6, gameScripts[i].script7, gameScripts[i].script8, gameScripts[i].script9,
                    gameScripts[i].scriptSpeed0, gameScripts[i].scriptSpeed1, gameScripts[i].scriptSpeed2, gameScripts[i].scriptSpeed3, gameScripts[i].scriptSpeed4, gameScripts[i].scriptSpeed5, gameScripts[i].scriptSpeed6, gameScripts[i].scriptSpeed7, gameScripts[i].scriptSpeed8, gameScripts[i].scriptSpeed9,
                    gameScripts[i].scriptAniSpeed0, gameScripts[i].scriptAniSpeed1, gameScripts[i].scriptAniSpeed2, gameScripts[i].scriptAniSpeed3, gameScripts[i].scriptAniSpeed4, gameScripts[i].scriptAniSpeed5, gameScripts[i].scriptAniSpeed6, gameScripts[i].scriptAniSpeed7, gameScripts[i].scriptAniSpeed8, gameScripts[i].scriptAniSpeed9,
                    gameScripts[i].timeLag0, gameScripts[i].timeLag1, gameScripts[i].timeLag2, gameScripts[i].timeLag3, gameScripts[i].timeLag4, gameScripts[i].timeLag5, gameScripts[i].timeLag6, gameScripts[i].timeLag7, gameScripts[i].timeLag8));
            }
        }
    }
    public Item SelectItem(int _key, int _amount = 1)
    {
        if(_key == -1)
            return null;
        else
        {
            Item _item = new Item(_key, null, null, 0 , 0, -1);
            switch(_key / 1000)
            {
                case 0:
                    for(int i = 0; i < hairList.Count; i++)
                    {
                        if (_key == hairList[i].itemKey)
                        {
                            Hair _hair = new Hair(hairList[i].itemKey, hairList[i].itemName, hairList[i].itemKorName, hairList[i].buyPrice, hairList[i].sellPrice, hairList[i].itemRank);
                            _item = _hair;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < faceHairList.Count; i++)
                    {
                        if (_key == faceHairList[i].itemKey)
                        {
                            FaceHair _faceHair = new FaceHair(faceHairList[i].itemKey, faceHairList[i].itemName, faceHairList[i].itemKorName, faceHairList[i].buyPrice, faceHairList[i].sellPrice, faceHairList[i].itemRank);
                            _item = _faceHair;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < clothList.Count; i++)
                    {
                        if (_key == clothList[i].itemKey)
                        {
                            Cloth _cloth = new Cloth(clothList[i].itemKey, clothList[i].itemName, clothList[i].itemKorName, clothList[i].buyPrice, clothList[i].sellPrice, clothList[i].defensivePower, clothList[i].equipLevel, clothList[i].disassembleItemKey, clothList[i].disassembleItemAmount, clothList[i].itemRank);
                            _item = _cloth;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < pantList.Count; i++)
                    {
                        if (_key == pantList[i].itemKey)
                        {
                            Pant _pant = new Pant(pantList[i].itemKey, pantList[i].itemName, pantList[i].itemKorName, pantList[i].buyPrice, pantList[i].sellPrice, pantList[i].defensivePower, pantList[i].equipLevel, pantList[i].disassembleItemKey, pantList[i].disassembleItemAmount, pantList[i].itemRank);
                            _item = _pant;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < helmetList.Count; i++)
                    {
                        if (_key == helmetList[i].itemKey)
                        {
                            Helmet _helmet = new Helmet(helmetList[i].itemKey, helmetList[i].itemName, helmetList[i].itemKorName, helmetList[i].buyPrice, helmetList[i].sellPrice,
                                helmetList[i].defensivePower, helmetList[i].equipLevel, helmetList[i].disassembleItemKey, helmetList[i].disassembleItemAmount, helmetList[i].itemRank);
                            _item = _helmet;
                        }
                    }
                    break;
                case 5:
                    for (int i = 0; i < armorList.Count; i++)
                    {
                        if (_key == armorList[i].itemKey)
                        {
                            Armor _armor = new Armor(armorList[i].itemKey, armorList[i].itemName, armorList[i].itemKorName, armorList[i].buyPrice, armorList[i].sellPrice, armorList[i].defensivePower, armorList[i].equipLevel, armorList[i].disassembleItemKey, armorList[i].disassembleItemAmount, armorList[i].itemRank);
                            _item = _armor;
                        }
                    }
                    break;
                case 6:
                    for (int i = 0; i < backList.Count; i++)
                    {
                        if (_key == backList[i].itemKey)
                        {
                            Back _back = new Back(backList[i].itemKey, backList[i].itemName, backList[i].itemKorName, backList[i].buyPrice, backList[i].sellPrice, backList[i].defensivePower, backList[i].equipLevel, backList[i].disassembleItemKey, backList[i].disassembleItemAmount, backList[i].itemRank);
                            _item = _back;
                        }
                    }
                    break;
                case 7:
                    for (int i = 0; i < shieldList.Count; i++)
                    {
                        if (_key == shieldList[i].itemKey)
                        {
                            Shield _shield = new Shield(
                                shieldList[i].itemKey, shieldList[i].itemName, shieldList[i].itemKorName, shieldList[i].buyPrice, shieldList[i].sellPrice, shieldList[i].defensivePower,
                                shieldList[i].equipLevel, shieldList[i].disassembleItemKey, shieldList[i].disassembleItemAmount, shieldList[i].itemRank);
                            _item = _shield;
                        }
                    }
                    break;
                case 8:
                    for (int i = 0; i < swordList.Count; i++)
                    {
                        if (_key == swordList[i].itemKey)
                        {
                            Sword _sword = new Sword(swordList[i].itemKey, swordList[i].itemName, swordList[i].itemKorName, swordList[i].buyPrice, swordList[i].sellPrice, swordList[i].attackType, swordList[i].weaponType, swordList[i].physicalDamage, swordList[i].magicalDamage,
                        swordList[i].atkRange, swordList[i].atkDistance, swordList[i].atkSpeed, swordList[i].skillKey1, swordList[i].skillKey2, swordList[i].skillKey3, swordList[i].equipLevel, swordList[i].disassembleItemKey, swordList[i].disassembleItemAmount, swordList[i].itemRank);
                            _item = _sword;
                        }
                    }
                    break;

                case 9:
                    for (int i = 0; i < exeList.Count; i++)
                    {
                        if (_key == exeList[i].itemKey)
                        {
                            Exe _exe = new Exe(exeList[i].itemKey, exeList[i].itemName, exeList[i].itemKorName, exeList[i].buyPrice, exeList[i].sellPrice, exeList[i].attackType, exeList[i].weaponType, exeList[i].physicalDamage, exeList[i].magicalDamage,
                        exeList[i].atkRange, exeList[i].atkDistance, exeList[i].atkSpeed, exeList[i].skillKey1, exeList[i].skillKey2, exeList[i].skillKey3, exeList[i].equipLevel, exeList[i].disassembleItemKey, exeList[i].disassembleItemAmount, exeList[i].itemRank);
                            _item = _exe;
                        }
                    }
                    break;
                case 10:
                    for (int i = 0; i < spearList.Count; i++)
                    {
                        if (_key == spearList[i].itemKey)
                        {
                            Spear _spear = new Spear(spearList[i].itemKey, spearList[i].itemName, spearList[i].itemKorName, spearList[i].buyPrice, spearList[i].sellPrice, spearList[i].attackType, spearList[i].weaponType, spearList[i].physicalDamage, spearList[i].magicalDamage,
                        spearList[i].atkRange, spearList[i].atkDistance, spearList[i].atkSpeed, spearList[i].skillKey1, spearList[i].skillKey2, spearList[i].skillKey3, spearList[i].equipLevel, spearList[i].disassembleItemKey, spearList[i].disassembleItemAmount, spearList[i].itemRank);
                            _item = _spear;
                        }
                    }
                    break;
                case 11:
                    for (int i = 0; i < bowList.Count; i++)
                    {
                        if (_key == bowList[i].itemKey)
                        {
                            Bow _bow = new Bow(
                                bowList[i].itemKey, bowList[i].itemName, bowList[i].itemKorName, bowList[i].buyPrice, bowList[i].sellPrice, bowList[i].attackType, bowList[i].weaponType, bowList[i].physicalDamage, bowList[i].magicalDamage,
                                bowList[i].atkRange, bowList[i].atkDistance, bowList[i].atkSpeed, bowList[i].skillKey1, bowList[i].skillKey2, bowList[i].skillKey3, bowList[i].equipLevel, bowList[i].disassembleItemKey, bowList[i].disassembleItemAmount, bowList[i].itemRank);
                            _item = _bow;
                        }
                    }
                    break;
                case 12:
                    for (int i = 0; i < wandList.Count; i++)
                    {
                        if (_key == wandList[i].itemKey)
                        {
                            Wand _wand = new Wand(
                                wandList[i].itemKey, wandList[i].itemName, wandList[i].itemKorName, wandList[i].buyPrice, wandList[i].sellPrice, wandList[i].attackType, wandList[i].weaponType, wandList[i].physicalDamage, wandList[i].magicalDamage,
                                wandList[i].atkRange, wandList[i].atkDistance, wandList[i].atkSpeed, wandList[i].skillKey1, wandList[i].skillKey2, wandList[i].skillKey3, wandList[i].equipLevel, wandList[i].disassembleItemKey, wandList[i].disassembleItemAmount, wandList[i].itemRank);
                            _item = _wand;
                        }
                    }
                    break;
                case 13:
                    for (int i = 0; i < consumablesList.Count; i++)
                    {
                        if (_key == consumablesList[i].itemKey)
                        {
                            Consumables _consumables = new Consumables(
                                consumablesList[i].itemKey, consumablesList[i].itemName, consumablesList[i].itemKorName, consumablesList[i].buyPrice, consumablesList[i].sellPrice,
                                consumablesList[i].useEffect, consumablesList[i].target, consumablesList[i].durationTime, consumablesList[i].value, consumablesList[i].maxCoolTime, consumablesList[i].itemRank);
                            _consumables.count = _amount;
                            _item = _consumables;
                        }
                    }
                    break;
                case 14:
                    for (int i = 0; i < miscellaneousList.Count; i++)
                    {
                        if (_key == miscellaneousList[i].itemKey)
                        {
                            Miscellaneous _miscellaneous = new Miscellaneous(
                                miscellaneousList[i].itemKey, miscellaneousList[i].itemName, miscellaneousList[i].itemKorName, miscellaneousList[i].buyPrice, miscellaneousList[i].sellPrice, miscellaneousList[i].purpose, miscellaneousList[i].itemRank);
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
        Skill _skill = new Skill(-1,"","","",-1,-1,-1, -1 ,1,-1,-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);

        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].skillKey == _key)
            {
                Skill _selectSkill = new Skill(skillList[i].skillKey, skillList[i].skillName, skillList[i].skillKorName, skillList[i].skillExplain, skillList[i].skillLevel, skillList[i].skillVariable, skillList[i].skillType, skillList[i].skillRange, skillList[i].maxCoolTime, skillList[i].skillHitCount,
                    skillList[i].skillValue1, skillList[i].skillValue2, skillList[i].skillValue3, skillList[i].skillValue4, skillList[i].skillValue5, skillList[i].skillValue6, skillList[i].skillValue7, skillList[i].skillValue8, skillList[i].skillValue9, skillList[i].skillValue10,
                    skillList[i].skillFigures1, skillList[i].skillFigures2, skillList[i].skillFigures3, skillList[i].skillFigures4, skillList[i].skillFigures5, skillList[i].skillFigures6, skillList[i].skillFigures7, skillList[i].skillFigures8, skillList[i].skillFigures9, skillList[i].skillFigures10);
                _skill = _selectSkill;
            }
        }
        return _skill;
    }

    public Grace SelectGrace(int _key)
    {
        Grace _grace = null;
        switch(_key/ 1000)
        {
            case 0:
                for(int i = 0; i < graceConditionWhoList.Count; i++)
                {
                    if (graceConditionWhoList[i].graceKey == _key)
                        _grace = graceConditionWhoList[i];
                    else
                        Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 1:
                for (int i = 0; i < graceConditionWhatList.Count; i++)
                {
                    if (graceConditionWhatList[i].graceKey == _key)
                        _grace = graceConditionWhatList[i];
                    else
                        Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 2:
                for (int i = 0; i < graceConditionHowList.Count; i++)
                {
                    if (graceConditionHowList[i].graceKey == _key)
                        _grace = graceConditionHowList[i];
                    else
                        Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 3:
                for (int i = 0; i < graceResultWhoList.Count; i++)
                {
                    if (graceResultWhoList[i].graceKey == _key)
                        _grace = graceResultWhoList[i];
                    else
                        Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 4:
                for (int i = 0; i < graceResultTargetList.Count; i++)
                {
                    if (graceResultTargetList[i].graceKey == _key)
                        _grace = graceResultTargetList[i];
                    else
                        Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 5:
                for (int i = 0; i < graceResultWhatList.Count; i++)
                {
                    if (graceResultWhatList[i].graceKey == _key)
                        _grace = graceResultWhatList[i];
                    else
                        Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
            case 6:
                for (int i = 0; i < graceResultHowList.Count; i++)
                {
                    if (graceResultHowList[i].graceKey == _key)
                        _grace = graceResultHowList[i];
                    else
                        Debug.Log("해당 은총 구성부가 없습니다.");
                }
                break;
        }
        return _grace;
    }

    public BigGrace SelectBigGrace(int _key)
    {
        BigGrace _grace = null;
        switch(_key / 1000)
        {
            case 0:
                for (int i = 0; i < warriorGraceList.Count; i++)
                {
                    if (warriorGraceList[i].graceKey == _key)
                        _grace = warriorGraceList[i];
                    else
                        Debug.Log("해당 은총이 없습니다.");
                }
                break;
            case 1:
                for (int i = 0; i < rangedGraceList.Count; i++)
                {
                    if (rangedGraceList[i].graceKey == _key)
                        _grace = rangedGraceList[i];
                    else
                        Debug.Log("해당 은총이 없습니다.");
                }
                break;
            case 2:
                for (int i = 0; i < magicGraceList.Count; i++)
                {
                    if (magicGraceList[i].graceKey == _key)
                        _grace = magicGraceList[i];
                    else
                        Debug.Log("해당 은총이 없습니다.");
                }
                break;
            case 3:
                for (int i = 0; i < commanderGraceList.Count; i++)
                {
                    if (commanderGraceList[i].graceKey == _key)
                        _grace = commanderGraceList[i];
                    else
                        Debug.Log("해당 은총이 없습니다.");
                }
                break;

        }
        return _grace;
    }
    public CraftRecipe SelectCraftRecipe(int _key)
    {
        CraftRecipe _craftRecipe = null;
        for (int i = 0; i < craftRecipeList.Count; i++)
        {
            if (craftRecipeList[i].recipeKey == _key)
                _craftRecipe = craftRecipeList[i];
            else
                Debug.Log("해당 제작 레시피가 없습니다.");
        }
        return _craftRecipe;
    }
    public AltarProperty SelectAltarProperty(int _key)
    {
        AltarProperty _altarProperty = null;
        for (int i = 0; i < altarPropertyList.Count; i++)
        {
            if (altarPropertyList[i].propertyKey == _key)
                _altarProperty = altarPropertyList[i];
            else
                Debug.Log("해당 제단 특성이 없습니다.");
        }
        return _altarProperty;
    }
    public GameScript SelectGameScript(int _key)
    {
        GameScript _gameScript = null;
        for (int i = 0; i < gameScriptList.Count; i++)
        {
            if (gameScriptList[i].scriptKey == _key)
                _gameScript = gameScriptList[i];
            else
                Debug.Log("해당 제작 레시피가 없습니다.");
        }
        return _gameScript;
    }
}