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
    public List<RushEnemy> rushEnemyList = new List<RushEnemy>();

    [Header("Stage")]
    public List<Stage> stageList = new List<Stage>();

    [Header("Skill")]
    public List<ActiveSkill> activeSkillList = new List<ActiveSkill>();
    public List<PassiveSkill> passiveSkillList = new List<PassiveSkill>();

    [Header("Grace")]
    public List<Grace> graceList = new List<Grace>();
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
                hairList.Add(new Hair(items[i].itemKey, items[i].itemName));
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
                faceHairList.Add(new FaceHair(items[i].itemKey, items[i].itemName));
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
                clothList.Add(new Cloth(items[i].itemKey, items[i].itemName,items[i].defensivePower, items[i].equipLevel));
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
                pantList.Add(new Pant(items[i].itemKey, items[i].itemName, items[i].defensivePower, items[i].equipLevel));
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
                helmetList.Add(new Helmet(items[i].itemKey, items[i].itemName,items[i].defensivePower, items[i].equipLevel));
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
                armorList.Add(new Armor(items[i].itemKey, items[i].itemName,items[i].defensivePower, items[i].equipLevel));
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
                backList.Add(new Back(items[i].itemKey, items[i].itemName,items[i].defensivePower, items[i].equipLevel));
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
                swordList.Add(new Sword(items[i].itemKey, items[i].itemName,items[i].attackType,items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel));
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
                shieldList.Add(new Shield(items[i].itemKey, items[i].itemName, items[i].attackType, items[i].weaponType, items[i].physicalDamage, 
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance,items[i].defensivePower, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel));
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
                bowList.Add(new Bow(items[i].itemKey, items[i].itemName, items[i].attackType, items[i].weaponType, items[i].physicalDamage,
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel));
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
                wandList.Add(new Wand(items[i].itemKey, items[i].itemName, items[i].attackType, items[i].weaponType, items[i].physicalDamage, 
                    items[i].magicalDamage, items[i].atkRange, items[i].atkDistance, items[i].atkSpeed, items[i].skillKey1, items[i].skillKey2, items[i].equipLevel));
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
                consumablesList.Add(new Consumables(items[i].itemKey, items[i].itemName,items[i].useEffect,items[i].target,items[i].durationTime,items[i].value));
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
                miscellaneousList.Add(new Miscellaneous(items[i].itemKey, items[i].itemName,items[i].purpose));
            }

        }
        if (!File.Exists(CombinePath("Enemy")))
        {
            Debug.Log("경로에 적 데이터 베이스가 존재하지 않습니다.");
        }
        else
        {
            string loadJson = fixJson(File.ReadAllText(CombinePath("Enemy")));
            RushEnemy[] rushEnemies = JsonHelper.FromJson<RushEnemy>(loadJson);
            for (var i = 0; i < rushEnemies.Length; i++)
            {
                rushEnemyList.Add(new RushEnemy(rushEnemies[i].objectName, rushEnemies[i].enemyKey, rushEnemies[i].enemyType, rushEnemies[i].hp, rushEnemies[i].mp,
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
            Grace[] skill = JsonHelper.FromJson<Grace>(loadJson);
            for (var i = 0; i < skill.Length; i++)
            {
                graceList.Add(new Grace(skill[i].graceKey, skill[i].explain, skill[i].necessaryGraceKey));
            }
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

    public RushEnemy SelectRushEnemy(int _key)
    {
        RushEnemy _enemy = null;

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
        }
        return _grace;
    }
}