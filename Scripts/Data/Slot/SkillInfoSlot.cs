using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillInfoSlot : MonoBehaviour
{
    [SerializeField] private Skill curSkill = null;
    [SerializeField] private TextMeshProUGUI skillName = null;
    [SerializeField] private TextMeshProUGUI skillExplain = null;
    [SerializeField] private Image skillImage = null;

    public void InitSkill()
    {
        curSkill = null;
        skillName.text = null;
        skillExplain.text = null;
        skillImage.sprite = null;
    }

    public void SetSkill(Skill _skill,CharacterStatus characterStatus)
    {
        curSkill = _skill;
        skillName.text = curSkill.skillKorName + " Lv. " + curSkill.skillLevel.ToString();
        skillExplain.text = SetSkillDamageByLevel(_skill, characterStatus) + "¸¸Å­ " + curSkill.skillExplain;
        skillImage.sprite = curSkill.singleSprite;
    }
    public int SetSkillDamageByLevel(Skill _skill, CharacterStatus _castingStatus)
    {
        int _skillDamage = 0;
        switch (_skill.skillLevel)
        {
            case 1:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures1);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures1);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures1);
                break;
            case 2:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures2);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures2);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures2);
                break;
            case 3:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures3);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures3);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures3);
                break;
            case 4:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures4);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures4);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures4);
                break;
            case 5:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures5);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures5);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures5);
                break;
            case 6:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures6);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures6);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures6);
                break;
            case 7:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures7);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures7);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures7);
                break;
            case 8:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures8);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures8);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures8);
                break;
            case 9:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures9);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures9);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures9);
                break;
            case 10:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(_castingStatus.TotalStr * _skill.skillFigures10);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(_castingStatus.TotalDex * _skill.skillFigures10);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(_castingStatus.TotalWiz * _skill.skillFigures10);
                break;
        }
        return _skillDamage;
    }
}
  