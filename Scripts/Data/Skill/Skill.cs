using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public int skillKey;
    public string skillName;
    public string skillKorName;
    public string skillExplain;
    public Sprite singleSprite = null;
    public int skillLevel;
    public int skillType;
    public int skillVariable;
    public int skillValue1;
    public int skillValue2;
    public int skillValue3;
    public int skillValue4;
    public int skillValue5;
    public int skillValue6;
    public int skillValue7;
    public int skillValue8;
    public int skillValue9;
    public int skillValue10;
    public float skillFigures1;
    public float skillFigures2;
    public float skillFigures3;
    public float skillFigures4;
    public float skillFigures5;
    public float skillFigures6;
    public float skillFigures7;
    public float skillFigures8;
    public float skillFigures9;
    public float skillFigures10;
    public float coolTime;
    public float maxCoolTime;
    public bool isCoolTime = false;
    public int skillHitCount = 0;
    public string targetStatus = null;
    public float skillRange = 0f;
    public Skill(int _skillKey, string _skillName, string _skillKorName, string _skillExplain,
        int _skillLevel, int _skillVarable
        , int _skillType, float _skillRange, float _maxCoolTime, int _skillHitCount
        , int  _skillValue1, int _skillValue2, int _skillValue3
        , int _skillValue4, int _skillValue5, int _skillValue6
        , int _skillValue7, int _skillValue8, int _skillValue9
        , int _skillValue10, float _skillFigures1, float _skillFigures2
        , float _skillFigures3, float _skillFigures4, float _skillFigures5
        , float _skillFigures6, float _skillFigures7, float _skillFigures8
        , float _skillFigures9, float _skillFigures10)
    {
        skillKey = _skillKey;
        skillName = _skillName;
        skillKorName = _skillKorName;
        skillExplain = _skillExplain;
        skillLevel = _skillLevel;
        skillVariable = _skillVarable;
        skillType = _skillType;
        skillRange = _skillRange;
        maxCoolTime = _maxCoolTime;
        skillHitCount = _skillHitCount;
        skillValue1 = _skillValue1;
        skillValue2 = _skillValue2;
        skillValue3 = _skillValue3;
        skillValue4 = _skillValue4;
        skillValue5 = _skillValue5;
        skillValue6 = _skillValue6;
        skillValue7 = _skillValue7;
        skillValue8 = _skillValue8;
        skillValue9 = _skillValue9;
        skillValue10 = _skillValue10;
        skillFigures1 = _skillFigures1;
        skillFigures2 = _skillFigures2;
        skillFigures3 = _skillFigures3;
        skillFigures4 = _skillFigures4;
        skillFigures5 = _skillFigures5;
        skillFigures6 = _skillFigures6;
        skillFigures7 = _skillFigures7;
        skillFigures8 = _skillFigures8;
        skillFigures9 = _skillFigures9;
        skillFigures10 = _skillFigures10;


        singleSprite = Resources.Load("Sprites/17_SkillIcon/" + skillName, typeof(Sprite)) as Sprite;
    }
}
