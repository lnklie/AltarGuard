using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    private List<Skill> skills = new List<Skill>();
    // 얻기, 없애기, 사용하기, 스킬 레벨업,
    [SerializeField]
    private List<float> coolTimes = new List<float>();

    [SerializeField]
    private bool[] isCoolTime = { false, false, false };
    [SerializeField]
    private List<SkillObject> skillPrefabs = new List<SkillObject>();

    private Status status = null;

    private void Awake()
    {
        status = this.GetComponentInParent<Status>();
        skillPrefabs.AddRange(this.GetComponentsInChildren<SkillObject>());
        for(int i =0; i < skillPrefabs.Count; i++)
        {
            skillPrefabs[i].gameObject.SetActive(false);
            skillPrefabs[i].gameObject.layer = this.gameObject.layer;
        }
    }
    private void Start()
    {
        AquireSkill(0);
        AquireSkill(1);
        AquireSkill(2);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (!isCoolTime[1])
                UseSkill(skills[1], status.Target);
        }


        CalculateSkillCoolTime();
    }
    public void AquireSkill(int _skillKey)
    {
        skills.Add(DatabaseManager.Instance.SelectSkill(_skillKey));
        coolTimes.Add(DatabaseManager.Instance.SelectSkill(_skillKey).coolTime);
    }

    public void RemoveSkill(Skill _skill)
    { 
        if (skills.IndexOf(_skill) != -1)
            skills.Remove(_skill);
        else
            Debug.Log("없는 스킬");
    }

    public void UseSkill(Skill _skill, GameObject _target)
    {
        if (skills.IndexOf(_skill) != -1)
        {
            SkillObject _skillObject = skillPrefabs[_skill.skillKey];
            int index = skills.IndexOf(_skill);
            isCoolTime[index] = true;
            coolTimes[index] = 0;

            _skillObject.IsSkillUse = true;
            _skillObject.gameObject.SetActive(true);
            _skillObject.Target = _target;
            _skillObject.Damage = SetSkillDamageByLevel(_skill);

            Debug.Log("쿨타임중");
            
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
                if (coolTimes[i] >= skills[i].coolTime)
                    isCoolTime[i] = false;
            }
        }
    }
    public int SetSkillDamageByLevel(Skill _skill)
    {
        int _skillDamage = 0;
        switch (_skill.skillLevel)
        {
            case 1:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(status.Str * _skill.skillFigures1);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(status.Dex * _skill.skillFigures1);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue1 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures1);
                break;
            case 2:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(status.Str * _skill.skillFigures2);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(status.Dex * _skill.skillFigures2);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue2 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures2);
                break;
            case 3:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(status.Str * _skill.skillFigures3);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(status.Dex * _skill.skillFigures3);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue3 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures3);
                break;
            case 4:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(status.Str * _skill.skillFigures4);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(status.Dex * _skill.skillFigures4);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue4 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures4);
                break;
            case 5:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(status.Str * _skill.skillFigures5);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(status.Dex * _skill.skillFigures5);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue5 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures5);
                break;
            case 6:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(status.Str * _skill.skillFigures6);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(status.Dex * _skill.skillFigures6);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue6 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures6);
                break;
            case 7:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(status.Str * _skill.skillFigures7);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(status.Dex * _skill.skillFigures7);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue7 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures7);
                break;
            case 8:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(status.Str * _skill.skillFigures8);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(status.Dex * _skill.skillFigures8);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue8 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures8);
                break;
            case 9:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(status.Str * _skill.skillFigures9);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(status.Dex * _skill.skillFigures9);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue9 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures9);
                break;
            case 10:
                if (_skill.skillVariable == 0)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(status.Str * _skill.skillFigures10);
                else if (_skill.skillVariable == 1)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(status.Dex * _skill.skillFigures10);
                else if (_skill.skillVariable == 2)
                    _skillDamage = _skill.skillValue10 + Mathf.CeilToInt(status.Wiz * _skill.skillFigures10);
                break;
        }
        return _skillDamage;
    }
}
