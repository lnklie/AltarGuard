using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private CharacterStatus status = null;
    [SerializeField] private List<Skill> skills = new List<Skill>();
    [SerializeField] private List<SkillObject> skillPrefabs = new List<SkillObject>();

    [SerializeField] private List<Skill> skillQueue = new List<Skill>();
    [SerializeField] private SpriteRenderer skillScope = null;
    [SerializeField] private bool isSkillDelay = false;

    #region Property
    public bool IsSkillDelay { get { return isSkillDelay; } set { isSkillDelay = value; } }
    public List<Skill> SkillQueue { get { return skillQueue; } }
    public List<Skill> Skills { get { return skills; } }

    #endregion

    private void Update()
    {
        CalculateSkillCoolTime();
        CheckSkillCoolTime();
    }
    public void AquireSkill(Skill _skill)
    {
        skills.Add(_skill);
        skillQueue.Add(_skill);
        skillPrefabs[_skill.skillKey].IsSkillActive = true;
        skillPrefabs[_skill.skillKey].SetSkill(_skill);
    }
    public void LevelUpSkill(int _skillKey)
    {
        //if (GetPassiveSkill(_skillKey).skillLevel < 10)
        //{

        //    //SetPassiveStatus();
        //    status.UpdateBasicStatus();
        //}
        //else
        //    Debug.Log("스킬 레벨이 MAX");
    }
    public void RemoveSkill(Skill _skill)
    { 
        if (skills.IndexOf(_skill) != -1)
            skills.Remove(_skill);
        else
            Debug.Log("없는 스킬");
    }

    public IEnumerator UseSkill(int _index = 0, bool _isRangeExpressed = false)
    {
        if(!status.IsDied)
        {
            if (_index != -1)
            {
                if (!skills[_index].isCoolTime)
                {
                    if (status.Target != null)
                    {
                        if(IsMatchSkillType(skills[_index], status.Target))
                        {
                            SkillObject _skillObject = skillPrefabs[skills[_index].skillKey];
                            skills[_index].isCoolTime = true;
                            skills[_index].coolTime = 0f;
                            Vector2 skillPos = status.Target.transform.position;
                            if (_isRangeExpressed)
                            {
                                skillScope.gameObject.SetActive(true);
                                skillScope.transform.localPosition = skillPos;
                                skillScope.transform.localScale = new Vector2(skills[_index].skillScopeX, skills[_index].skillScopeY);
                                yield return new WaitForSeconds(1f);
                                skillScope.gameObject.SetActive(false);
                            }
                            _skillObject.SetSkillTarget(status, skillPos, SetSkillValueByLevel(skillQueue[0]));
                            skillQueue.RemoveAt(skillQueue.IndexOf(skills[_index]));
                        }
                        else
                            UIManager.Instance.Notice("올바른 타겟이 아닙니다.");
                    }
                    else
                    {
                        UIManager.Instance.Notice("타겟이 없음");
                    }
                }
                else
                    Debug.Log("쿨타임 중");

            }
            else
                Debug.Log("대기열에 스킬이 존재하지 않습니다");
        }

    }
    public IEnumerator UseAutoSkill(bool _isRangeExpressed = false)
    {
        if (!status.IsDied)
        {
            if (skillQueue.Count > 0)
            {
                if (!skillQueue[0].isCoolTime)
                {
                    if (status.Target != null)
                    {
                        SkillObject _skillObject = skillPrefabs[skillQueue[0].skillKey];
                        skillQueue[0].isCoolTime = true;
                        skillQueue[0].coolTime = 0f;
                        Vector2 skillPos = status.Target.transform.position;
                        switch (skillQueue[0].skillType)
                        {
                            case 0:
                                if (IsMatchSkillType(skillQueue[0], status.Target))
                                {
                                    if (_isRangeExpressed)
                                    {
                                        skillScope.gameObject.SetActive(true);
                                        skillScope.color = new Color(1f,0,0,100f/255);
                                        skillScope.transform.localPosition = skillPos;
                                        skillScope.transform.localScale = new Vector2(skillQueue[0].skillScopeX, skillQueue[0].skillScopeY);
                                        yield return new WaitForSeconds(1f);
                                        skillScope.gameObject.SetActive(false);
                                    }
                                    _skillObject.SetSkillTarget(status, skillPos, SetSkillValueByLevel(skillQueue[0]));
                                    StartCoroutine(_skillObject.CastingSkill(0));
                                    skillQueue.RemoveAt(skillQueue.IndexOf(skillQueue[0]));
                                }
                                else
                                    Debug.Log("올바른 타겟이 아닙니다. " + status.Target.ObjectName);
                                break;
                            case 1:
                                if (IsMatchSkillType(skillQueue[0], status.Target))
                                {
                                    if (_isRangeExpressed)
                                    {
                                        skillScope.gameObject.SetActive(true);
                                        skillScope.color = new Color(0, 1f, 0, 100f / 255);
                                        skillScope.transform.localPosition = skillPos;
                                        skillScope.transform.localScale = new Vector2(skillQueue[0].skillScopeX, skillQueue[0].skillScopeY);
                                        yield return new WaitForSeconds(1f);
                                        skillScope.gameObject.SetActive(false);
                                    }
                                    _skillObject.SetSkillTarget(status, skillPos);
                                    status.Target.recovered(SetSkillValueByLevel(skillQueue[0]));
                                    StartCoroutine(_skillObject.CastingSkill(1));
                                    skillQueue.RemoveAt(skillQueue.IndexOf(skillQueue[0]));
                                }
                                else
                                    Debug.Log("올바른 타겟이 아닙니다. " + status.Target.ObjectName);
                                break;
                        }

                    }
                    else
                    {
                        UIManager.Instance.Notice("타겟이 없음");
                    }
                }
                else
                    Debug.Log("쿨타임 중");

            }
            else
                Debug.Log("대기열에 스킬이 존재하지 않습니다");
        }
    }
    public bool IsMatchSkillType(Skill _skill, Status _character)
    {
        bool _bool = false;
        switch (_skill.skillType)
        {
            case (int)ESkillType.Attack:
            case (int)ESkillType.Curse:
                if (_character.gameObject.layer != status.gameObject.layer)
                    _bool = true;
                break;
            case (int)ESkillType.Cure:
            case (int)ESkillType.Buff:
                if (_character.gameObject.layer == status.gameObject.layer)
                    _bool = true;
                break;
        }
        return _bool;
    }
    public void CalculateSkillCoolTime()
    {
        for(int i = 0; i < skills.Count; i++)
        {
            if (skills[i].isCoolTime)
            {
                skills[i].coolTime += Time.deltaTime;

            }
        }

    }
    public void CheckSkillCoolTime()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if(skills[i].isCoolTime)
            {
                if (skills[i].coolTime >= skills[i].maxCoolTime)
                {
                    Debug.Log("쿨타임 회복 완료");
                    skills[i].coolTime = skills[i].maxCoolTime;
                    skills[i].isCoolTime = false;
                    skillQueue.Add(skills[i]);
                }
            }

        }
    }
    //public void SetPassiveStatus()
    //{
    //    for (int i = 0; i < passiveSkills.Count; i++)
    //    {
    //        if (passiveSkills[i].targetStatus == "str")
    //            status.GraceStr = SetPassiveSkillByLevel(passiveSkills[i]);
    //        else if (passiveSkills[i].targetStatus == "dex")
    //            status.GraceDex = SetPassiveSkillByLevel(passiveSkills[i]);
    //        else if (passiveSkills[i].targetStatus == "wiz")
    //            status.GraceWiz = SetPassiveSkillByLevel(passiveSkills[i]);
    //    }
    //}
    public int SetSkillValueByLevel(Skill _skill)
    {
        int _skillDamage = 0;

        if (_skill.skillVariable == 0)
            _skillDamage = _skill.skillValues[_skill.skillLevel] + Mathf.CeilToInt(status.TotalStatus[(int)EStatus.Str] * _skill.skillFigures[_skill.skillLevel]);
        else if (_skill.skillVariable == 1)
            _skillDamage = _skill.skillValues[_skill.skillLevel] + Mathf.CeilToInt(status.TotalStatus[(int)EStatus.Dex] * _skill.skillFigures[_skill.skillLevel]);
        else if (_skill.skillVariable == 2)
            _skillDamage = _skill.skillValues[_skill.skillLevel] + Mathf.CeilToInt(status.TotalStatus[(int)EStatus.Wiz] * _skill.skillFigures[_skill.skillLevel]);

        return _skillDamage;
    }
}
