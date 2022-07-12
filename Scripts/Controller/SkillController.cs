using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    private CharacterStatus status = null;

    [SerializeField]
    private List<Skill> activeSkills = new List<Skill>();
    [SerializeField]
    private List<Skill> passiveSkills = new List<Skill>();
    [SerializeField]
    private List<float> coolTimes = new List<float>();
    [SerializeField]
    private List<SkillObject> skillPrefabs = new List<SkillObject>();
    [SerializeField]
    private bool[] isCoolTime = { false, false, false };

    #region Property
    public List<Skill> ActiveSkills
    {
        get { return activeSkills; }
    }
    public List<Skill> PassiveSkills
    {
        get { return passiveSkills; }
        set { passiveSkills = value; }
    }
    public bool[] IsCoolTime
    {
        get { return isCoolTime; }
        set { isCoolTime = value; }
    }
    #endregion
    private void Start()
    {
        AquireSkill(0);
        AquireSkill(1);
        AquireSkill(2);
    }
    private void Update()
    {
        CalculateSkillCoolTime();
    }
    public void AquireSkill(int _skillKey)
    {
        if (_skillKey < 1000)
        {
            activeSkills.Add(DatabaseManager.Instance.SelectSkill(_skillKey));
            coolTimes.Add(DatabaseManager.Instance.SelectSkill(_skillKey).coolTime);
        }
        else
        {
            passiveSkills.Add(DatabaseManager.Instance.SelectSkill(_skillKey));
            SetPassiveStatus();
            status.UpdateBasicStatus();
        }
        
    }
    public void LevelUpSkill(int _skillKey)
    {
        if (GetPassiveSkill(_skillKey).skillLevel < 10)
        {
            GetPassiveSkill(_skillKey).skillLevel++;
            SetPassiveStatus();
            status.UpdateBasicStatus();
        }
        else
            Debug.Log("스킬 레벨이 MAX");
    }
    public void RemoveSkill(Skill _skill)
    { 
        if (activeSkills.IndexOf(_skill) != -1)
            activeSkills.Remove(_skill);
        else
            Debug.Log("없는 스킬");
    }

    public void UseSkill(Skill _skill, GameObject _target)
    {
        if (activeSkills.IndexOf(_skill) != -1)
        {
            SkillObject _skillObject = skillPrefabs[_skill.skillKey];
            int index = activeSkills.IndexOf(_skill);
            isCoolTime[index] = true;
            coolTimes[index] = 0;

            _skillObject.IsSkillUse = true;
            _skillObject.transform.position = _target.transform.position;
            _skillObject.gameObject.SetActive(true);
            _skillObject.Target = _target;
            _skillObject.Damage = SetSkillDamageByLevel(_skill);
            _skillObject.SkillHitCount = _skill.skillHitCount;
        }
        else
            Debug.Log("없는 스킬");
    }
    public void CalculateSkillCoolTime()
    {
        for (int i = 0; i < coolTimes.Count; i++)
        {
            if (isCoolTime[i])
            {
                coolTimes[i] += Time.deltaTime;
                if (coolTimes[i] >= activeSkills[i].coolTime)
                    isCoolTime[i] = false;
            }
        }
    }
    public void SetPassiveStatus()
    {
        for (int i = 0; i < passiveSkills.Count; i++)
        {
            if (passiveSkills[i].targetStatus == "str")
                status.PassiveStr = SetPassiveSkillByLevel(passiveSkills[i]);
            else if (passiveSkills[i].targetStatus == "dex")
                status.PassiveDex = SetPassiveSkillByLevel(passiveSkills[i]);
            else if (passiveSkills[i].targetStatus == "wiz")
                status.PassiveWiz = SetPassiveSkillByLevel(passiveSkills[i]);
        }
    }
    public Skill GetPassiveSkill(int _skillKey)
    {
        Skill _skill = null;
        for(int i = 0; i < passiveSkills.Count; i++)
        {
            if(_skillKey == passiveSkills[i].skillKey)
                _skill = passiveSkills[i];
        }
        return _skill;
    }
    public int SetPassiveSkillByLevel(Skill _skill)
    {
        int _skillDamage = 0;
        switch (_skill.skillLevel)
        {
            case 1:
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures1);
                break;
            case 2:
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures2);
                break;
            case 3:
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures3);
                break;
            case 4:
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures4);
                break;
            case 5:
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures5);
                break;
            case 6:
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures6);
                break;
            case 7:
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures7);
                break;
            case 8:
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures8);
                break;
            case 9:
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures9);
                break;
            case 10:
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(status.CurLevel * _skill.skillFigures10);
                break;
        }
        Debug.Log("올려주는 능력치는 " + _skillDamage);
        return _skillDamage;
    }
    public int SetSkillDamageByLevel(Skill _skill)
    {
        int _skillDamage = 0;
        switch (_skill.skillLevel)
        {
            case 1:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures1 * 1000);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures1);
                else if (_skill.skillVariable == 2) 
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures1);
                break;
            case 2:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures2);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures2);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures2);
                break;
            case 3:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures3);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures3);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures3);
                break;
            case 4:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures4);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures4);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures4);
                break;
            case 5:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures5);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures5);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures5);
                break;
            case 6:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures6);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures6);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures6);
                break;
            case 7:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures7);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures7);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures7);
                break;
            case 8:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures8);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures8);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures8);
                break;
            case 9:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures9);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures9);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures9);
                break;
            case 10:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(status.TotalStr * _skill.skillFigures10);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(status.TotalDex * _skill.skillFigures10);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(status.TotalWiz * _skill.skillFigures10);
                break;
        }
        return _skillDamage;
    }
}
