using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveSkill : Skill
{
    public ActiveSkill(int _skillKey, string _skillName, int _skillLevel, int _skillType, int _skillVarable, float _skillRange, int _skillValue1, int _skillValue2, int _skillValue3, int _skillValue4, int _skillValue5, int _skillValue6, int _skillValue7, int _skillValue8, int _skillValue9, int _skillValue10, float _skillFigures1, float _skillFigures2, float _skillFigures3, float _skillFigures4, float _skillFigures5, float _skillFigures6, float _skillFigures7, float _skillFigures8, float _skillFigures9, float _skillFigures10, float _maxCoolTime, int _skillHitCount) 
        : base(_skillKey, _skillName, _skillLevel, _skillVarable, _skillValue1, _skillValue2, _skillValue3, _skillValue4, _skillValue5, _skillValue6, _skillValue7, _skillValue8, _skillValue9, _skillValue10, _skillFigures1, _skillFigures2, _skillFigures3, _skillFigures4, _skillFigures5, _skillFigures6, _skillFigures7, _skillFigures8, _skillFigures9, _skillFigures10)
    {
        skillType = _skillType;
        skillRange = _skillRange;
        maxCoolTime = _maxCoolTime;
        skillHitCount = _skillHitCount;
    }
}
