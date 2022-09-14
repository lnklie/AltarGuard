using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private CharacterStatus status = null;
    [SerializeField] private List<Skill> skills = new List<Skill>();
    [SerializeField] private List<SkillObject> skillPrefabs = new List<SkillObject>();

    [SerializeField] private List<Skill> attackSkillQueue = new List<Skill>();
    [SerializeField] private List<Skill> cureSkillQueue = new List<Skill>();
    [SerializeField] private List<Skill> buffSkillQueue = new List<Skill>();
    [SerializeField] private List<Skill> debuffSkillQueue = new List<Skill>();
    [SerializeField] private bool isSkillDelay = false;
    private bool[] isCoolTime = { false, false, false };

    public bool IsSkillDelay { get { return isSkillDelay; } set { isSkillDelay = value; } }
    #region Property
    public List<Skill> SkillQueue { get { return attackSkillQueue; } }
    public List<Skill> Skills { get { return skills; } }

    public bool[] IsCoolTime { get { return isCoolTime; } set { isCoolTime = value; } }
    #endregion
    private void Start()
    {
    }
    private void Update()
    {
        CalculateSkillCoolTime();

        if(status.IsSkillChange)
        {
            status.IsSkillChange = false;
        }
    }
    public void AquireSkill(int _skillKey)
    {
        Skill _newSkill = DatabaseManager.Instance.SelectSkill(_skillKey);

        skills.Add(_newSkill);
        if (_newSkill.skillType == 0)
        {
            attackSkillQueue.Add(_newSkill);
        }
        else if (_newSkill.skillType == 1)
        {
            cureSkillQueue.Add(_newSkill);
        }
        else if (_newSkill.skillType == 2)
        {
            buffSkillQueue.Add(_newSkill);
        }
        else
        {
            debuffSkillQueue.Add(_newSkill);
        }

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

    public void UseSkill(Skill _skill, Transform _target)
    {
        int index = skills.IndexOf(_skill);
        if (index != -1)
        {
            if (status.Target != null)
            {
                if (!_skill.isCoolTime)
                {
                    SkillObject _skillObject = skillPrefabs[_skill.skillKey];
                    _skill.isCoolTime = true;
                    status.IsAtk = true;
                    _skill.coolTime = 0f;
                    _skillObject.SetSkill(_target, _skill, status);
                    attackSkillQueue.RemoveAt(index);
                }
                else
                    Debug.Log("쿨타임 중");
            }
            else
            {
                UIManager.Instance.Notice("타겟이 없음");
            }
        }
        else
            Debug.Log("없는 스킬");
    }
    public void UseSkill(Transform _target)
    {
        //int index = activeSkills.IndexOf(skillQueue[0]);
        // Debug.Log("인덱스는 " + index);
        if(status.Target !=null)
        {
            
            if (attackSkillQueue.Count > 0)
            {

                SkillObject _skillObject = skillPrefabs[attackSkillQueue[0].skillKey];
                attackSkillQueue[0].isCoolTime = true;
                attackSkillQueue[0].coolTime = 0;
                status.IsAtk = true;
                _skillObject.SetSkill(_target, attackSkillQueue[0],status);
                attackSkillQueue.RemoveAt(0);
            }
            else if(cureSkillQueue.Count > 0)
            {
                // 스킬 큐 여러개로 나눈거 없애기
            }
            else if(buffSkillQueue.Count > 0)
            {

            }
            //else if()
        }
        else
        {
            Debug.Log("타겟이 없음");
        }
    }
    public void CalculateSkillCoolTime()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].isCoolTime)
            {
                skills[i].coolTime += Time.deltaTime;
                Debug.Log("쿨타임 회복 중 ");
                if (skills[i].coolTime >= skills[i].maxCoolTime)
                {
                    Debug.Log("쿨타임 회복 완료");
                    skills[i].coolTime = skills[i].maxCoolTime;
                    skills[i].isCoolTime = false;
                    attackSkillQueue.Add(skills[i]);
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
}
