using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private CharacterStatus status = null;
    [SerializeField] private List<Skill> skills = new List<Skill>();
    [SerializeField] private List<SkillObject> skillPrefabs = new List<SkillObject>();

    [SerializeField] private List<Skill> skillQueue = new List<Skill>();

    [SerializeField] private bool isSkillDelay = false;
    private bool[] isCoolTime = { false, false, false };

    public bool IsSkillDelay { get { return isSkillDelay; } set { isSkillDelay = value; } }
    #region Property
    public List<Skill> SkillQueue { get { return skillQueue; } }
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
    
        Skill _newSkill = DatabaseManager.Instance.SelectSkill(_skillKey);

        skills.Add(_newSkill);

        skillQueue.Add(_newSkill);
        

        skillQueue.Add(_newSkill);
        
    }
    public void LevelUpSkill(int _skillKey)
    {
        //if (GetPassiveSkill(_skillKey).skillLevel < 10)
        //{

        //    //SetPassiveStatus();
        //    status.UpdateBasicStatus();
        //}
        //else
    }
    public void RemoveSkill(Skill _skill)
    { 
        if (skills.IndexOf(_skill) != -1)
            skills.Remove(_skill);
        else
    }

    public void UseSkill(Skill _skill)
    {
        int index = skillQueue.IndexOf(_skill);
        Debug.Log("�ε����� " + index);
        if (index != -1)
        {

            if (!_skill.isCoolTime)
            {

                SkillObject _skillObject = skillPrefabs[_skill.skillKey];
                _skill.isCoolTime = true;
                _skill.coolTime = 0f;

                if (_skill.skillType == 0)
                {
                    if (status.Target != null)
                    {
                        _skillObject.SetSkill(status.Target, _skill, status);
                    }
                    else
                    {
                        UIManager.Instance.Notice("Ÿ���� ���");
                    }
                }
                else if (_skill.skillType == 1)
                {
                    if (status.AllyTarget != null)
                    {
                        _skillObject.SetSkill(status.AllyTarget, _skill, status);
                    }
                    else
                    {
                        UIManager.Instance.Notice("Ÿ���� ���");
                    }
                    
                }
                else if (_skill.skillType == 2)
                {
                    if (status.AllyTarget != null)
                    {
                        _skillObject.SetSkill(status.AllyTarget, _skill, status);
                    }
                    else
                    {
                        UIManager.Instance.Notice("Ÿ���� ���");
                    } 
                }
                else if (_skill.skillType == 3)
                {
                    if (status.Target != null)
                    {
                        _skillObject.SetSkill(status.Target, _skill, status);
                    }
                    else
                    {
                        UIManager.Instance.Notice("Ÿ���� ���");
                    }
                }
                skillQueue.RemoveAt(index);
            }
            else
                Debug.Log("��Ÿ�� ��");

        }
        else
            Debug.Log("��� ��ų");

    }
    public void UseSkill()
    {
        //int index = activeSkills.IndexOf(skillQueue[0]);
        if(status.Target !=null)
        {
            
            if (skillQueue.Count > 0)
            {

                skillQueue[0].isCoolTime = true;
                skillQueue[0].coolTime = 0;
                //status.IsAtk = true;
                SkillObject _skillObject = skillPrefabs[skillQueue[0].skillKey];
                if(skillQueue[0].skillType == 0)
                {
                    _skillObject.SetSkill(status.Target, skillQueue[0],status);
                }
                else if(skillQueue[0].skillType == 1)
                {
                    _skillObject.SetSkill(status.AllyTarget, skillQueue[0], status);
                }
                else if (skillQueue[0].skillType == 2)
                {
                    _skillObject.SetSkill(status.AllyTarget, skillQueue[0], status);
                }
                else if (skillQueue[0].skillType == 3)
                {
                    _skillObject.SetSkill(status.Target, skillQueue[0], status);
                }
                skillQueue.RemoveAt(0);
            }
            //else if()
        }
        else
        {
        }
    }
    public void CalculateSkillCoolTime()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].isCoolTime)
            {
                skills[i].coolTime += Time.deltaTime;
                if (skills[i].coolTime >= skills[i].maxCoolTime)
                {
                    Debug.Log("��Ÿ�� ȸ�� �Ϸ�");
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
}
